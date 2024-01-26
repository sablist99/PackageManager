using PackageManager.Data;

namespace PackageManager.Logic.ExecuteStrategy
{
    public interface IExecuteStrategy
    {
        public ExecuteStatistic Execute(Package package);
    }
}
