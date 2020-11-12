using System;
using System.IO;

namespace Utilities
{
    class Logger
    {
        private string path { get; }
        public bool isEnabled { get; }

        public Logger(string path, bool isEnabled)
        {
            this.path = path;
            this.isEnabled = isEnabled;
        }

        public void Log(string message)
        {
            if (isEnabled)
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine($"[{DateTime.Now:hh:mm:ss dd.MM.yyyy}] - {message}");
                }
            }
        }
    }
}
