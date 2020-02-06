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
        private string filepathUser = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "User_data.xml");
        private string filepathHandyman = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Handyman.xml");
        Initializer initializer;

        public Serializer(Initializer initializer)
        {
            this.initializer = initializer;
            DeserializerHandyMen();
            DeserializerUser();
        }

        public void SaveData()
        {
            SerializeUser();
            SerializeHandyMen();
        }

        public void DeserializerUser()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));

            if (File.Exists(filepathUser) && new FileInfo(filepathUser).Length>0)
            {
                using (FileStream fs = File.OpenRead(filepathUser))
                {
                    initializer.UserList = (List<User>)serializer.Deserialize(fs);
                }
            }
            else
            {
                File.Create(filepathUser);
            }

        }

        public void SerializeUser()
        {
            using (Stream fs = new FileStream(filepathUser, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
                serializer.Serialize(fs, initializer.UserList);
            }
        }
        public void DeserializerHandyMen()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Handyman>));

            if (File.Exists(filepathHandyman) && new FileInfo(filepathHandyman).Length > 0)
            {
                using (FileStream fs = File.OpenRead(filepathHandyman))
                {
                    initializer.HandymanList = (List<Handyman>)serializer.Deserialize(fs);
                }
            }
            else
            {
                initializer.GenerateHandymanFromCsv();
                SaveData();
                
            }
        }

        public void SerializeHandyMen()
        {
            using (Stream fs = new FileStream(filepathHandyman, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Handyman>));
                serializer.Serialize(fs, initializer.handymanList);
            }
        }
    }
}
