using PackageManager.Data;
using PackageManager.Logic.ExecuteStrategy;
using PackageManager.Logic.PackageBuilder;
using PackageManager.Logic.RamManager;

namespace PackageManager.Logic.Computer
{
    public abstract class Computer
    {
        protected abstract IExecuteStrategy Strategy { get; set; }
        protected abstract IPackageBuilder PackageBuilder { get; set; }
        protected abstract IRamManager RamManager { get; set; }
        public abstract Report Start(TaskType taskType, int percent);
        public abstract Report Start(Package package, TaskType taskType, int percent);

    }
}
