using PackageManager.Data;
using TaskStatus = PackageManager.Data.TaskStatus;

namespace PackageManager.Logic.RamManager
{
    public interface IRamManager
    {
        public (bool, RamPart?) FindFreeSpace(int size);

        public bool TryInsertTask(ProgramTask task);

        public bool DeleteTask(ProgramTask task);

        public bool ContainsTask(int tid);

        public RamPart? GetPartByTaskTid(int tid);
        
        public RamPart GetRamPart();

        public ProgramTask? GetFirstTaskByStatus(TaskStatus status);

        public ProgramTask? GetNextReadyTaskByTid(int? tid);

        protected void InsertTask(RamPart freeSpace, ProgramTask task);
    }
}
