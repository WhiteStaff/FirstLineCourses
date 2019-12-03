using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Encryptor.Exceptions
{
    public class InvalidKeyException : CustomEncryptException
    {
        public InvalidKeyException():base("Введен некорректный ключ")
        { }
    }
}