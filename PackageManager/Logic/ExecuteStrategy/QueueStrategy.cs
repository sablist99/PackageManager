using PackageManager.Data;
using PackageManager.Logic.RamManager;
using TaskStatus = PackageManager.Data.TaskStatus;
using static PackageManager.Data.Constants;

namespace PackageManager.Logic.ExecuteStrategy
{
    public class QueueStrategy : IExecuteStrategy
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

                // Стратегия выбирает задачу
                if (currentTask == null && package.Tasks.Where(i => i.Status == TaskStatus.New).Any())
                {
                    // Если нет текущей выполняемой задачи и в пакете есть новые задачи, то берем из пакета.

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
                    // TODO не рассматривается случай, когда задача в принципе не может поместиться в память.
                    // На текущий момент регулируется только константой в конфиге
                }

                if (currentTask == null)
                {
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
                            currentTask.WaitTicks--;
                            if (currentTask.WaitTicks == 0)
                            {
                                // Операция ввода-вывода завершилась
                                currentTask.Operations.RemoveAt(0);
                                currentTask.WaitTicks = 5;
                            }
                            statistic.CompletedTicksOnPending++;
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
