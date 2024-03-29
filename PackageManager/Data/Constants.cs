﻿namespace PackageManager.Data
{
    public static class Constants
    {
        // Пороговые значения количества операций
        public const int SmallQuantityMin = 1;
        public const int SmallQuantityMax = 10;
        public const int MediumQuantityMin = 11;
        public const int MediumQuantityMax = 20;
        public const int LargeQuantityMin = 21;
        public const int LargeQuantityMax = 30;

        // Пороговые значения занимаемой задачами памяти
        public const int RequiredMemoryMin = 50;
        public const int RequiredMemoryMax = 500;

        /// <summary>
        /// Количество тиков на операцию ввода-вывода
        /// </summary>
        public static int PendingIOCost = 5;

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
        public static int TaskCount = 100;

        /// <summary>
        /// Синглтон для выдачи уникального номера задачи
        /// </summary>
        public static int GlobalTID = 1;
    }
}
