using PackageManager.Data;
using PackageManager.Logic.TaskFactory;

namespace PackageManager.Logic.PackageBuilder
{
    public interface IPackageBuilder
    {
        public IEnumerable<ITaskFactory> Factories { get; set; }
        public Package GetPackage();
    }
}
