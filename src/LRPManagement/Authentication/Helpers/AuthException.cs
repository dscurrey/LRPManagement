using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Helpers
{
    public class AuthException : Exception
    {
        public AuthException() : base()
        {
        }

        public AuthException(string msg) : base(msg)
        {
        }

        public AuthException(string msg, params object[] args) : base
            (String.Format(CultureInfo.CurrentCulture, msg, args))
        {
        }
    }
}
