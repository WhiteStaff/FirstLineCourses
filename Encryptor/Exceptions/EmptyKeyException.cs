using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Encryptor.Exceptions
{
    public class EmptyKeyException : CustomEncryptException
    {
        public EmptyKeyException():base("Ключ не может быть пустым")
        { }
    }
}