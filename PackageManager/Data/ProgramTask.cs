namespace PackageManager.Data
{
    public class ProgramTask
    {
        /// <summary>
        /// Требуемое количество оперативной памяти в МБ
        /// </summary>
        public int RequiredMemory { get; set; }

        /// <summary>
        /// Адрес задачи в ОП
        /// </summary>
        public int StartMemory { get; set; }

        /// <summary>
        /// Текущий статус задачи
        /// </summary>
        public TaskStatus Status { get; set; }

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
