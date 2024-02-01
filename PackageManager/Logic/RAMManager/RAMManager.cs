using PackageManager.Data;

namespace PackageManager.Logic.RamManager
{
    public class RamManager : IRamManager
    {
        private RamPart firstPart { get; set; }

        public RamManager(int ramSize, int osSize)
        {
            firstPart = new RamPart { EndAddress = ramSize, BeginAddress = osSize};
        }
        public (bool, RamPart?) FindFreeSpace(int size)
        {
            var currentPart = firstPart;
            if (currentPart.IsEmpty && currentPart.EndAddress - currentPart.BeginAddress > size)
            {
                return (true, currentPart);
            }
            while (currentPart.NextPart != null)
            {
                if (currentPart.IsEmpty && currentPart.EndAddress - currentPart.BeginAddress > size)
                {
                    return (true, currentPart);
                }   
                currentPart = currentPart.NextPart;
            }

            return (false, null);
        }

        public bool TryInsertTask(ProgramTask task)
        {
            var freeSpace = FindFreeSpace(task.RequiredMemory);

            if (freeSpace.Item1)
            {
                if (freeSpace.Item2 == null)
                {
                    throw new Exception("Не найдена задача для вставки в ОП");
                }
                InsertTask(freeSpace.Item2, task);
                return true;
            }

            return false;
        }

        public bool DeleteTask(ProgramTask task)
        {
            if (!ContainsTask(task.TID))
            {
                return false;
            }

            var currentPart = firstPart;
            while (currentPart.NextPart != null)
            {
                if (currentPart.ProgramTask?.TID == task.TID)
                {
                    currentPart.ProgramTask = null;
                    currentPart.IsEmpty = true;

                    if (currentPart.PreviousPart?.IsEmpty == true)
                    {
                        // Сливаем предыдущее пустое пространство и текущее
                        currentPart.BeginAddress = currentPart.PreviousPart.BeginAddress;
                        if (currentPart.PreviousPart.PreviousPart != null)
                        {
                            // PP -> P -> C -> N
                            // PP -> C -> N
                            currentPart.PreviousPart.PreviousPart.NextPart = currentPart;
                            currentPart.PreviousPart = currentPart.PreviousPart.PreviousPart;
                        }
                        else
                        {
                            currentPart.PreviousPart = null;
                            firstPart = currentPart;
                        }
                    }

                    if (currentPart.NextPart?.IsEmpty == true)
                    {
                        // Сливаем следующее пустое пространство и текущее
                        currentPart.EndAddress = currentPart.NextPart.EndAddress;
                        if (currentPart.NextPart.NextPart != null)
                        {
                            currentPart.NextPart.NextPart.PreviousPart = currentPart;
                            currentPart.NextPart = currentPart.NextPart.NextPart;
                        }
                        else
                        {
                            currentPart.NextPart = null;
                        }
                    }

                    return true;
                }
                currentPart = currentPart.NextPart;
            }
            throw new Exception("При удалении произошла ошибка");
        }

        public bool ContainsTask(int tid)
        {
            var currentPart = firstPart;
            while (currentPart.NextPart != null)
            {
                if (currentPart.ProgramTask?.TID == tid)
                {
                    return true;
                }
                currentPart = currentPart.NextPart;
            }

            return false;
        }

        public void InsertTask(RamPart freeSpace, ProgramTask task)
        {
            freeSpace.IsEmpty = false;
            freeSpace.ProgramTask = task;

            if (freeSpace.EndAddress - freeSpace.BeginAddress != task.RequiredMemory)
            {
                RamPart newTaskPart = new()
                {
                    BeginAddress = freeSpace.BeginAddress + task.RequiredMemory,
                    EndAddress = freeSpace.EndAddress,
                    IsEmpty = true,
                    PreviousPart = freeSpace,
                    NextPart = freeSpace.NextPart,
                    ProgramTask = null
                };
                freeSpace.EndAddress = newTaskPart.BeginAddress;
                freeSpace.NextPart = newTaskPart;
            }
        }

        public RamPart GetRamPart()
        {
            return firstPart;
        }
    }
}
