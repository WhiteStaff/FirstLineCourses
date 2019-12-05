using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using Encryptor.Models;
using NUnit.Framework;
using NUnit.Framework.Internal.Filters;


namespace EncryptorTests
{
    [TestFixture]
    class FileCreatorTest
    {
        [TestCase("123456", TestName = "Только цифры")]
        [TestCase("ыуероыувпывап", TestName = "Только русские буквы")]
        [TestCase("123asdfываываs45sdfasdfs6", TestName = "Смешанный текст")]
        public void FileCreatesWithCorrectText(string text)
        {
            var bytes = new DocxCreator(text).Create();
            string docText;
            using (var stream = new MemoryStream(bytes))
            {
                using (WordprocessingDocument doc =
                    WordprocessingDocument.Open(stream, true))
                {
                    docText = doc.MainDocumentPart.Document.Body.InnerText;
                }
            }
            Assert.AreEqual(text, docText);
        }
    }
}
