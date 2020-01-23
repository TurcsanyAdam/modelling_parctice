using System;
using System.Collections.Generic;
using System.Text;
using Szaki_kereso;

namespace Szaki_kereso_console
{
    public class MenuHandle
    {
        ILogger logger;
        public MenuHandle(ILogger logger)
        {
            this.logger = logger;
        }
        public void HandleMenu(Login login, Serializer serializer)
        {
            while (true)
            {
                Console.Clear();
                string menu =
                    "1 - Login\n" +
                    "2 - Register\n" +
                    "3 - Save Data\n" +
                    "4 - Exit programme";
                Console.WriteLine(menu);
                Console.Write("Enter a number to navigate the menu: ");
                int userChocie = int.Parse(Console.ReadLine());



                switch (userChocie)
                {
                    case 1:
                        string username = login.UserLogin(login.loginInfo);
                        User user = (login.UserList).Find(x => x.Username == username);
                        LoginMenu(login, user, serializer);
                        break;
                    case 2:
                        login.RegisterUser();
                        serializer.SaveData(login);
                        break;
                    case 3:
                        serializer.SaveData(login);
                        break;
                    case 4:
                        serializer.SaveData(login);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Not a valid input");
                        break;


                }
            }


        }
        public void LoginMenu(Login login, User user, Serializer serializer)
        {


            while (true)
            {
                Console.Clear();
                ILogger logger = new ConsoleLogger();
                string menu =
                "1 - Find closest handyman\n" +
                "2 - Find handymen in radius\n" +
                "3 - Search handyman by username\n" +
                "4 - Search handyman by profession\n" +
                "5 - Top up balance\n" +
                "6 - Update account information\n" +
                "7 - Back to main menu";
                Console.WriteLine(menu);
                Console.Write("Enter a number to navigate the menu: ");
                int userChocie = int.Parse(Console.ReadLine());



                switch (userChocie)
                {
                    case 1:
                        DistanceProcess distance = new DistanceProcess();
                        var a = distance.GetClosestHandyman(user, login);
                        a.Wait();
                        logger.Info("Work done! Transaction complete.  Press ENTER to proceed!");
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("Enter radius to search for handymen: ");
                        double radius = int.Parse(Console.ReadLine());
                        DistanceProcess distance1 = new DistanceProcess();
                        var b = distance1.GetHandymanInRadius(user, login, radius);
                        b.Wait();
                        Dictionary<Handyman, double> c = new Dictionary<Handyman, double>();
                        foreach (var aDict in b.Result)
                        {
                            c.Add(aDict.Key, aDict.Value);
                        }
                        WriteHandymanInRadiusToFile(c);
                        logger.Info("Data added to file! Press ENTER to proceed!");
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.Write("Enter username of handymen: ");
                        string handymanUsername = Console.ReadLine();
                        DistanceProcess distance2 = new DistanceProcess();
                        _ = distance2.GetHandymanByUsername(user, login, handymanUsername);

                        break;
                    case 4:
                        break;
                    case 5:
                        Console.Write("Enter amount to upload to your balance: ");
                        int topUp = int.Parse(Console.ReadLine());
                        user.AddMoney(topUp);
                        serializer.SaveData(login);
                        break;
                    case 6:
                        UpdateAccountInformation(user);
                        break;
                    case 7:
                        HandleMenu(login, serializer);
                        break;
                    default:
                        Console.WriteLine("Not a valid input");
                        break;

                }
            }

        }

        public void UpdateAccountInformation(User user)
        {
            Console.Clear();

            string menu =                    
                    "1 - Update first name\n" +
                    "2 - Update last name\n" +
                    "3 - Update age\n" +
                    "4 - Update email\n" +
                    "5 - Update adress\n";
            Console.WriteLine(menu);
            Console.Write("Enter a number to navigate the menu: ");
            int userChocie = int.Parse(Console.ReadLine());

            switch (userChocie)
            {

                case 1:
                    Console.Write("Enter your new first name here: ");
                    string newFirstName = Console.ReadLine();
                    Console.WriteLine("Succesfully modified");
                    user.FirstName = newFirstName;
                    logger.Info("Data modified successfully! Press ENTER to proceed!");
                    Console.ReadLine();
                    break;
                case 2:
                    Console.Write("Enter your new last name here: ");
                    string newLastName = Console.ReadLine();
                    Console.WriteLine("Succesfully modified");
                    user.LastName = newLastName;
                    logger.Info("Data modified successfully! Press ENTER to proceed!");
                    Console.ReadLine();
                    break;
                case 3:
                    Console.Write("Enter your new age here: ");
                    int newAge = int.Parse(Console.ReadLine());
                    Console.WriteLine("Succesfully modified");
                    user.Age = newAge;
                    logger.Info("Data modified successfully! Press ENTER to proceed!");
                    Console.ReadLine();
                    break;
                case 4:
                    Console.Write("Enter your new email here: ");
                    string newEmail = Console.ReadLine();
                    user.Email = newEmail;
                    logger.Info("Data modified successfully! Press ENTER to proceed!");
                    Console.ReadLine();
                    
                    break;
                case 5:
                    Console.Write("Enter your new city here: ");
                    string newCity = Console.ReadLine();
                    Console.Write("Enter your new street here: ");
                    string newStreet = Console.ReadLine();
                    Console.Write("Enter your new house number here: ");
                    int newHouse = int.Parse(Console.ReadLine());
                    string adress = $"{newHouse}+{newStreet.Replace(" ", "")}+{newCity}";
                    user.Adress = adress;
                    logger.Info("Data modified successfully! Press ENTER to proceed!");
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Not a valid input");
                    break;


            }
        }

        public void WriteHandymanInRadiusToFile(Dictionary<Handyman,double> handymanInRadius)
        {
            string menu =
            "1 - Write to CSV\n" +
            "2 - Write to TXT\n" +
            "3 - Write to Console\n";
            Console.WriteLine(menu);
            Console.Write("Enter a number to navigate the menu: ");
            int userChocie = int.Parse(Console.ReadLine());

            switch (userChocie)
            {
                case 1:
                    FileHandlingCSV fileHandlingCSV = new FileHandlingCSV();
                    fileHandlingCSV.WriteToFile(handymanInRadius);
                    break;
                case 2:
                    FileHandlingTXT fileHandlingTXT = new FileHandlingTXT();
                    fileHandlingTXT.WriteToFile(handymanInRadius);
                    break;
                case 3:
                    foreach(var kvp in handymanInRadius)
                    {
                        Console.WriteLine($"{kvp.Key} - {kvp.Value}");
                    }
                    break;
            }
        }
    }
}
