using PackageManager.Data;
using PackageManager.Logic.Computer;
using PackageManager.Logic.ExecuteStrategy;

namespace PackageManager.Logic.Demonstration
{
    public static class Demo
    {
        public static Dictionary<string, Dictionary<string, List<ExecuteStatistic>>> GetData()
        {
            return new Dictionary<string, Dictionary<string, List<ExecuteStatistic>>>
            {
                {"Преобладание вычислительных операций", GetDataByTaskType(TaskType.Arithmetic) },
                {"Сбалансированный", GetDataByTaskType(TaskType.Balance) },
                {"Преобладание ввода-вывода", GetDataByTaskType(TaskType.IO) },
            };
        }

        public static Dictionary<string, List<ExecuteStatistic>> GetDataByTaskType(TaskType type)
        {
            SimpleComputer queueComputer = new(new QueueStrategy());
            SimpleComputer noWaitComputer = new(new NoWaitStrategy());
            SimpleComputer robinRoundComputer = new(new RobinRoundStrategy());

            List<ExecuteStatistic> queueReports = new();
            List<ExecuteStatistic> noWaitReports = new();
            List<ExecuteStatistic> robinRoundReports = new();

            for (int percent = 10; percent <= 100; percent += 10)
            {
                Package referencePackage = (new PackageBuilder.PackageBuilder()).GetPackage(type, percent);

                Package packageForQueue = (Package)referencePackage.Clone();
                Package packageForBNoWait = (Package)referencePackage.Clone();
                Package packageForRobinRound = (Package)referencePackage.Clone();

                queueReports.Add(queueComputer.Start(packageForQueue));
                noWaitReports.Add(noWaitComputer.Start(packageForBNoWait));
                robinRoundReports.Add(robinRoundComputer.Start(packageForRobinRound));
            }
            return new Dictionary<string, List<ExecuteStatistic>>
            {
                { "Очередь", queueReports },
                { "Очередь без ожидания", noWaitReports },
                { "RobinRound", robinRoundReports }
            };
        }
    }
}
