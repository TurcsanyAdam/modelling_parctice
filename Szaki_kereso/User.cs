using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;

namespace Szaki_kereso
{
    [Serializable()]
    public class User : ISerializable
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public int Money { get; set; }

        // Initializes User
        public User(string aUsername, string aFirstName, string aLastName, int aAge, string aEmail, string aAdress)
        {
            Username = aUsername;
            FirstName = aFirstName;
            LastName = aLastName;
            Age = aAge;
            Email = aEmail;
            Adress = aAdress;
        }

        // Modifies money according to user input
        public void AddMoney(int moneyAmount)
        {
            Money += moneyAmount;
        }

        public override string ToString()
        {
            string result = $"{Username} ({Age}) - Current balance {Money}";
            return result;
        }

        // Helper method for serialization
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Username", Username);
            info.AddValue("First name", FirstName);
            info.AddValue("Last name", LastName);
            info.AddValue("Age", Age);
            info.AddValue("Email", Email);
            info.AddValue("Adress", Adress);
            info.AddValue("Money", Money);
        }
        public User()
        {

        }
        // Helper method for serialization
        public User(SerializationInfo info, StreamingContext context)
        {
            Username = (string)info.GetValue("User Name", typeof(string));
            FirstName = (string)info.GetValue("First Name", typeof(string));
            LastName = (string)info.GetValue("Last name", typeof(string));
            Age = (int)info.GetValue("Age", typeof(int));
            Email = (string)info.GetValue("Email", typeof(string));
            Adress = (string)info.GetValue("Adress", typeof(string));
            Money = (int)info.GetValue("Money", typeof(int));
        }


    }
}
