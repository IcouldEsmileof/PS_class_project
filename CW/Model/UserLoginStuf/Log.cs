namespace CW
{
    public class Log
    {
        public int LogId { get; set; }
        public string Activity { get; set; }

        public Log()
        {
        }

        public Log(string activity)
        {
            Activity = activity;
        }
    }
}
