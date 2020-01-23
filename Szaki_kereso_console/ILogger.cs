using System;
using System.Collections.Generic;
using System.Text;

namespace Szaki_kereso_console
{
    public interface ILogger
    {
        void Info(string message);
        void Error(string message);
    }
}
