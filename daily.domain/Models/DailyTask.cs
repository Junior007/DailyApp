namespace daily.domain.Models
{
    public class DailyTask
    {
        internal delegate void TaskStartEventHandler(object sender, DailyTaskStartEventArgs e);
        internal event TaskStartEventHandler TaskStartEvent;

        public string Title { get; private set; }
        public string Description { get; private set; }
        public List<Interval> Intervals { get; private set; }
        public List<DailyTask> SubTasks { get; private set; }
        public bool IsRunning => HasIntervals && Intervals.Any(i => i.IsOpen);
        public bool HasIntervals => Intervals.Count > 0;

        public DailyTask(string title, string description):this()
        {
            Title = title;
            Description = description;
        }

        private DailyTask()
        {
            Intervals = new List<Interval>();
            SubTasks = new List<DailyTask>();
        }
        public void AddTask(DailyTask task)
        {
            //TODO : en la creación también se a de atachar todos los eventos
            task.TaskStartEvent += (sender, e) => StartUp(sender as DailyTask);
            SubTasks.Add(task);
        }

        public void AddTask(string title, string description)
        {
            DailyTask task = new DailyTask() { Title = title, Description = description };
            AddTask(task);
        }

        public void Start()
        {
            DailyTask taskRaisedEvent = null;
            StartUp(taskRaisedEvent);
        }
        public void Stop()
        {
            DailyTask taskRaisedEvent = null;
            StopDown(taskRaisedEvent);
        }

        private void StartUp(DailyTask taskRaisedEvent)
        {
            StopDown(taskRaisedEvent);

            Interval newInterval = new Interval();
            Intervals.Add(newInterval);
            TaskStartEvent?.Invoke(this, new DailyTaskStartEventArgs());
        }
        private void StopDown(DailyTask taskRaisedStartEvent)
        {
            //Stop childs
            IEnumerable<DailyTask> subTasks = SubTasks.Where(t => taskRaisedStartEvent==null || t != taskRaisedStartEvent);
            foreach (var subTask in subTasks)
            {
                subTask.Stop();
            }
            // Stop intervals
            IEnumerable<Interval> intervals = Intervals.Where(i => !i.IsClose);
            foreach (var interval in intervals)
            {
                interval.Stop();
            }

        }
    }
}