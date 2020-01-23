using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Szaki_kereso
{
    public class FileHandlingTXT : FileHandling
    {
        public override void WriteToFile(Dictionary<Handyman, double> handymenInRadius)
        {
            string path = Path.Combine(filePath, "HandymanInRadius.txt");
            File.WriteAllText(path, "");

            foreach (KeyValuePair<Handyman, double> kvp in handymenInRadius)
            {
                string result = $"{kvp.Key} - {kvp.Value} km\n";
                File.AppendAllText(path, result);
            }

        }
    }
}
