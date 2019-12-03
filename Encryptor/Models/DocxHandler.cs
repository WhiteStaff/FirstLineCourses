using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Ajax.Utilities;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;

namespace Encryptor.Models
{
    public class DocxHandler
    {
        private readonly Stream _docxStream;
        private readonly TextEncryptor _encryptor;
        


        public DocxHandler(Stream docxStream, string key, bool isEncrypted)
        {
            _docxStream = docxStream;
            _encryptor = new TextEncryptor(key, isEncrypted);
        }


        public byte[] Parse()
        {
            byte[] resultArray;
            using (var stream = new MemoryStream())
            {
                var bytes = GetStreamBytes(_docxStream);
                stream.Write(bytes, 0, bytes.Length);

                using (WordprocessingDocument doc =
                    WordprocessingDocument.Open(stream, true))
                {
                    string docText = null;
                    using (StreamReader sr = new StreamReader(doc.MainDocumentPart.GetStream()))
                        docText = sr.ReadToEnd();

                    docText = TransformText(docText);

                    using (StreamWriter sw = new StreamWriter(doc.MainDocumentPart.GetStream(FileMode.Create)))
                        sw.Write(docText);
                }

                resultArray = stream.ToArray();
            }

            return resultArray;
        }

        private byte[] GetStreamBytes(Stream stream)
        {
            using (var resultStream = new MemoryStream())
            {
                stream.CopyTo(resultStream);
                return resultStream.ToArray();
            }
        }

        private string TransformText(string text) => _encryptor.Transform(text);
        
    }
}