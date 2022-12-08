using daily.domain.Models.Daily;

namespace daily.application.Services
{
    public class DailyServices : IDailyServices
    {
        public DailyTask Get()
        {
            DailyTask journey = new DailyTask(DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));

            DailyTask userTEC_00001 = new DailyTask("TEC_00001", "TEC_00001 task description");
            DailyTask userTEC_00001_sub1 = new DailyTask("TEC_00001 1", "Pruebas TEC_00001");
            DailyTask userTEC_00001_sub2 = new DailyTask("TEC_00001 2", "Integrar staging");
            DailyTask userTEC_00001_sub3 = new DailyTask("TEC_00001 3", "Probar DEMO");
            DailyTask userTEC_00001_sub4 = new DailyTask("TEC_00001 4", "Integrar DEVELOP");
            DailyTask userTEC_00001_sub5 = new DailyTask("TEC_00001 5", "Probar PRE");

            DailyTask userTEC_00002 = new DailyTask("TEC_00002", "TEC_00002 task description");
            DailyTask userTEC_00002_sub1 = new DailyTask("TEC_00002 1", "Pruebas TEC_00002");
            DailyTask userTEC_00002_sub2 = new DailyTask("TEC_00002 2", "Integrar staging");
            DailyTask userTEC_00002_sub3 = new DailyTask("TEC_00002 3", "Probar DEMO");
            DailyTask userTEC_00002_sub4 = new DailyTask("TEC_00001 4", "Integrar DEVELOP");
            DailyTask userTEC_00002_sub5 = new DailyTask("TEC_00001 5", "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.");


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

            journey.AddTask(userTEC_00001);
            journey.AddTask(userTEC_00002);


            return journey;
        }

        public DailyTask Get(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
