using PackageManager.Data;
using PackageManager.Logic.RamManager;
using TaskStatus = PackageManager.Data.TaskStatus;
using static PackageManager.Data.Constants;

namespace PackageManager.Logic.ExecuteStrategy
{
    public class NoWaitStrategy : IExecuteStrategy
    {
        public ExecuteStatistic Execute(Package package, IRamManager ramManager)
        {
            if (package == null || package.Tasks == null || !package.Tasks.Any())
            {
                throw new Exception("Ошибка в пакете задач");
            }

            var statistic = new ExecuteStatistic
            {
                CompletedTicksOnPending = 0,
                CompletedTicksOnExecute = 0,
                TicksOnSwitch = 0
            };

            ProgramTask? currentTask = null;

            while (true)
            {
                /*
                 * Цикл по тикам. За одну итерацию параллельно работают:
                 *      1)  Ядро, выполняющее задачи
                 *      2)  Ядро с RamManager
                 *      3)  Все устройства ввода-вывода
                 *      4*) Ядро со стратегией (будем считать это магией и 
                 *          не будем учитывать затраченные на это ресурсы) 
                 * 
                 * Сперва стратегия актуализирует статусы задач. 
                 * При необходимости RamManager загружает или выгружает задачу
                 */

                // Актуализируем статусы
                foreach (var actualTask in package.Tasks.Where(i => i.Status == TaskStatus.Pending))
                {
                    actualTask.WaitTicks--;
                    if (actualTask.WaitTicks == 0)
                    {
                        // Операция ввода-вывода завершилась
                        actualTask.Operations.RemoveAt(0);
                        actualTask.WaitTicks = PendingIOCost;
                        actualTask.Status = TaskStatus.Ready;
                    }
                    //Время ввода-вывода теперь не учитывается, потому что процессор не простаивает, а занимается выполнением другой задачи
                }

                // Стратегия выбирает задачу
                if (currentTask == null && package.Tasks.Where(i => i.Status == TaskStatus.New || i.Status == TaskStatus.Ready).Any())
                {
                    // Если нет текущей выполняемой задачи и в пакете есть новые или готовые к выполнению задачи
                    // Сначала смотрим в ОП
                    currentTask = ramManager.GetFirstTaskByStatus(TaskStatus.Ready);

                    if (currentTask == null)
                    {
                        // Затем, если не нашли готовую к выполнению задачу в ОП, то ищем в пакете новую
                        foreach (var packageTask in package.Tasks.Where(i => i.Status == TaskStatus.New))
                        {
                            if (ramManager.FindFreeSpace(packageTask.RequiredMemory).Item1)
                            {
                                // Если нашли задачу, которая поместится в память
                                currentTask = packageTask;
                                currentTask.Status = TaskStatus.Performed;
                                try
                                {
                                    ramManager.TryInsertTask(currentTask);
                                    statistic.TicksOnSwitch += SwitchTaskCost;
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception($"Не удалось вставить задачу в память. {ex.Message}");
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        // Значит, получилось взять задачу из ОП
                        statistic.TicksOnSwitch += SwitchTaskCost;
                    }
                    // TODO не рассматривается случай, когда задача в принципе не может поместиться в память.
                    // На текущий момент регулируется только константой в конфиге
                }

                if (currentTask == null)
                {
                    if (package.Tasks.Where(i => i.Status == TaskStatus.Pending).Any())
                    {
                        // Значит, что не нашлось задания для выполнения, но при этом есть задачи ожидающие ввода-вывода
                        statistic.CompletedTicksOnPending++;
                        continue;
                    }
                    // Значит, что задачи кончились
                    return statistic;
                }

                // Выполнение задачи
                if (currentTask.Operations.Any())
                {
                    // Задача еще не выполнена
                    OperationType currentOperation = currentTask.Operations.First();
                    switch (currentOperation)
                    {
                        case OperationType.Arithmetic:
                            currentTask.Operations.RemoveAt(0);
                            statistic.CompletedTicksOnExecute++;
                            break;
                        case OperationType.IO:
                            currentTask.Status = TaskStatus.Pending;
                            statistic.TicksOnSwitch += SwitchTaskCost;
                            currentTask = null;
                            break;
                        default:
                            throw new Exception("Неизвестный тип операции");
                    }
                }
                else
                {
                    currentTask.Status = TaskStatus.Completed;
                    ramManager.DeleteTask(currentTask);
                    statistic.TicksOnSwitch += SwitchTaskCost;
                    currentTask = null;
                }

            }
        }
    }
}
