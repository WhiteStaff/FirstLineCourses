using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Encryptor.Models
{
    public class TextRequest
    {
        public string Text { get;}
        public string Key { get;}
        public bool IsEncrypted { get;}
        public string Result { get;}
        

        public TextRequest(string text, string key, bool isEncrypted, string result)
        {
            Text = text;
            Key = key;
            IsEncrypted = isEncrypted;
            Result = result;
        }
    }
}