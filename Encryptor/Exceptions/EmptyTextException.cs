using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Encryptor.Exceptions
{
    public class EmptyTextException : CustomEncryptException
    {
        public EmptyTextException() : base("Текст не может быть пустым")
        {
        }
    }
}