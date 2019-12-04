using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.Ajax.Utilities;


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
                    doc.MainDocumentPart.Document.Body.Descendants()
                        .ForEach(x => x.Elements<OpenXmlLeafTextElement>().ForEach(Transform));
                }

                resultArray = stream.ToArray();
            }

            return resultArray;
        }

        private void Transform(OpenXmlLeafTextElement element)
        {
            element.Text = _encryptor.Transform(element.Text);
        }

        private byte[] GetStreamBytes(Stream stream)
        {
            using (var resultStream = new MemoryStream())
            {
                stream.CopyTo(resultStream);
                return resultStream.ToArray();
            }
        }
    }
}