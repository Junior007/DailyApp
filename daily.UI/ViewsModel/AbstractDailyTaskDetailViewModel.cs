using daily.application.Services;
using daily.domain.Models.Daily;
using daily.UI.Views.Controls;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Input;
using daily.UI.Commands;
using System.Collections.Generic;
using System.Timers;
using System.Threading.Tasks;
using System.Reflection;

namespace daily.UI.ViewsModel
{
    internal abstract class AbstractDailyTaskDetailViewModel : AbstractViewModel
    {

        public TimeSpan Timming
        {
            get => _timming;
            set
            {
                _timming = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                _dailyTask.Title = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                _dailyTask.Description = value;
                OnPropertyChanged();
            }
        }
        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                _isRunning = value;
                OnPropertyChanged();
            }

        }
        public List<DailyTask> SubTasks
        {
            get => _dailyTask.SubTasks;
        }

        public DailyTask DailyTask
        {
            private get => _dailyTask;
            set
            {
                SetDailyTask(value);
            }
        }

        public Guid Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
        public ICommand StartStop => startStop;
        public ICommand OnButtonClickAddTask => onButtonClickAddTask;
        public ICommand OnButtonClickDeleteTask => onButtonClickDeleteTask;
        
        public delegate void DeleteTaskEventHandler(object sender, Guid id);
        public event DeleteTaskEventHandler DeleteTaskEvent;

        protected DailyTask _dailyTask;
        protected string _title;
        protected string _description;
        protected Guid _id;
        protected bool _isRunning;
        protected TimeSpan _timming = DateTime.Now - DateTime.Now;

        protected IDailyServices _dailyService;
        protected StackPanel stackPanelContainer;
        protected string Container = nameof(Container);
        protected ICommand startStop;
        protected ICommand onButtonClickAddTask;
        protected ICommand onButtonClickDeleteTask;

        protected Timer refreshTimming = new Timer(30000);

        public AbstractDailyTaskDetailViewModel(IDailyServices dailyService)
        {
            _dailyService = dailyService ?? throw new ArgumentNullException(nameof(dailyService));
            startStop = new RelayCommand(startStopAction, value => true);
            //onButtonClickAddTask = new RelayCommand(showAddTaskModal, value => true);
            onButtonClickAddTask = new RelayCommand(addTaskAction, value => true);
            onButtonClickDeleteTask = new RelayCommand(deleteTaskAction, value => true);

            refreshTimming.Enabled = true;
            refreshTimming.Elapsed += new ElapsedEventHandler((object? sender, ElapsedEventArgs e) => setTimming());

        }

        private void deleteTaskAction(object obj)
        {
            Guid id = (Guid)obj ;
            DeleteTaskEvent?.Invoke(this, id);
        }

        private void addTaskAction(object obj)
        {
            AddTask();
        }

        /*protected void showAddTaskModal(object obj)
        {
            Window window = new Window
            {
                Title = "My User Control Dialog",
                Content = new AddTaskDialogView()
            };

            window.ShowDialog();
        }*/

        protected void startStopAction(object obj)
        {
            if (DailyTask.IsRunning)
                DailyTask.Stop();
            else
                DailyTask.Start();
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);
            RefreshSubtasksViews();
        }

        protected void SetDailyTask(DailyTask value)
        {
            _dailyTask = value;
            Title = _dailyTask.Title;
            Description = _dailyTask.Description;
            Id = _dailyTask.Id;

            _dailyTask.TaskStartEvent += OnChangeTaskState;
            _dailyTask.TaskStopEvent += OnChangeTaskState;
        }
        protected void AddTask()
        {
            DailyTask subTask = new DailyTask();
            DailyTask.AddTask(subTask);
            RefreshSubtasksViews();
        }

        protected void DeleteSubTask(Guid id)
        {
            DailyTask.DeleteTask(id);
            RefreshSubtasksViews();
        }

        protected void OnChangeTaskState(object sender, Object e)
        {
            IsRunning = _dailyTask.IsRunning;
        }

        protected abstract void RefreshSubtasksViews();

        protected void setTimming()
        {//TODO -  pensar en timmer único en la vista principal

            lock (_dailyTask.Intervals)
            {
                long ticks = _dailyTask.Intervals.Where(i => i.IsClose).Select(i => (TimeSpan)(i.End - i.Init)).Sum(ts => ts.Ticks);
                if (_dailyTask.Intervals.Where(i => i.IsOpen).Any())
                {
                    var lastOpen = _dailyTask.Intervals.Where(i => i.IsOpen).Last();
                    ticks += (DateTime.Now - lastOpen.Init).Ticks;
                }
                Timming = new TimeSpan(ticks);
            }
        }
    }
}
