namespace Daily
{
    public class Interval
    {
        public DateTime Init { get; private set; }
        public DateTime? End { get; private set; }
        public bool IsOpen => End == null;
        public bool IsClose => !IsOpen;

        public Interval()
        {
            Init = DateTime.Now;
        }

        internal void Stop()
        {
            End = DateTime.Now;
        }
        public double TotalSeconds()
        {
            if (IsClose)
                return (End.Value - Init).TotalSeconds;
            return 0;
        }
        public double Hours()
        {
            if (IsClose)
                return (End.Value - Init).Hours;
            return 0;
        }

        public double Minutes()
        {
            if (IsClose)
                return (End.Value - Init).Minutes;
            return 0;
        }
        public double Seconds()
        {
            if (IsClose)
                return (End.Value - Init).Seconds;
            return 0;
        }
    }
}
