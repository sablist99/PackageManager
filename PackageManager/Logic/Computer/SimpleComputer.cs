using PackageManager.Data;
using PackageManager.Logic.ExecuteStrategy;
using PackageManager.Logic.PackageBuilder;
using PackageManager.Logic.RamManager;
using static PackageManager.Data.Constants;

namespace PackageManager.Logic.Computer
{
    public class SimpleComputer : Computer
    {
        public SimpleComputer(IExecuteStrategy strategy)
        {
            Strategy = strategy;
            PackageBuilder = new PackageBuilder.PackageBuilder();
            RamManager = new RamManager.RamManager(RAMCapacity, OperationSystemWeight);
        }

        protected override IExecuteStrategy Strategy { get; set; }
        protected override IPackageBuilder PackageBuilder { get; set; }
        protected override IRamManager RamManager { get; set; }

        public override Report Start(TaskType taskType, int percent)
        {
            return new Report
            {
                Statistic = Strategy.Execute(PackageBuilder.GetPackage(taskType, percent), RamManager),
                Type = taskType,
                Percent = percent
            };
        }

        public override Report Start(Package package, TaskType taskType, int percent)
        {
            return new Report
            {
                Statistic = Strategy.Execute(package, RamManager),
                Type = taskType,
                Percent = percent
            };
        }
    }
}
