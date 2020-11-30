using System;
using System.IO;
using ETLService.Option;
namespace Utilities
{
    class Logger
    {
        public static LoggerOptions options = new LoggerOptions();

        public static bool isEnabled { get; set; } = true;

        public static void Log(string message)
        {
            if (isEnabled)
            {
                using (StreamWriter sw = new StreamWriter(options.Path, true))
                {
                    sw.WriteLine($"[{DateTime.Now:hh:mm:ss dd.MM.yyyy}] - {message}");
                }
            }
        }
    }
}
