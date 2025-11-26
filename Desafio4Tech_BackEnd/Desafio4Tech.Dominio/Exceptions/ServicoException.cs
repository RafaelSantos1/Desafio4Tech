using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio4Tech.Dominio.Exceptions
{
    public class ServicoException : Exception
    {
        public string Field { get; }
        public string Error { get; }

        public ServicoException(string message, string field = null, string error = null)
            : base(message)
        {
            Field = field;
            Error = error;
        }
    }   
}
