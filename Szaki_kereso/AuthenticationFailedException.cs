using System;
using System.Collections.Generic;
using System.Text;

namespace Szaki_kereso
{
    //Handles excpetions realated to authentication
    public class AuthenticationFailedException :Exception
    {
        public AuthenticationFailedException()
        {

        }

        public AuthenticationFailedException(string message)
            : base(message)
        {

        }
    }
}
