using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Szaki_kereso
{
    public class Serializer
    {
        public string filepathUser = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "User_data.xml");
        public string filepathHandyman = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Handyman.xml");


        public void SaveData(Login login)
        {
            SerializeUser(login.UserList);
            SerializeHandyMen(login.HandymanList);
        }

        public void DeserializerUser(Login login)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));

            if (File.Exists(filepathUser) && new FileInfo(filepathUser).Length>0)
            {
                using (FileStream fs = File.OpenRead(filepathUser))
                {
                    login.UserList = (List<User>)serializer.Deserialize(fs);
                }
            }
            else
            {
                File.Create(filepathUser);
            }

        }

        public void SerializeUser(List<User> userList)
        {
            using (Stream fs = new FileStream(filepathUser, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
                serializer.Serialize(fs, userList);
            }
        }
        public void DeserializerHandyMen(Login login)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Handyman>));

            if (File.Exists(filepathHandyman) && new FileInfo(filepathHandyman).Length > 0)
            {
                using (FileStream fs = File.OpenRead(filepathHandyman))
                {
                    login.HandymanList = (List<Handyman>)serializer.Deserialize(fs);
                }
            }
            else
            {
                login.GenerateHandymanFromCsv();
                SaveData(login);
                
            }
        }

        public void SerializeHandyMen(List<Handyman> handymanList)
        {
            using (Stream fs = new FileStream(filepathHandyman, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Handyman>));
                serializer.Serialize(fs, handymanList);
            }
        }
    }
}
