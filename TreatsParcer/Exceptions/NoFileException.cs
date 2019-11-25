using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreatsParcer.Exceptions
{
    class NoFileException : Exception
    {
        public NoFileException() : base("Нет файла")
        {

        }
    }
}
