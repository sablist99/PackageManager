namespace PackageManager.Data
{
    public class Report
    {
        /// <summary>
        /// Статистика по пакету
        /// </summary>
        public ExecuteStatistic Statistic { get; set; }

        /// <summary>
        /// Тип задачи по количеству операций
        /// </summary>
        public TaskType Type { get; set; }

        /// <summary>
        /// Процентная составляющая задач в пакете
        /// </summary>
        public int Percent { get; set; }
    }
}
