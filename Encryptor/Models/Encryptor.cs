using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.WebPages;

namespace Encryptor.Models
{
    public  class Encryptor
    {

        private string Encrypt(string text, string key)
        {
            string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            StringBuilder result = new StringBuilder();
            int counter = 0;
            for (int i = 0; i < text.Length; i++)
            {
                var keySymbolNumber = alphabet.IndexOf(key[counter % key.Length]);
                var symbol = text[i];
                if (alphabet.Contains(symbol))
                {
                    counter++;
                    var newSymbolNumber = (alphabet.IndexOf(symbol) + keySymbolNumber) % alphabet.Length;
                    result.Append(alphabet[newSymbolNumber]);
                }
                else result.Append(text[i]);
            }
            return result.ToString();
        }

        private string Decrypt(string text, string key)
        {
            string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            StringBuilder result = new StringBuilder();
            int counter = 0;
            for (int i = 0; i < text.Length; i++)
            {
                var keySymbolNumber = alphabet.IndexOf(key[counter % key.Length]);
                var symbol = text[i];
                if (alphabet.Contains(symbol))
                {
                    counter++;
                    var newSymbolNumber = (alphabet.IndexOf(symbol) + alphabet.Length - keySymbolNumber) % alphabet.Length;
                    result.Append(alphabet[newSymbolNumber]);
                }
                else result.Append(text[i]);
            }
            return result.ToString();
        }

        public string Encrypt(string text, string key, bool isEncrypted)
        {
            return isEncrypted ? Decrypt(text, key) : Encrypt(text, key);
        }
    }
}