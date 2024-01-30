using PackageManager.Data;
using System;
using static PackageManager.Data.Constants;
using TaskStatus = PackageManager.Data.TaskStatus;

namespace PackageManager.Logic.TaskFactory
{
    public abstract class TaskFactory
    {
        public virtual ProgramTask GetTask()
        {
            var random = new Random();

            return new ProgramTask()
            {
                TID = GlobalTID++,
                Status = TaskStatus.New,
                RequiredMemory = random.Next(RequiredMemoryMin, RequiredMemoryMax)
            };
        }   
    }
}
