using System;
using Encryptor.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Assert = NUnit.Framework.Assert;

namespace EncryptorTests
{
    [TestFixture]
    public class TextTest
    {
        [TestCase("123", "фыв", "123")]
        [TestCase("asd", "фыв", "asd")]
        [TestCase("привет!!", "скорпион", "бычтфы!!")]
        [TestCase("Привет!!", "скорпион", "Бычтфы!!")]
        [TestCase("Привет, эТо Text!!", "скорпион", "Бычтфы, лАа Text!!")]
        public void CorrectlyEncrypt(string text, string key, string result)
        {
            var encrypted = new Encryptor.Models.Encryptor(key, false).Transform(text);
            Assert.AreEqual(result, encrypted);
        }

        [TestCase("123", "фыв", "123")]
        [TestCase("asd", "фыв", "asd")]
        [TestCase("привет!!", "скорпион", "бычтфы!!")]
        [TestCase("Привет!!", "скорпион", "Бычтфы!!")]
        [TestCase("Привет, эТо Text!!", "скорпион", "Бычтфы, лАа Text!!")]
        public void CorrectlyDecrypt(string result, string key, string text)
        {
            var decrypted = new Encryptor.Models.Encryptor(key, true).Transform(text);
            Assert.AreEqual(result, decrypted);
        }

        [TestCase("123", "фыв", "123")]
        [TestCase("asd", "фыв", "asd")]
        [TestCase("привет!!", "скорпион", "привет!!")]
        [TestCase("Привет!!", "скорпион", "Привет!!")]
        [TestCase("Привет, эТо Text!!", "скорпион", "Привет, эТо Text!!")]
        public void CorrectlyDecryptAndEncrypt(string text, string key, string result)
        {
            var encryptText = new Encryptor.Models.Encryptor(key, false).Transform(text);
            var decryptText = new Encryptor.Models.Encryptor(key, true).Transform(encryptText);
            Assert.AreEqual(decryptText, result);
        }

        [TestCase("123", "", typeof(EmptyKeyException))]
        [TestCase("asd", "фы12в", typeof(InvalidKeyException))]
        public void CorrectlyThrowExceptions(string text, string key, Type ex)
        {
            var encryptor = new Encryptor.Models.Encryptor(key, false);
            Assert.Throws(ex, ()=>encryptor.Transform(text));
        }
    }
}
