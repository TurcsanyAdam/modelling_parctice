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
                Initializer initializer = new Initializer();
                Login login = new Login(initializer);
                Serializer serializer = new Serializer(initializer);
                MenuHandle menu = new MenuHandle(logger,initializer,login,serializer);
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
             

        }

    }
}
