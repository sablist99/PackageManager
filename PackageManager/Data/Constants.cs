namespace PackageManager.Data
{
    public static class Constants
    {
        /// <summary>
        /// Количество тиков на операцию ввода-вывода
        /// </summary>
        public const int PendingIOCost = 5;

        /// <summary>
        /// Количество тиков на переключение задачи
        /// </summary>
        public const int SwitchTaskCost = 2;

        /// <summary>
        /// Количество ОП в компьютере в МБ
        /// </summary>
        public const int RAMCapacity = 4096;

        /// <summary>                                                                 
        /// Место, занимаемое ОС, в МБ
        /// </summary>
        public const int OperationSystemWeight = 1024;

        /// <summary>
        /// Количество задач, на которых проводится тестирование
        /// </summary>
        public const int TaskCount = 10000;

        /// <summary>
        /// Синглтон для выдачи уникального номера задачи
        /// </summary>
        public static int GlobalTID = 1;

    }
}
