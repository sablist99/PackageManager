using PackageManager.Data;
using PackageManager.Logic.TaskFactory;

namespace PackageManager.Logic.PackageBuilder
{
    public class PackageBuilder : IPackageBuilder
    {
        public PackageBuilder(){}
        public IDictionary<TaskType, ITaskFactory> Factories { get; set; } = new Dictionary<TaskType, ITaskFactory>()
        {
            {TaskType.Arithmetic, new ArithmeticTaskFactory()},
            {TaskType.Balance, new BalanceTaskFactory()},
            {TaskType.IO, new IOTaskFactory()},
        };

        public IDictionary<TaskType, int> Parts { get; set; }

        public Package GetPackage(TaskType taskType, int percent)
        {
            Parts = new Dictionary<TaskType, int>()
            {
                { taskType, percent }
            };

            // Пренебрегаем точностью при делении. Например, если Percent = 45, то на оставшиеся части должно прийтись  по 27,5. Но храним в int.
            // Небольшая неточность роли не сыграет. Главное, что Percent = 45
            var otherPartPercent = (100 - percent) / (Factories.Count() - 1);
            var probabilities = new List<int>();

            // Заполняем вероятность выпадения исследуемого типа задач
            for (var i = 0; i < percent; i++)
            {
                probabilities.Add((int)taskType);
            }

            foreach (var currentTask in Factories.Keys)
            {
                if (currentTask == taskType) continue;
                for (var i = 0; i < otherPartPercent; i++)
                {
                    probabilities.Add((int)currentTask);
                }
            }

            var counter = 0;
            var tasks = new List<ProgramTask>();
            var random = new Random();
            while (counter++ < Constants.TaskCount)
            {                                                                     
                tasks.Add(Factories[(TaskType)probabilities[random.Next(0, 100)]].GetTask());
            }

            return new Package() { Tasks = tasks };
        }
    }
}
