using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Excepciones
{
    public class LoginException : Exception
    {
        public LoginException(string mensaje) : base(mensaje) { }
    }
}
