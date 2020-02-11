using System;
using System.Collections.Generic;
using System.Text;

namespace Szaki_kereso
{
    //Handles excpetion if user wants to work withy handyman with lower balance then the working fee
    public class NoMoneyForWorkException:Exception
    {
        public NoMoneyForWorkException()
        {

        }

        public NoMoneyForWorkException(string message)
            :base(message)
        {

        }
    }
}
