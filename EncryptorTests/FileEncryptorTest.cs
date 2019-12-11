using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Encryptor.Models;
using NUnit.Framework;

namespace EncryptorTests
{
    [TestFixture]
    class FileEncryptorTest
    {
        [TestCase("Files/", "Result_v5.docx", TestName = "Документ с обычным текстом")]
        [TestCase("Files/", "formula.docx", TestName = "Документ с формулами")]
        [TestCase("Files/", "tables.docx", TestName = "Документ с таблицей")]
        public void EncryptAndDecryptWithStructureAndStylesSaving(string path, string fileName)
        {
            string solution_dir = Path.GetDirectoryName(Path.GetDirectoryName(
                TestContext.CurrentContext.TestDirectory));
            var allPath = $"{solution_dir}/{path}{fileName}";
            Body originalText;
            Body docText;
            byte[] result2;
            byte[] result1;

            using (WordprocessingDocument doc =
                WordprocessingDocument.Open(allPath, true))
            {
                originalText = doc.MainDocumentPart.Document.Body;
            }


            using (var fileStream = File.OpenRead(allPath))
            {
                result1 = new DocxHandler(fileStream, "скорпион", true).Parse();
            }


            using (var stream = new MemoryStream(result1))
            {
                result2 = new DocxHandler(stream, "скорпион", false).Parse();
            }

            using (var stream = new MemoryStream(result2))
            {
                using (WordprocessingDocument doc =
                    WordprocessingDocument.Open(stream, true))
                {
                    docText = doc.MainDocumentPart.Document.Body;
                }
            }

            Assert.AreEqual(docText, originalText);
        }
        [Test]
        public void ThrowsExceptionWithCorruptedFile()
        {
            Assert.Throws(typeof(FileFormatException), () =>
                {
                    string solution_dir = Path.GetDirectoryName(Path.GetDirectoryName(
                        TestContext.CurrentContext.TestDirectory));
                    var allPath = $"{solution_dir}/Files/Corrupted.docx";
                    using (WordprocessingDocument doc =
                        WordprocessingDocument.Open(allPath, true))
                    {
                        var originalText = doc.MainDocumentPart.Document.Body;
                    }
                }
            );
        }
    }
}