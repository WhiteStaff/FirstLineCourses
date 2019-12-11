using System.IO;
using DocumentFormat.OpenXml.Packaging;
using Encryptor.Models;
using NUnit.Framework;

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
