using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
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
        private readonly bool _isEncrypted;
        private readonly string _key;


        public DocxHandler(Stream docxStream, string key, bool isEncrypted)
        {
            _docxStream = docxStream;
            _isEncrypted = isEncrypted;
            _key = key;
        }

        public DocX Parse()
        {
            var doc = DocX.Load(_docxStream);
            foreach (var section in doc.Sections)
            {
                foreach (var paragraph in section.SectionParagraphs)
                {
                    ParseParagraph(paragraph);
                }
            }

            return doc;
        }

        private void ParseParagraph(Paragraph paragraph)
        {
            var text = paragraph.Text;
            if (string.IsNullOrEmpty(text)) return;
            var newText = new Encryptor().Encrypt(text, _key, _isEncrypted);
            try
            {
                paragraph.ReplaceText(text, newText);
            }
            catch (ArgumentException e)
            {
                var formula = ((XElement) paragraph.Xml.FirstNode).Value;
                if (text == formula) return;
                var texts = text.Split(new[] {formula}, System.StringSplitOptions.None);
                foreach (var item in texts)
                {
                    if (item == "") continue;
                    newText = new Encryptor().Encrypt(item, _key, _isEncrypted);
                    paragraph.ReplaceText(item, newText);
                }
            }
            catch (Exception e)
            {
                // ignored
            }
        }
        
    }
}