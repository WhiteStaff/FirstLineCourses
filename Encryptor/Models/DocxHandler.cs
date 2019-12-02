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
        private readonly Encryptor _encryptor;
        


        public DocxHandler(Stream docxStream, string key, bool isEncrypted)
        {
            _docxStream = docxStream;
            _encryptor = new Encryptor(key, isEncrypted);
        }


        public byte[] Parse()
        {
            byte[] a;
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

                a = stream.ToArray();
            }

            return a;

            //var doc = DocX.Load(_docxStream);
            //doc.Sections.ForEach(x => x.SectionParagraphs.ForEach(ParseParagraph));
            //_docxStream.Close();

            //return doc;
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


        /*private void ParseParagraph(Paragraph paragraph)
        {
            var text = paragraph.Text;
            if (string.IsNullOrEmpty(text)) return;
            var newText = _encryptor.Transform(text);
            if (paragraph.Xml.Name.LocalName == "p" && mock == null) mock = paragraph.Xml.Name;
            Regex reg = new Regex("[а-яА-ЯЁё]");
            try
            {
                //paragraph.ReplaceText(text, newText);
                paragraph.InsertText(newText);
                //paragraph.RemoveText(0, paragraph.Text.Length);



            }
            catch (ArgumentException e)
            {
                var a = paragraph.Xml.Name;
                paragraph.Xml.Name = mock;
                paragraph.InsertText(newText);
                paragraph.Xml.Name = a;
                /*var formula = paragraph.Xml.FirstNode;
                List<string> list = new List<string>();
                while (formula != null)
                {
                    if ((formula as XElement)?.Name.NamespaceName ==
                        "http://schemas.openxmlformats.org/officeDocument/2006/math")
                    {
                        var curr = (formula as XElement)?.Value ?? "";
                        list.Add(curr);
                    }

                    formula = formula.NextNode;
                }

                var texts = text.Split(list.ToArray(), System.StringSplitOptions.None);
                foreach (var item in texts)
                {
                    if (!reg.IsMatch(item)) continue;
                    newText = _encryptor.Transform(item);
                    paragraph.ReplaceText(item, newText);
                }
            }
        }*/
    }
}