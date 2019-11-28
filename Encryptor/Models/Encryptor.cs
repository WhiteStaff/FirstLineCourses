using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace Encryptor.Models
{
    public static class Encryptor
    {

        public static string Encrypt(string text, string key)
        {
            string alphabet = "абвгдеёжзиёкламопрстуфхцчшщъыьэюя";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                var keySymbolNumber = alphabet.IndexOf(key[i % key.Length]);
                var symbol = text[i];
                if (alphabet.Contains(symbol))
                {
                    var newSymbolNumber = (alphabet.IndexOf(symbol) + keySymbolNumber) % alphabet.Length;
                    result.Append(alphabet[newSymbolNumber]);
                }
            }
            return result.ToString();
        }
    }
}