using System;
using System.Collections.Generic;
using System.Text;

namespace Szaki_kereso
{
    class NotAValidAddressException : Exception
    {

        public NotAValidAddressException()
            : base("Not a valid address")
        {

        }
    }
}
