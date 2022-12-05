using daily.application.Services;
using System;

namespace daily.UI
{
    internal class PageState
    {
        private IDailyServices _dailyService;
        public PageState(IDailyServices dailyService)
        {
            _dailyService = dailyService ?? throw new ArgumentNullException(nameof(dailyService));
        }
    }
}
