using daily.domain.Models.Daily;

namespace daily.application.Services
{
    public interface IDailyServices
    {
        public DailyTask Get();
        public DailyTask Get(DateTime date);
        IList<DailyTask> GetWeek();
        void Save(DailyTask dailyTask);
    }
}
