using System;
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
        [TestCase("Files/Encrypted/", "Result_v5.docx", TestName = "Документ с обычным текстом")]
        [TestCase("Files/Encrypted/", "dgfsdfg.docx", TestName = "Документ с формулами")]
        [TestCase("Files/Encrypted/", "lab_3.docx", TestName = "Документ с таблицей")]
        public void CorrectlyEncryptAndDecryptWithStructureAndStylesSaving(string path, string fileName)
        {
            string solution_dir = Path.GetDirectoryName(Path.GetDirectoryName(
                TestContext.CurrentContext.TestDirectory));
            var allPath = $"{solution_dir}/{path}{fileName}";
            var newPath = $"{solution_dir}/Files/Encrypted/Result_v5_1.docx";
            Body originalText;
            Body docText;
            byte[] result2;
            byte[] result1;

            using (WordprocessingDocument doc =
                WordprocessingDocument.Open(allPath, true))
            {
                originalText = doc.MainDocumentPart.Document.Body;
            }


            using (var x = File.OpenRead(allPath))
            {
                result1 = new DocxHandler(x, "скорпион", true).Parse();
            }


            using (var stream = new MemoryStream(result1))
            {
                result2 = new DocxHandler(stream, "скорпион", false).Parse();
            }

            File.WriteAllBytes(newPath, result2);

            using (WordprocessingDocument doc =
                WordprocessingDocument.Open(newPath, true))
            {
                docText = doc.MainDocumentPart.Document.Body;
            }

            Assert.AreEqual(docText, originalText);
        }
    }
}