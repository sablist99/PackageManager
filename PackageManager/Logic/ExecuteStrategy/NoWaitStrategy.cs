using PackageManager.Data;
using PackageManager.Logic.RamManager;

namespace PackageManager.Logic.ExecuteStrategy
{
    public class NoWaitStrategy : IExecuteStrategy
    {
        public ExecuteStatistic Execute(Package package, IRamManager ramManager)
        {
            throw new NotImplementedException();
        }
    }
}
