using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace EncryptorTests
{
    [TestFixture]
    public class TextTest
    {
        [TestCase("123", "фыв")]
        public void CorrectlyEncrypt(string text, string key)
        {

        }
    }
}
