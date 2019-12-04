using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.Ajax.Utilities;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

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
                   doc.MainDocumentPart.Document.Body.Elements<Paragraph>().ForEach(Check);
                }

                resultArray = stream.ToArray();
            }

            return resultArray;
        }

        private void Check(Paragraph element)
        {
            var c = element;
            var x = c.Elements().First().Elements().First().Elements().First().Elements<DocumentFormat.OpenXml.Math.Text>();
            foreach (var item in x)
            {
                item.Text = "1212";
                var w = item;
                var z = w;
            }
            var t = x;
            element.Elements().ForEach(q=>q.Elements<OfficeMath>().ForEach(y=>y.Elements<Run>().ForEach(u=>u.Elements<Text>().ForEach(AAA))));
            //var t = z;
            //x.Elements<Run>().ForEach(c=>c.Elements<Text>().ForEach(q=>AAA(q, x)));
        }

        private void AAA(Text text)
        {
            text.Text = "1212";
            
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