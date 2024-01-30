using PackageManager.Data;
using static PackageManager.Data.Constants;

namespace PackageManager.Logic.TaskFactory
{
    public class IoTaskFactory : TaskFactory
    {
        public override ProgramTask GetTask()
        {
            ProgramTask task = base.GetTask();
            var random = new Random();

            task.ArithmeticOperations = random.Next(SmallQuantityMin, SmallQuantityMax);
            task.IOOperations = random.Next(LargeQuantityMin, LargeQuantityMax);

            return task;
        }
    }
}
