using PackageManager.Data;
using PackageManager.Logic.ExecuteStrategy;
using PackageManager.Logic.PackageBuilder;

namespace PackageManager.Logic.Computer
{
    public interface IComputer
    {
        public IExecuteStrategy Strategy { get; set; }
        public IPackageBuilder PackageBuilder { get; set; }
        public Package GetPackage();
        public Report GetReport();
    }
}
