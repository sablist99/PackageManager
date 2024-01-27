namespace PackageManager.Data
{
    public class Package
    {
        /// <summary>
        /// Пакет задач, отправляемых на исполнение
        /// </summary>
        public IEnumerator<ProgramTask> Tasks { get; set; }
    }
}
