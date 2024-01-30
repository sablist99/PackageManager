namespace PackageManager.Data
{
    public class ProgramTask
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
        /// Количество арифметических операций
        /// </summary>
        public int ArithmeticOperations { get; set; }

        /// <summary>
        /// Количество операций ввода-вывода
        /// </summary>
        public int IOOperations { get; set; }

        /// <summary>
        /// Затраченное число тиков на ожидание ввода-вывода
        /// </summary>
        public int CompletedTicksOnPending { get; set; }

        /// <summary>
        /// Затраченное число тиков на выполнение задачи
        /// </summary>
        public int CompletedTicksOnExecute { get; set; }

    }
}
