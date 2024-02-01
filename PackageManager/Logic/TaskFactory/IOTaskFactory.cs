using PackageManager.Data;
using static PackageManager.Data.Constants;

namespace PackageManager.Logic.TaskFactory
{
    public class IoTaskFactory : TaskFactory
    {
        public override int ArithmeticOperationsMin { get => SmallQuantityMin; }
        public override int ArithmeticOperationsMax { get => SmallQuantityMax; }
        public override int IoOperationsMin { get => LargeQuantityMin; }
        public override int IoOperationsMax { get => LargeQuantityMax; }
        public override ProgramTask GetTask() => base.GetTask();
    }
}
