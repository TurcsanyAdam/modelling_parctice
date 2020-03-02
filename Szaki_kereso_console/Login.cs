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

        // Initializes Login
        public Login(Initializer initializer)
        {
            this.initializer = initializer;
        }

        // Validates if user had already registered
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
                throw new AuthenticationFailedException("Invalid username of password");
            }
        }
        // Allows registartion for first-time users
        public void RegisterUser()
        {
            string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Login_details.csv");
            string username;
            string firstName;
            string lastName;
            int age;
            string city;
            string street;
            int houseNumber;
            string email;


            while (true)
            {
                Console.Write("Please enter your username here: ");
                username = Console.ReadLine();
                if (username.Length > 3)
                {
                    break;
                }
            }
            

            Console.Write("Please enter your password here: ");
            string password = "";

            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter && password.Length <= 3)
                {
                    Console.Write("\nPassword not strong enough yet\n");
                    password = "";
                    Console.Write("Please enter your again password here: ");
                }
                else if (key.Key == ConsoleKey.Enter)
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
            while (true)
            {
                Console.Write("Please enter your first name here: ");
                firstName = Console.ReadLine();
                if (firstName.Length >= 2)
                {
                    break;
                }
            }

            while (true)
            {

                Console.Write("Please enter your last name here: ");
                lastName = Console.ReadLine();
                if (lastName.Length >= 2)
                {
                    break;
                }
            }

            while (true)
            {
                Console.Write("Please enter your age here: ");
                bool isAge = int.TryParse(Console.ReadLine(), out age);
                if (isAge)
                {
                    break;
                }
            }

            while (true)
            {

                Console.Write("Please enter your email here: ");
                email = Console.ReadLine();
                if (Utility.IsValidEmail(email))
                {
                    break;
                }
            }

            while (true)
            {

                Console.Write("Please enter your city here: ");
                city = Console.ReadLine();
                if (city.Length >= 2)
                {
                    break;
                }
            }
            while (true)
            {

                Console.Write("Please enter your street name here: ");
                street = Console.ReadLine();
                if (street.Length >= 2)
                {
                    break;
                }
            }
            while (true)
            {
                Console.Write("Please enter your house number here: ");
                bool isHouseNumber = int.TryParse(Console.ReadLine(), out houseNumber);
                if (isHouseNumber)
                {
                    break;
                }
            }


            string adress = $"{Convert.ToString(houseNumber)}+{street.Replace(" ", "")}+{city}";

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
                throw new AuthenticationFailedException("You are a robot! Access denied!");
            }
        }
    }
}

