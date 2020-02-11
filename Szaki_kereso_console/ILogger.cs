using System;
using System.Collections.Generic;
using System.Text;

namespace Szaki_kereso_console
{
    // Allows logging info and error messages
    public interface ILogger
    {
        void Info(string message);
        void Error(string message);
    }
}
