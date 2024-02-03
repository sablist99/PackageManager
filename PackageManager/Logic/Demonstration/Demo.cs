using PackageManager.Data;
using PackageManager.Logic.Computer;
using PackageManager.Logic.ExecuteStrategy;

namespace PackageManager.Logic.Demonstration
{
    public static class Demo
    {
        public static Dictionary<string, List<(string, ExecuteStatistic)>> GetDataCompareAlgorithmsByCompletedticksByTaskTypes()
        {
            return new Dictionary<string, List<(string, ExecuteStatistic)>>
            {
                {"Вычислительные операции", GetDataCompareAlgorithmsByCompletedticks(TaskType.Arithmetic) },
                {"Сбалансированные операции", GetDataCompareAlgorithmsByCompletedticks(TaskType.Balance) },
                {"Операции ввода-вывода", GetDataCompareAlgorithmsByCompletedticks(TaskType.IO) },
            };
        }
        private static List<(string, ExecuteStatistic)> GetDataCompareAlgorithmsByCompletedticks(TaskType type)
        {
            SimpleComputer queueComputer = new(new QueueStrategy());
            SimpleComputer noWaitComputer = new(new NoWaitStrategy());
            SimpleComputer robinRoundComputer_1 = new(new RobinRoundStrategy(1));
            SimpleComputer robinRoundComputer_5 = new(new RobinRoundStrategy(5));
            SimpleComputer robinRoundComputer_10 = new(new RobinRoundStrategy(10));
            SimpleComputer robinRoundComputer_15 = new(new RobinRoundStrategy(15));
            SimpleComputer robinRoundComputer_20 = new(new RobinRoundStrategy(20));

            Package referencePackage = (new PackageBuilder.PackageBuilder()).GetPackage(type, 100);

            Package packageForQueue = (Package)referencePackage.Clone();
            Package packageForBNoWait = (Package)referencePackage.Clone();
            Package packageForRobinRound_1 = (Package)referencePackage.Clone();
            Package packageForRobinRound_5 = (Package)referencePackage.Clone();
            Package packageForRobinRound_10 = (Package)referencePackage.Clone();
            Package packageForRobinRound_15 = (Package)referencePackage.Clone();
            Package packageForRobinRound_20 = (Package)referencePackage.Clone();

            return new List<(string, ExecuteStatistic)>
            {
                {("Очередь", queueComputer.Start(packageForQueue) )},
                {("Очередь без ожидания", noWaitComputer.Start(packageForBNoWait) )},
                {("RobinRound, ticks = 1", robinRoundComputer_1.Start(packageForRobinRound_1))},
                {("RobinRound, ticks = 5", robinRoundComputer_5.Start(packageForRobinRound_5))},
                {("RobinRound, ticks = 10", robinRoundComputer_10.Start(packageForRobinRound_10))},
                {("RobinRound, ticks = 15", robinRoundComputer_15.Start(packageForRobinRound_15))},
                {("RobinRound, ticks = 20", robinRoundComputer_20.Start(packageForRobinRound_20))},
            };
        }


        public static Dictionary<string, Dictionary<string, List<ExecuteStatistic>>> GetDataCompareAlgorithmsByTaskTypes()
        {
            return new Dictionary<string, Dictionary<string, List<ExecuteStatistic>>>
            {
                {"Вычислительные операции", GetDataByTaskType(TaskType.Arithmetic) },
                {"Сбалансированные операции", GetDataByTaskType(TaskType.Balance) },
                {"Операции ввода-вывода", GetDataByTaskType(TaskType.IO) },
            };
        }

        private static Dictionary<string, List<ExecuteStatistic>> GetDataByTaskType(TaskType type)
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
