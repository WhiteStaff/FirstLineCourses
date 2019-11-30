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
            string bigAlphabet = alphabet.ToUpper();
            StringBuilder result = new StringBuilder();
            int counter = 0;
            for (int i = 0; i < text.Length; i++)
            {
                var keySymbolNumber = alphabet.IndexOf(key[counter % key.Length]);
                var symbol = text[i];
                if (alphabet.Contains(symbol) || bigAlphabet.Contains(symbol))
                {
                    counter++;
                    if (alphabet.Contains(symbol))
                    {
                        var newSymbolNumber = (alphabet.IndexOf(symbol) + keySymbolNumber) % alphabet.Length;
                        result.Append(alphabet[newSymbolNumber]);
                    }
                    else
                    {
                        var newSymbolNumber = (bigAlphabet.IndexOf(symbol) + keySymbolNumber) % bigAlphabet.Length;
                        result.Append(bigAlphabet[newSymbolNumber]);
                    }
                }
                else result.Append(text[i]);
            }
            return result.ToString();
        }

        private string Decrypt(string text, string key)
        {
            string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            string bigAlphabet = alphabet.ToUpper();
            StringBuilder result = new StringBuilder();
            int counter = 0;
            for (int i = 0; i < text.Length; i++)
            {
                var keySymbolNumber = alphabet.IndexOf(key[counter % key.Length]);
                var symbol = text[i];
                if (alphabet.Contains(symbol) || bigAlphabet.Contains(symbol))
                {
                    counter++;
                    if (alphabet.Contains(symbol))
                    {
                        var newSymbolNumber = (alphabet.IndexOf(symbol) + alphabet.Length - keySymbolNumber) % alphabet.Length;
                        result.Append(alphabet[newSymbolNumber]);
                    }
                    else
                    {
                        var newSymbolNumber = (bigAlphabet.IndexOf(symbol) + bigAlphabet.Length - keySymbolNumber) % bigAlphabet.Length;
                        result.Append(bigAlphabet[newSymbolNumber]);
                    }
                    
                    
                }
                else result.Append(text[i]);
            }
            return result.ToString();
        }

        public string Encrypt(string text, string key, bool isEncrypted)
        {
            try
            {
                return isEncrypted ? Decrypt(text, key) : Encrypt(text, key);
            }
            catch (Exception e)
            {
                return "ошибка";
            }
        }
    }
}