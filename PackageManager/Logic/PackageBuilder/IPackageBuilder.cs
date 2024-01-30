using PackageManager.Data;
using PackageManager.Logic.TaskFactory;

namespace PackageManager.Logic.PackageBuilder
{
    public interface IPackageBuilder
    {
        public IDictionary<TaskType, TaskFactory.TaskFactory> Factories { get; set; }
        public Package GetPackage(TaskType taskType, int percent);
    }
}
