using PackageManager.Data;
using PackageManager.Logic.ExecuteStrategy;
using PackageManager.Logic.PackageBuilder;

namespace PackageManager.Logic.Computer
{
    public class Computer : IComputer
    {
        public IExecuteStrategy Strategy { get; set; }
        public IPackageBuilder PackageBuilder { get; set; }
        public Package GetPackage()
        {
            throw new NotImplementedException();
        }

        public Report GetReport()
        {
            throw new NotImplementedException();
        }
    }
}
