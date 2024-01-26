using PackageManager.Data;
using PackageManager.Logic.TaskFactory;

namespace PackageManager.Logic.PackageBuilder
{
    public class PackageBuilder : IPackageBuilder
    {
        public IEnumerable<ITaskFactory> Factories { get; set; }
        public Package GetPackage()
        {
            throw new NotImplementedException();
        }
    }
}
