using daily.domain.Models.Daily;
using System.Text.Json;

namespace daily.application.Services
{
    public class DailyServices : IDailyServices
    {
        public DailyTask Get()
        {

            DailyTask today = Get(DateTime.Now);
            return today;

        }

        public IList<DailyTask> GetWeek()
        {
            DailyTask today = Get(DateTime.Now);
            DailyTask yesterday = Get(DateTime.Now.AddDays(-1));
            DailyTask theDayBeforeYesterday = Get(DateTime.Now.AddDays(-2));

            IList<DailyTask> dailyTasks = new List<DailyTask>();
            dailyTasks.Add(today);
            dailyTasks.Add(yesterday);
            dailyTasks.Add(theDayBeforeYesterday);


            return dailyTasks;
        }
        public DailyTask Get(DateTime date)
        {


            string jsonString = File.ReadAllText(@"c:\temp\test.json");
            DailyTask mainTask = JsonSerializer.Deserialize<DailyTask>(jsonString);
            return mainTask;
            /*DailyTask dailyTask = new DailyTask(date.ToString("dd/MM/yyyy"), date.ToString("dd/MM/yyyy"));

            DailyTask userTEC_00001 = new DailyTask("TEC_00001", $"TEC_00001 task description {date.ToString("dd/MM/yyyy")}");
            DailyTask userTEC_00001_sub1 = new DailyTask("TEC_00001 1", $"Pruebas TEC_00001 {date.ToString("dd/MM/yyyy")}");
            DailyTask userTEC_00001_sub2 = new DailyTask("TEC_00001 2", $"Integrar staging {date.ToString("dd/MM/yyyy")}");
            DailyTask userTEC_00001_sub3 = new DailyTask("TEC_00001 3", $"Probar DEMO {date.ToString("dd/MM/yyyy")}");
            DailyTask userTEC_00001_sub4 = new DailyTask("TEC_00001 4", $"Integrar DEVELOP {date.ToString("dd/MM/yyyy")}");
            DailyTask userTEC_00001_sub5 = new DailyTask("TEC_00001 5", $"Probar PRE {date.ToString("dd/MM/yyyy")}");

            DailyTask userTEC_00002 = new DailyTask("TEC_00002", $"TEC_00002 task description {date.ToString("dd/MM/yyyy")}");
            DailyTask userTEC_00002_sub1 = new DailyTask("TEC_00002 1", $"Pruebas TEC_00002 {date.ToString("dd/MM/yyyy")}");
            DailyTask userTEC_00002_sub2 = new DailyTask("TEC_00002 2", $"Integrar staging {date.ToString("dd/MM/yyyy")}");
            DailyTask userTEC_00002_sub3 = new DailyTask("TEC_00002 3", $"Probar DEMO {date.ToString("dd/MM/yyyy")}");
            DailyTask userTEC_00002_sub4 = new DailyTask("TEC_00001 4", $"Integrar DEVELOP {date.ToString("dd/MM/yyyy")}");
            DailyTask userTEC_00002_sub5 = new DailyTask("TEC_00001 5", $" {date.ToString("dd/MM/yyyy")}There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.");


            userTEC_00001.AddTask(userTEC_00001_sub1);
            userTEC_00001.AddTask(userTEC_00001_sub2);
            userTEC_00001.AddTask(userTEC_00001_sub3);
            userTEC_00001.AddTask(userTEC_00001_sub4);
            userTEC_00001.AddTask(userTEC_00001_sub5);

            userTEC_00002.AddTask(userTEC_00002_sub1);
            userTEC_00002.AddTask(userTEC_00002_sub2);
            userTEC_00002.AddTask(userTEC_00002_sub3);
            userTEC_00002.AddTask(userTEC_00002_sub4);
            userTEC_00002.AddTask(userTEC_00002_sub5);

            dailyTask.AddTask(userTEC_00001);
            dailyTask.AddTask(userTEC_00002);


            return dailyTask;
            */
        }

        public void Save(DailyTask mainTask)
        {
            string jsonString = JsonSerializer.Serialize(mainTask);
            File.WriteAllText(@"c:\temp\test.json", jsonString);
        }
    }
}
