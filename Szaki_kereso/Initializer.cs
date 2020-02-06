using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Szaki_kereso
{
    public class Initializer
    {
        public List<User> UserList = new List<User>();

        public List<Handyman> HandymanList = new List<Handyman>();
        public IReadOnlyList<Handyman> handymanList { get { return HandymanList; } }
        public readonly Dictionary<string, string> LoginInfo = new Dictionary<string, string>();
        public IReadOnlyDictionary<string, string> loginInfo { get { return LoginInfo; } }
        public Initializer()
        {
            GenerateUserPasswordPairs();
        }
        public void GenerateUserPasswordPairs()
        {
            string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Login_details.csv");

            if (File.Exists(filepath) && new FileInfo(filepath).Length > 0)
            {
                using (var reader = new StreamReader(filepath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        LoginInfo.Add(values[0], values[1]);
                    }

                }
            }

        }

        public void GenerateHandymanFromCsv()
        {
            using (var reader = new StreamReader(@"..\\..\\..\\Handyman_details.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    Handyman handyman = new Handyman(values[0], values[1], values[2], int.Parse(values[3]), values[4], values[5], values[6]);
                    HandymanList.Add(handyman);
                }

            }
        }
    }
}
