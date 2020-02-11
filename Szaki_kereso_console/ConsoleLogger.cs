using System;
using System.Collections.Generic;
using System.Text;

namespace Szaki_kereso_console
{
    // Console logging implementation for Ilogger interface
    public class ConsoleLogger : ILogger
    {
        public void Info(string message)
        {
            string info = "INFO ";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(info);
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine(DateTime.Now + " " + message);
        }

        public void Error(string message)
        {
            string info = "ERROR ";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(info);
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine(DateTime.Now + " " + message);
        }
    }
}
