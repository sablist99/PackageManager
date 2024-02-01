namespace PackageManager.Data
{
    public class Package
    {
        /// <summary>
        /// Пакет задач, отправляемых на исполнение
        /// </summary>
        public IEnumerable<ProgramTask> Tasks { get; set; } = new List<ProgramTask>();
    }
}
