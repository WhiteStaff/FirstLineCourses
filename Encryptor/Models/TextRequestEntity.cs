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
            this.Text = text;
            this.Key = key;
            this.IsEncrypted = isEncrypted;
            this.Result = result;
        }

        public TextRequestEntity()
        {

        }
    }
}