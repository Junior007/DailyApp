using daily.domain.Models.Daily;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace daily.application.Services
{
    public class DailyServices : IDailyServices
    {

        private readonly string _pathBase = @"c:\temp\";
        private readonly string _fileNameDatePattern = "yyyyMMdd";
        private readonly string _taskDescriptionDatePattern = @"dddd - dd/MM/yyyy";


        public DailyServices()
        {
            string _pathAssembly = Assembly.GetAssembly(this.GetType()).Location.Replace(Assembly.GetAssembly(this.GetType()).ManifestModule.Name,"");
            _pathBase = $"{_pathAssembly}\\files\\";
            if (!Directory.Exists(_pathBase))
            {
                Directory.CreateDirectory(_pathBase);
            }
        }
        public DailyTask Get()
        {

            DailyTask today = Get(DateTime.Now);
            return today;

        }

        public IList<DailyTask> GetWeek()
        {
            int maxIterations = 10;
            int totalDays = 5;
            IList<DailyTask> dailyTasks = new List<DailyTask>();

            for (int i = 0; i < 10 && dailyTasks.Count < 5; i++)
            {
                DailyTask currentTask = Get(DateTime.Now.AddDays(-1 * i));
                if (currentTask != null)
                    dailyTasks.Add(currentTask);
            }

            return dailyTasks;
        }
        public DailyTask Get(DateTime date)
        {
            string filePathaName = $"{_pathBase}{date.ToString(_fileNameDatePattern)}.json";
            DailyTask mainTask;
            if (!File.Exists(filePathaName) && $"{date.Year}-{date.Month}-{date.Day}" == $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}")
            {
                mainTask = new DailyTask() {  Description = date.ToString(_taskDescriptionDatePattern).ToUpper() };
                mainTask.AddTask(new DailyTask() { Description = "COMIDA" });
                mainTask.AddTask(new DailyTask() { Description = "DESCANSO" });
                Save(mainTask);
            }
            if (File.Exists(filePathaName))
            {
                string jsonString = File.ReadAllText(filePathaName);
                mainTask = JsonSerializer.Deserialize<DailyTask>(jsonString);
                mainTask.RefreshEventHandlers();
                return mainTask;
            }
            return null;
        }

        public void Save(DailyTask mainTask)
        {
            string filePathaName = $"{_pathBase}{mainTask.Date.ToString(_fileNameDatePattern)}.json";
            string jsonString = JsonSerializer.Serialize(mainTask);
            File.WriteAllText(filePathaName, jsonString);
        }
    }
}
