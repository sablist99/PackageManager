﻿namespace PackageManager.Data
{
    public class ExecuteStatistic
    {
        /// <summary>
        /// Затраченное число тиков на ожидание ввода-вывода
        /// </summary>
        public int CompletedTicksOnPending { get; set; }

        /// <summary>
        /// Затраченное число тиков на выполнение задачи
        /// </summary>
        public int CompletedTicksOnExecute { get; set; }

        /// <summary>
        /// Затраченное число тиков на переключение задач
        /// </summary>
        public int TicksOnSwitch { get; set; }

        /// <summary>
        /// Полезная работа процессора
        /// </summary>
        public double UsefulWork =>
            (double)CompletedTicksOnExecute /
            (TicksOnSwitch + CompletedTicksOnExecute + CompletedTicksOnPending);
    }
}
