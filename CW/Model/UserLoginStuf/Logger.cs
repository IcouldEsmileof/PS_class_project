using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW
{
    public static class Logger
    {
        private static List<string> currentSessionActivities = new List<string>();

        public static string FileName
        {
            get { return "D:\\Icould\\C#\\PS_CW\\CW\\CW\\log.txt"; }
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
            StringBuilder s = new StringBuilder();
            currentSessionActivities.ForEach(i => s.Append(i + "\n"));
            return s.ToString();
        }
    }
}
