using PackageManager.Data;
using PackageManager.Logic.RamManager;

namespace PackageManager.Logic.ExecuteStrategy
{
    public interface IExecuteStrategy
    {
        public ExecuteStatistic Execute(Package package, IRamManager ramManager);
    }
}
