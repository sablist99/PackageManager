using PackageManager.Data;

namespace PackageManager.Logic.TaskFactory
{
    public interface ITaskFactory
    {
        public ProgramTask GetTask();
    }
}
