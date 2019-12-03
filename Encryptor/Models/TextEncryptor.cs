using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.WebPages;
using Encryptor.Exceptions;

namespace Encryptor.Models
{
    public class TextEncryptor
    {
        private string _key;
        private readonly bool _isEncrypted;
        private const string Alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        private static readonly string BigAlphabet = Alphabet.ToUpper();

        public TextEncryptor(string key, bool isEncrypted)
        {
            _key = key.ToLower();
            _isEncrypted = isEncrypted;
        }

        private string Encrypt(string text)
        {
            StringBuilder result = new StringBuilder();
            int counter = 0;
            if (_key == "") throw new EmptyKeyException();
            if (!IsKeyCorrect(_key)) throw new InvalidKeyException();
            foreach (var symbol in text)
            {
                var keySymbolNumber = Alphabet.IndexOf(_key[counter % _key.Length]);
                if (Alphabet.Contains(symbol) || BigAlphabet.Contains(symbol))
                {
                    counter++;
                    var currentAlphabet = GetCurrentAlphabet(symbol);

                    var newSymbolNumber = (currentAlphabet.IndexOf(symbol) + keySymbolNumber) % currentAlphabet.Length;
                    result.Append(currentAlphabet[newSymbolNumber]);
                }
                else result.Append(symbol);
            }

            return result.ToString();
        }

        private bool IsKeyCorrect(string key)
        {
            foreach (var symbol in key)
            {
                var keySymbolNumber = Alphabet.IndexOf(symbol);
                if (keySymbolNumber == -1) return false;
            }

            return true;
        }

        private string Decrypt(string text)
        {
            StringBuilder result = new StringBuilder();
            int counter = 0;
            if (_key == "") throw new EmptyKeyException();
            if (!IsKeyCorrect(_key)) throw new InvalidKeyException();
            foreach (var symbol in text)
            {
                var keySymbolNumber = Alphabet.IndexOf(_key[counter % _key.Length]);
                if (Alphabet.Contains(symbol) || BigAlphabet.Contains(symbol))
                {
                    counter++;
                    var currentAlphabet = GetCurrentAlphabet(symbol);
                    var newSymbolNumber =
                        (currentAlphabet.IndexOf(symbol) + currentAlphabet.Length - keySymbolNumber) %
                        currentAlphabet.Length;
                    result.Append(currentAlphabet[newSymbolNumber]);
                }
                else result.Append(symbol);
            }

            return result.ToString();
        }

        private static string GetCurrentAlphabet(char symbol)
        {
            return Alphabet.Contains(symbol) ? Alphabet : BigAlphabet;
        }

        public string Transform(string text)
        {
            try
            {
                return _isEncrypted ? Decrypt(text) : Encrypt(text);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}