using PackageManager.Data;

namespace PackageManager.Logic.RamManager
{
    public interface IRamManager
    {
        public (bool, RamPart?) FindFreeSpace(int size);

        public bool TryInsertTask(ProgramTask task);

        public bool DeleteTask(ProgramTask task);

        public bool ContainsTask(int tid);
        public RamPart GetRamPart();
        protected void InsertTask(RamPart freeSpace, ProgramTask task);
    }
}
