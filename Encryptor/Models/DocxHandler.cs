using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Ajax.Utilities;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Paragraph = Xceed.Document.NET.Paragraph;

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

        public DocX Parse()
        {
            var doc = DocX.Load(_docxStream);
            doc.Sections.ForEach(x => x.SectionParagraphs.ForEach(ParseParagraph));
            _docxStream.Close();

            return doc;
        }

        private void ParseParagraph(Paragraph paragraph)
        {
            var text = paragraph.Text;
            if (string.IsNullOrEmpty(text)) return;
            var newText = _encryptor.Transform(text);
            Regex reg = new Regex("[а-яА-ЯЁё]");
            try
            {
                paragraph.ReplaceText(text, newText);
            }
            catch (ArgumentException e)
            {
                var formula = paragraph.Xml.FirstNode;
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
        }
    }
}