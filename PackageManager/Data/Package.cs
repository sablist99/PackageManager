namespace PackageManager.Data
{
    public class Package : ICloneable
    {
        /// <summary>
        /// Пакет задач, отправляемых на исполнение
        /// </summary>
        public IEnumerable<ProgramTask> Tasks { get; set; } = new List<ProgramTask>();

        public object Clone()
        {
            List<ProgramTask> newList = new(Tasks.Count());

            foreach (var item in Tasks)
            {
                newList.Add((ProgramTask)item.Clone());
            }
            return new Package() { Tasks = newList };
        }
    }
}
