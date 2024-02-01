using PackageManager.Data;
using static PackageManager.Data.Constants;

namespace PackageManager.Logic.TaskFactory
{
    public class BalanceTaskFactory : TaskFactory
    {
        public override int ArithmeticOperationsMin { get => MediumQuantityMin; }
        public override int ArithmeticOperationsMax { get => MediumQuantityMax; }
        public override int IoOperationsMin { get => MediumQuantityMin; }
        public override int IoOperationsMax { get => MediumQuantityMax; }
        public override ProgramTask GetTask() => base.GetTask();
    }
}
