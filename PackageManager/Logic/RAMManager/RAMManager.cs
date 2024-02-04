using PackageManager.Data;

namespace PackageManager.Logic.RamManager
{
    public class RamManager : IRamManager
    {
        private RamPart firstPart { get; set; }

        private string RamState
        {
            get
            {
                string result = "";
                var currentPart = firstPart;
                do
                {
                    result += $"({currentPart.BeginAddress} - {currentPart.EndAddress}) -> {currentPart.ProgramTask?.TID} -> Prev = {currentPart.PreviousPart?.ProgramTask?.TID}; Next = {currentPart.NextPart?.ProgramTask?.TID}\n";

                    currentPart = currentPart.NextPart;
                }
                while (currentPart != null);

                return result;
            }
        }
        public RamManager(int ramSize, int osSize)
        {
            firstPart = new RamPart { EndAddress = ramSize, BeginAddress = osSize};
        }

        /// <summary>
        /// Печатет состояние ОП в консоль
        /// </summary>
        /// <param name="message">Информационное сообщение, например, место вызова</param>
        private void printState(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.Write(RamState);
        }

        public (bool, RamPart?) FindFreeSpace(int size)
        {
            var currentPart = firstPart;
            
            do
            {
                if (currentPart.IsEmpty && currentPart.EndAddress - currentPart.BeginAddress > size)
                {
                    return (true, currentPart);
                }
                currentPart = currentPart.NextPart;
            }
            while (currentPart != null);

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
            do
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
            while (currentPart != null);
            throw new Exception("При удалении произошла ошибка");
        }

        public RamPart? GetPartByTaskTid(int tid)
        {
            var currentPart = firstPart;
            do
            {
                if (currentPart.ProgramTask?.TID == tid)
                {
                    return currentPart;
                }
                currentPart = currentPart.NextPart;
            }
            while (currentPart != null);

            return null;
        }

        public bool ContainsTask(int tid) => GetPartByTaskTid(tid) != null;

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
                if (freeSpace.NextPart.NextPart != null)
                {
                    freeSpace.NextPart.NextPart.PreviousPart = newTaskPart;
                }
            }
        }      

        public ProgramTask? GetFirstTaskByStatus(Data.TaskStatus status)
        {
            var currentPart = firstPart;
            do
            {
                if (currentPart.ProgramTask != null && currentPart.ProgramTask.Status == status)
                {
                    return currentPart.ProgramTask;
                }

                currentPart = currentPart.NextPart;
            }
            while (currentPart != null);
            return null;
        }

        public ProgramTask? GetNextReadyTaskByTid(int? tid)
        {
            if (tid == null)
            {
                return null;
            }
            var sourcePart = GetPartByTaskTid((int)tid);
            var currentPart = sourcePart;
            if (currentPart == null)
            {
                // Если не смогли найти исходную задачу в памяти, то вообще ничего не возвращаем
                return null;
            }
            do
            {
                currentPart = currentPart.NextPart;
                if (currentPart == null)
                {
                    // Это обеспечивает цикличность
                    // FP -> P1 -> P2 -> SP -> null
                    currentPart = firstPart;
                }
                if (currentPart.ProgramTask != null && currentPart.ProgramTask.Status == Data.TaskStatus.Ready)
                {
                    return currentPart.ProgramTask;
                }
            }
            while (currentPart != sourcePart);
            return null;
        }
    }
}
