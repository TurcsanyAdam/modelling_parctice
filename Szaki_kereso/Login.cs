using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Szaki_kereso
{
    public class Login
    {

        public List<User> UserList = new List<User>();
        public IReadOnlyList<User> userList { get { return UserList; } }

        private Dictionary<string, string> LoginInfo = new Dictionary<string, string>();
        public IReadOnlyDictionary<string, string> loginInfo { get { return LoginInfo; } }

        public List<Handyman> HandymanList = new List<Handyman>();
        public IReadOnlyList<Handyman> handymanList { get { return HandymanList; } }
        public Login()
        {
            GenerateUserPasswordPairs();
        }

        public void GenerateUserPasswordPairs()
        {
            string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Login_details.csv");

            if (File.Exists(filepath) && new FileInfo(filepath).Length >0)
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
                    Handyman handyman = new Handyman(values[0],values[1],values[2],int.Parse(values[3]),values[4],values[5],values[6]);
                    HandymanList.Add(handyman);
                }

            }
        }


        public string UserLogin(IReadOnlyDictionary<string, string> loginInfo)
        {


            Console.Write("Please enter your username here: ");
            string username = Console.ReadLine();
            Console.Write("Please enter your password here: ");
            string password = "";
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                else
                {
                    Console.Write("*");
                    password += key.KeyChar;
                }
            }

            if (loginInfo[username].Equals(Utility.Hash(password)))
            {
                return username;

            }
            else
            {
                throw new ArgumentException("Invalid username of password");
            }
        }
        public void RegisterUser()
        {
            string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Login_details.csv");
            Console.Write("Please enter your username here: ");
            string username = Console.ReadLine();
            Console.Write("Please enter your password here: ");
            string password = "";

            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                else
                {
                    Console.Write("*");
                    password += key.KeyChar;

                }
            }

            string hash = Utility.Hash(password);

            Console.Write("Please enter your first name here: ");
            string firstName = Console.ReadLine();
            Console.Write("Please enter your last name here: ");
            string lastName = Console.ReadLine();
            Console.Write("Please enter your age here: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Please enter your email here: ");
            string email = Console.ReadLine();
            Console.Write("Please enter your city here: ");
            string city = Console.ReadLine();
            Console.Write("Please enter your street name here: ");
            string street = Console.ReadLine();
            Console.Write("Please enter your house number here: ");
            string houseNumber = Console.ReadLine();
            string adress = $"{houseNumber}+{street.Replace(" ", "")}+{city}";

            Console.Write("To prove that you are not a robot, enter dog in hungarian: ");
            string prove = Console.ReadLine().ToLower();
            if (prove.Equals("kutya"))
            {
                LoginInfo.Add(username, hash);
                string loginDetails = username + "," + hash + "\n";
                File.AppendAllText(filepath, loginDetails);
                User user = new User(username, firstName, lastName, age, email, adress);
                UserList.Add(user);


            }
            else
            {
                throw new ArgumentException("You are a robot! Access denied!");
            }
        }
    }
}
