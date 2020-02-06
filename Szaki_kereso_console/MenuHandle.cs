using System;
using System.Collections.Generic;
using System.Text;
using Szaki_kereso;

namespace Szaki_kereso_console
{
    public class MenuHandle
    {
        ILogger logger;
        DistanceProcess distanceProcess = null;
        User currentUser;
        Initializer initializer;
        Login login;
        Serializer serializer;
        HandymanProcessor handymanProcessor;
        public MenuHandle(ILogger logger, Initializer initializer, Login login, Serializer serializer)
        {
            this.logger = logger;
            this.initializer = initializer;
            this.login = login;
            this.serializer = serializer;
            HandleMenu();
        }
        public void HandleMenu()
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
                        string username = login.UserLogin(initializer.loginInfo);
                        this.currentUser = (initializer.UserList).Find(x => x.Username == username);
                        distanceProcess = new DistanceProcess(currentUser, initializer);
                        handymanProcessor = new HandymanProcessor();
                        LoginMenu(login, serializer);
                        break;
                    case 2:
                        login.RegisterUser();
                        serializer.SaveData();
                        break;
                    case 3:
                        serializer.SaveData();
                        break;
                    case 4:
                        serializer.SaveData();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Not a valid input");
                        break;


                }
            }


        }
        public void LoginMenu(Login login, Serializer serializer)
        {

            while (true)
            {
                Console.Clear();
                ILogger logger = new ConsoleLogger();
                string menu =
                currentUser.ToString()+
                "\n1 - Find closest handyman\n" +
                "2 - Find handymen in radius\n" +
                "3 - Search handyman by username\n" +
                "4 - Search handyman by specialization\n" +
                "5 - Top up balance\n" +
                "6 - Update account information\n" +
                "7 - Back to main menu";
                Console.WriteLine(menu);
                Console.Write("Enter a number to navigate the menu: ");
                int userChocie = int.Parse(Console.ReadLine());



                switch (userChocie)
                {
                    case 1:
                        Dictionary<Handyman, double> ClosestHandyman = handymanProcessor.GetClosestHandyman(distanceProcess);
                        foreach (KeyValuePair<Handyman, double> kvp in ClosestHandyman )
                        {
                            Console.WriteLine($"{kvp.Key.Username} is {kvp.Value/1000} km away from you. Working fee: {kvp.Key.WorkingFee}");
                            Console.Write($"Do you want to work with {kvp.Key.Username} ? ");
                            string wantToWorkClosest = Console.ReadLine();
                            if(wantToWorkClosest.ToLower() == "yes")
                            {
                                kvp.Key.Work(currentUser);
                                logger.Info("Work done! Transaction complete.  Press ENTER to proceed!");

                            }
                        }
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("Enter radius to search for handymen: ");
                        bool isRadius = double.TryParse(Console.ReadLine(),out double radius);
                        if (isRadius)
                        {
                            Dictionary<Handyman, double> ClosestHandymanInRadius = handymanProcessor.GetHandymanInRadius(distanceProcess, radius);
                            WriteHandymanInRadiusToFile(ClosestHandymanInRadius);
                            logger.Info("Done! Press ENTER to proceed!");
                            Console.ReadLine();
                        }
                        else
                        {
                            throw new ArgumentException("Not a valid input");
                        }

                        break;
                    case 3:
                        Console.Write("Enter username of handymen: ");
                        string handymanUsername = Console.ReadLine();
                        Handyman handymanByUsername = handymanProcessor.GetHandymanByUsername(distanceProcess, handymanUsername); 
                        Console.Write($"Do you want to work with {handymanByUsername.Username} for working fee: {handymanByUsername.WorkingFee}? ");
                        string wantToWorkName = Console.ReadLine();
                        if(wantToWorkName.ToLower() == "yes")
                        {
                            handymanByUsername.Work(currentUser);
                            logger.Info("Work done! Transaction complete.  Press ENTER to proceed!");

                        }
                        logger.Info("Press ENTER to proceed!");
                        Console.ReadLine();
                        break;
                    case 4:
                        Console.Write("Enter specialization of handymen: ");
                        string handymanSpecialization = Console.ReadLine();
                        Dictionary<Handyman, double> handymanBySpecialization = handymanProcessor.GetHandymanBySpecialization(distanceProcess, handymanSpecialization);
                        foreach (KeyValuePair<Handyman, double> kvp in handymanBySpecialization)
                        {
                            Console.WriteLine($"{kvp.Key.Username} - {kvp.Key.Specialization}");
                        }

                        logger.Info("Press ENTER to proceed!");
                        Console.ReadLine();
                        break;
                    case 5:
                        Console.Write("Enter amount to upload to your balance: ");
                        bool topUp = int.TryParse(Console.ReadLine(), out int topup);
                        if (topUp)
                        {
                            currentUser.AddMoney(topup);
                        }
                        else
                        {
                            throw new ArgumentException("Not a valid input");
                        }

                        serializer.SaveData();
                        break;
                    case 6:
                        UpdateAccountInformation(currentUser);
                        break;
                    case 7:
                        HandleMenu();
                        break;
                    case 8:
                        foreach(KeyValuePair<Handyman,double> kvp in distanceProcess.handymenWithRadius)
                        {
                            Console.WriteLine($"{kvp.Key.Username} is {kvp.Value} km away from you");
                        }
                        Console.ReadLine();
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
