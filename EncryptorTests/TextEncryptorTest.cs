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
        [TestCase("123dsfg2@%^#", "фыв", "123dsfg2@%^#", TestName =
            "Символы, не входящие в русский алфавит, не шифруются")]
        [TestCase("абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ", "скорпион",
            "слруунффщушыыхььбыаггэддигзккеллрКПТТМУУШТЧЪЪФЫЫАЪЯВВЬГГЗВЖЙЙДККПЙ", TestName = "Русские буквы шифруются")]
        [TestCase("привет!!", "скорпион", "бычтфы!!", TestName = "В смешанном тексте шифруются только русские буквы")]
        public void CorrectlyEncrypt(string text, string key, string result)
        {
            var encrypted = new Encryptor.Models.TextEncryptor(key, false).Transform(text);
            Assert.AreEqual(result, encrypted);
        }

        [TestCase("абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ", "скорпион",
            "слруунффщушыыхььбыаггэддигзккеллрКПТТМУУШТЧЪЪФЫЫАЪЯВВЬГГЗВЖЙЙДККПЙ",
            TestName = "Ключ: скорпион")]
        [TestCase("абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ", "ТестыКруто",
            "тёухяпцъъчьпэяищаддбёщжитгйннкпгрТЬМУЧЧФЩМЪЬЁЦЭББЮГЦДЁПАЖККЗМАНПЩЙ", TestName = "Ключ: ТестыКруто")]
        [TestCase("абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ", "курсы",
            "кфтфяпщчщдфюьюищгбгнюзёзтгмкмчзспСЬМЦФЦБСЫЩЫЁЦАЮАКЫЕГЕПАЙЗЙФЕОМОЩЙ", TestName = "Ключ: курсы")]
        public void RussianCorrectlyEncryptWithDifferentKeys(string text, string key, string result)
        {
            var encrypted = new Encryptor.Models.TextEncryptor(key, false).Transform(text);
            Assert.AreEqual(result, encrypted);
        }

        [TestCase("123dsfg2@%^#", "фыв", "123dsfg2@%^#",
            TestName = " Символы, не входящие в русский алфавит, не расшифровываются")]
        [TestCase("абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ", "скорпион",
            "слруунффщушыыхььбыаггэддигзккеллрКПТТМУУШТЧЪЪФЫЫАЪЯВВЬГГЗВЖЙЙДККПЙ",
            TestName = "Русские буквы расшифровываются")]
        [TestCase("привет!!", "скорпион", "бычтфы!!",
            TestName = "В смешанном тексте расшифровываются только русские буквы ")]
        public void CorrectlyDecrypt(string result, string key, string text)
        {
            var decrypted = new Encryptor.Models.TextEncryptor(key, true).Transform(text);
            Assert.AreEqual(result, decrypted);
        }

        [TestCase("123dsfg2@%^#", "фыв", TestName =
            "Получаем исходный текст, состоящий из символов, не входящих в русский алфавит")]
        [TestCase("Исходный текст", "скорпион", TestName = "Получаем исходный текст, состоящий только из русских букв")]
        [TestCase("Это - исходный текст!! Vot tak vot!", "фыв", TestName = "Получаем смешанный исходный текст")]
        public void CorrectlyDecryptAndEncrypt(string text, string key)
        {
            var encryptText = new Encryptor.Models.TextEncryptor(key, false).Transform(text);
            var decryptText = new Encryptor.Models.TextEncryptor(key, true).Transform(encryptText);
            Assert.AreEqual(decryptText, text);
        }


        [TestCase("123", "", typeof(EmptyKeyException), TestName = "Пустой ключ")]
        [TestCase("asd", "фы12в", typeof(InvalidKeyException), 
            TestName = "Ключ с символами, не входящими в русский алфавит")]
        [TestCase("", "фыв", typeof(EmptyTextException), TestName = "Пустой текст")]
        public void CorrectlyThrowExceptions(string text, string key, Type ex)
        {
            var encryptor = new Encryptor.Models.TextEncryptor(key, false);
            Assert.Throws(ex, () => encryptor.Transform(text));
        }

        [TestCase("Тест для шифровки", "скОрПиоН", "скорпион", false , TestName = "Шифровка с одинаковым ключом в разных регистрах")]
        [TestCase("Тест для расшифровки", "КЛЮЮЮч", "клюююЧ", true , TestName = "Расшифровка с одинаковым ключом в разных регистрах")]
        public void KeyRegisterNoMatter(string text, string firstKey, string secondKey, bool isEncrypted)
        {
            var withFirstKey = new Encryptor.Models.TextEncryptor(firstKey, isEncrypted).Transform(text);
            var withSecondKey = new Encryptor.Models.TextEncryptor(secondKey, isEncrypted).Transform(text);
            Assert.AreEqual(withFirstKey, withSecondKey);
        }

    }
}