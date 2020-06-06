using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UserLogin
{
    public static class Logger
    {
        private static List<string> currentSessionActivities = new List<string>();

        public static string FileName
        {
            get { return "..\\..\\..\\..\\UserLogin\\log.txt"; }
            private set { }
        }

        public static void LogActivity(string activity)
        {
            string str = DateTime.Now + ";" +
                         LoginValidation.CurrentUserUsername + ";" +
                         LoginValidation.CurrentRole + ";" +
                         activity;
            currentSessionActivities.Add(str);
            //Console.WriteLine(FileName);
            File.AppendAllLines(FileName, new[] { str });

        }

        public static string GetCurrentSessionActivities()
        {
            StringBuilder s = new StringBuilder("\n");
            currentSessionActivities.ForEach(i => s.Append(i + "\n"));
            return s.ToString();
        }
    }
}
