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

        public NoMoneyForWorkException(string name)
            : base($"Not enought money for this job to work with {name}")
        {

        }
    }
}
