using PackageManager.Data;
using static PackageManager.Data.Constants;

namespace PackageManager.Logic.TaskFactory
{
    public class ArithmeticTaskFactory : TaskFactory
    {
        public override ProgramTask GetTask()
        {
            ProgramTask task = base.GetTask();
            var random = new Random();

            task.ArithmeticOperations = random.Next(LargeQuantityMin, LargeQuantityMax);
            task.IOOperations = random.Next(SmallQuantityMin, SmallQuantityMax);

            return task;
        }
    }
}
