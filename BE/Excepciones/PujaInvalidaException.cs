using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Excepciones
{
    public class PujaInvalidaException : Exception
    {
        public PujaInvalidaException(string mensaje) : base(mensaje) { }
    }
}
