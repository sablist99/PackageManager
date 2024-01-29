namespace PackageManager.Data
{
    public static class Constants
    {
        /// <summary>
        /// Количество тиков на операцию ввода-вывода
        /// </summary>
        public static int PendingIOCost = 5;

        /// <summary>
        /// Количество тиков на переключение задачи
        /// </summary>
        public static int SwitchTaskCost = 2;

        /// <summary>
        /// Количество ОП в компьютере в МБ
        /// </summary>
        public static int RAMCapacity = 4096;

        /// <summary>                                                                 
        /// Место, занимаемое ОС, в МБ
        /// </summary>
        public static int OperationSystemWeight = 1024;

        public static int GlobalPID = 1;
    }
}
