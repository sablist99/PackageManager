using System.Threading.Tasks;

namespace PackageManager.Data
{
    public class ProgramTask : ICloneable
    {
        /// <summary>
        /// Уникальный идентификатор задачи
        /// </summary>
        public int TID { get; set; }
        /// <summary>
        /// Требуемое количество оперативной памяти в МБ
        /// </summary>
        public int RequiredMemory { get; set; }

        /// <summary>
        /// Текущий статус задачи
        /// </summary>
        public TaskStatus Status { get; set; }

        /// <summary>
        /// Список операций к выполнению
        /// </summary>
        public IList<OperationType> Operations { get; set; } = new List<OperationType>();

        /// <summary>
        /// Используется при операциях ввода-вывода. Показатель того, сколько тиков еще будет длиться операция
        /// </summary>
        public int WaitTicks { get; set; } = 5;
        
        /// <summary>
        /// Количество арифметических операций
        /// </summary>
        public int ArithmeticOperationsCount { get; set; }

        /// <summary>
        /// Количество операций ввода-вывода
        /// </summary>
        public int IOOperationsCount { get; set; }

        /// <summary>
        /// Затраченное число тиков на ожидание ввода-вывода
        /// </summary>
        public int CompletedTicksOnPending { get; set; }

        /// <summary>
        /// Затраченное число тиков на выполнение задачи
        /// </summary>
        public int CompletedTicksOnExecute { get; set; }

        public object Clone()
        {
            List<OperationType> newList = new(Operations.Count);

            foreach (var item in Operations)
            {
                newList.Add(item);
            }
            return new ProgramTask()
            {
                TID = TID,
                RequiredMemory = RequiredMemory,
                Status = Status,
                Operations = newList,
                WaitTicks = WaitTicks,
                ArithmeticOperationsCount = ArithmeticOperationsCount,
                IOOperationsCount = IOOperationsCount,
                CompletedTicksOnPending = CompletedTicksOnPending,
                CompletedTicksOnExecute = CompletedTicksOnExecute,
            };
        }
    }
}
