using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Encryptor.Exceptions
{
    public class CustomEncryptException : Exception
    {
        public CustomEncryptException(string message):base(message)
        { }
    }
}