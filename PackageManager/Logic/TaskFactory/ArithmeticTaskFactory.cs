using PackageManager.Data;
using static PackageManager.Data.Constants;

namespace PackageManager.Logic.TaskFactory
{
    public class ArithmeticTaskFactory : TaskFactory
    {
        public override int ArithmeticOperationsMin { get => LargeQuantityMin; }
        public override int ArithmeticOperationsMax { get => LargeQuantityMax; }
        public override int IoOperationsMin { get => SmallQuantityMin; }
        public override int IoOperationsMax { get => SmallQuantityMax; }
        public override ProgramTask GetTask() => base.GetTask();
    }
}
