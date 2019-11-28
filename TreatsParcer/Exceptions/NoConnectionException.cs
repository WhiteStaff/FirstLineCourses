using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreatsParser.Exceptions
{
    class NoConnectionException : Exception
    {
        public NoConnectionException() : base("Проблемы с соединением к серверу.")
        { }
    }
}
