using PackageManager.Data;
using PackageManager.Logic.ExecuteStrategy;
using PackageManager.Logic.PackageBuilder;
using PackageManager.Logic.RAMManager;
using static PackageManager.Data.Constants;

namespace PackageManager.Logic.Computer
{
    public class Computer : IComputer
    {
        public Computer(IExecuteStrategy strategy)
        {
            Strategy = strategy;
            PackageBuilder = new PackageBuilder.PackageBuilder();
            RamManager = new RamManager(RAMCapacity, OperationSystemWeight);
        }

        public IExecuteStrategy Strategy { get; set; }
        public IPackageBuilder PackageBuilder { get; set; }
        public IRamManager RamManager { get; set; }

        public Report Start(TaskType taskType, int percent)
        {
            return new Report
            {
                Statistic = Strategy.Execute(PackageBuilder.GetPackage(taskType, percent)),
                Type = taskType,
                Percent = percent
            };
        }
    }
}
