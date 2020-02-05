using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Szaki_kereso;

namespace Szaki_kereso_console
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = new ConsoleLogger();
            try
            {
                ApiHelper.InitializeClient();
                Login login = new Login();
                Serializer serializer = new Serializer();

                serializer.DeserializerHandyMen(login);
                serializer.DeserializerUser(login);
                MenuHandle menu = new MenuHandle(logger,login,serializer);
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
             

        }

    }
}
