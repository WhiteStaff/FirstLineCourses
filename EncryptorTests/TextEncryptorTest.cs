using System;
using Encryptor.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Assert = NUnit.Framework.Assert;

namespace EncryptorTests
{
    [TestFixture]
    public class TextEncryptorTest
    {
        [TestCase("123", "фыв", "123", TestName = "Цифры не шифруются")]
        [TestCase("абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ", "скорпион", "слруунффщушыыхььбыаггэддигзккеллрКПТТМУУШТЧЪЪФЫЫАЪЯВВЬГГЗВЖЙЙДККПЙ", TestName = "Русские буквы шифруются")]
        [TestCase("привет!!", "скорпион", "бычтфы!!", TestName = "Шифруются только буквы")]
        public void CorrectlyEncrypt(string text, string key, string result)
        {
            var encrypted = new Encryptor.Models.TextEncryptor(key, false).Transform(text);
            Assert.AreEqual(result, encrypted);
        }

        [TestCase("абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ", "скорпион", "слруунффщушыыхььбыаггэддигзккеллрКПТТМУУШТЧЪЪФЫЫАЪЯВВЬГГЗВЖЙЙДККПЙ", TestName = "Ключ: скорпион")]
        [TestCase("абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ", "ТестыКруто", "тёухяпцъъчьпэяищаддбёщжитгйннкпгрТЬМУЧЧФЩМЪЬЁЦЭББЮГЦДЁПАЖККЗМАНПЩЙ", TestName = "Ключ: ТестыКруто")]
        [TestCase("абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ", "скорпион", "слруунффщушыыхььбыаггэддигзккеллрКПТТМУУШТЧЪЪФЫЫАЪЯВВЬГГЗВЖЙЙДККПЙ", TestName = "Ключ: скорпион")]
        public void RussianCorrectlyEncryptWithDifferentKeys(string text, string key, string result)
        {
            var encrypted = new Encryptor.Models.TextEncryptor(key, false).Transform(text);
            Assert.AreEqual(result, encrypted);
        }

        [TestCase("123", "фыв", "123", TestName = "Цифры не меняются")]
        [TestCase("asd!№;%:?*()", "фыв", "asd!№;%:?*()", TestName = "Не русские буквы не меняются")]
        [TestCase("привет!!", "скорпион", "бычтфы!!", TestName = "Меняются только русские буквы")]
        public void CorrectlyDecrypt(string result, string key, string text)
        {
            var decrypted = new Encryptor.Models.TextEncryptor(key, true).Transform(text);
            Assert.AreEqual(result, decrypted);
        }

        [TestCase("123", "фыв", "123", TestName = "Цифры")]
        [TestCase("asd", "фыв", "asd", TestName = "Не русские буквы")]
        [TestCase("Это тест", "фыв", "Это тест", TestName = "Текст")]
        public void CorrectlyDecryptAndEncrypt(string text, string key, string result)
        {
            var encryptText = new Encryptor.Models.TextEncryptor(key, false).Transform(text);
            var decryptText = new Encryptor.Models.TextEncryptor(key, true).Transform(encryptText);
            Assert.AreEqual(decryptText, result);
        }

        [TestCase("123", "", typeof(EmptyKeyException), TestName = "Пустой ключ")]
        [TestCase("asd", "фы12в", typeof(InvalidKeyException), TestName = "Ключ с символами кроме русского алфавита")]
        public void CorrectlyThrowExceptions(string text, string key, Type ex)
        {
            var encryptor = new Encryptor.Models.TextEncryptor(key, false);
            Assert.Throws(ex, ()=>encryptor.Transform(text));
        }
    }
}
