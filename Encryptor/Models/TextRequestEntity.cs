using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Encryptor.Models
{
    [Serializable]
    public class TextRequestEntity
    {
        public string Text { get;}
        public string Key { get;}
        public bool IsEncrypted { get;}
        public string Result { get;}
        

        public TextRequestEntity(string text, string key, bool isEncrypted, string result)
        {
            Text = text;
            Key = key;
            IsEncrypted = isEncrypted;
            Result = result;
        }
    }
}