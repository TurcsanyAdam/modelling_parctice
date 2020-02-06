using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Szaki_kereso;

namespace Szaki_kereso_console
{
    public class Login
    {
        Initializer initializer;

        public Login(Initializer initializer)
        {
            this.initializer = initializer;
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
            if (!Utility.IsValidEmail(email))
            {
                throw new ArgumentException("Not a vaild email");
            }
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
                initializer.LoginInfo.Add(username, hash);
                string loginDetails = username + "," + hash + "\n";
                File.AppendAllText(filepath, loginDetails);
                User user = new User(username, firstName, lastName, age, email, adress);
                initializer.UserList.Add(user);
                Utility.SendMail(email);


            }
            else
            {
                throw new ArgumentException("You are a robot! Access denied!");
            }
        }
    }
}

