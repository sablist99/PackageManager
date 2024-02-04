using PackageManager.Data;
using PackageManager.Extension;
using System;
using System.Threading.Tasks;
using static PackageManager.Data.Constants;
using TaskStatus = PackageManager.Data.TaskStatus;

namespace PackageManager.Logic.TaskFactory
{
    public abstract class AbstractTaskFactory
    {
        public virtual int ArithmeticOperationsMin { get; }
        public virtual int ArithmeticOperationsMax { get; }
        public virtual int IoOperationsMin { get; }
        public virtual int IoOperationsMax { get; }


        public virtual ProgramTask GetTask()
        {
            var random = new Random();

            int arithmeticOperationsCount = 1;
            int ioOperationsCount = 1;
            return new ProgramTask()
            {
                TID = GlobalTID++,
                Status = TaskStatus.New,
                RequiredMemory = random.Next(RequiredMemoryMin, RequiredMemoryMax),
                ArithmeticOperationsCount = arithmeticOperationsCount = random.Next(ArithmeticOperationsMin, ArithmeticOperationsMax),
                IOOperationsCount = ioOperationsCount = random.Next(IoOperationsMin, IoOperationsMax),
                Operations = GetOperationQueue(arithmeticOperationsCount, ioOperationsCount)
            };
        }   

        private IList<OperationType> GetOperationQueue(int arithmeticOperationCount, int IoOperationCount)
        {
            IList<OperationType> operations = new List<OperationType>();

            for (int i = 0; i < arithmeticOperationCount; i++)
            {
                operations.Add(OperationType.Arithmetic);
            }

            for (int i = 0; i < IoOperationCount; i++)
            {
                operations.Add(OperationType.IO);
            }

            return operations.MixData();
        }
    }
}
