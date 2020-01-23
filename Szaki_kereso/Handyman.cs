using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Szaki_kereso
{
    [Serializable()]
    public class Handyman: ISerializable
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Specialization { get; set; }
        public int Money { get; set; }
        public Handyman(string aUsername, string aFirstName, string aLastName, int aAge, string aEmail, string aAdress, string aSpecialization)
        {
            Username = aUsername;
            FirstName = aFirstName;
            LastName = aLastName;
            Age = aAge;
            Email = aEmail;
            Adress = aAdress;
            Specialization = aSpecialization;
        }

        public void Work(User user, int workingFee)
        {
            if(workingFee <= user.Money)
            {
                user.Money -= workingFee;
                Money += workingFee;
            }
            else
            {
                throw new ArgumentException("Not enough money for this job!");
            }

        }


        public override string ToString()
        {
            string result = $"{Username} - {FirstName} {LastName} ({Age}) Email adress: {Email} - {Specialization}";
            return result;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Username", Username);
            info.AddValue("First name", FirstName);
            info.AddValue("Last name", LastName);
            info.AddValue("Age", Age);
            info.AddValue("Email", Email);
            info.AddValue("Adress", Adress);
            info.AddValue("Specialization", Specialization);
            info.AddValue("Money", Money);
        }
        public Handyman()
        {

        }
        public Handyman(SerializationInfo info, StreamingContext context)
        {
            Username = (string)info.GetValue("User Name", typeof(string));
            FirstName = (string)info.GetValue("First Name", typeof(string));
            LastName = (string)info.GetValue("Last name", typeof(string));
            Age = (int)info.GetValue("Age", typeof(int));
            Email = (string)info.GetValue("Email", typeof(string));
            Adress = (string)info.GetValue("Adress", typeof(string));
            Specialization = (string)info.GetValue("Specialization", typeof(string));
            Money = (int)info.GetValue("Money", typeof(int));
        }
    }
}
