﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreatsParser.Exceptions
{
    class NoFileException : Exception
    {
        public NoFileException() : base("Отсутствует файл базы данных угроз.")
        {

        }
    }
}
