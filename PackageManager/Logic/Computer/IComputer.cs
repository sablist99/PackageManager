using PackageManager.Data;
using PackageManager.Logic.ExecuteStrategy;
using PackageManager.Logic.PackageBuilder;
using PackageManager.Logic.RAMManager;

namespace PackageManager.Logic.Computer
{
    public interface IComputer
    {
        public IExecuteStrategy Strategy { get; set; }
        public IPackageBuilder PackageBuilder { get; set; }
        public IRamManager RamManager { get; set; }
        public Report Start(TaskType taskType, int percent);
    }
}
