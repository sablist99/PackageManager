using PackageManager.Data;
using static PackageManager.Data.Constants;

namespace PackageManager.Logic.TaskFactory
{
    public class BalanceTaskFactory : TaskFactory
    {
        public override ProgramTask GetTask()
        {
            ProgramTask task = base.GetTask();
            var random = new Random();

            task.ArithmeticOperations = random.Next(MediumQuantityMin, MediumQuantityMax);
            task.IOOperations = random.Next(MediumQuantityMin, MediumQuantityMax);

            return task;
        }
    }
}
