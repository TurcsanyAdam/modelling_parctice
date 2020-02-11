using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Szaki_kereso
{
    // Handles exporting data to file
    public abstract class FileHandling
    {
        protected string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public abstract void WriteToFile(Dictionary<Handyman, double> handymenInRadius);

    }
}
