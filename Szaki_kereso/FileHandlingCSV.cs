using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Szaki_kereso
{
    public class FileHandlingCSV : FileHandling
    {
        //Handles file writing to CSV
        public override void WriteToFile(Dictionary<Handyman, double> handymenInRadius)
        {
            string path = Path.Combine(filePath,"HandymanInRadius.csv");
            File.WriteAllText(path, "");
            
            foreach(KeyValuePair<Handyman,double> kvp in handymenInRadius)
            {
                string result = $"{kvp.Key};{kvp.Value} km\n";
                File.AppendAllText(path, result);
            }

        }
    }
}
