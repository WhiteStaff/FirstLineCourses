using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Ajax.Utilities;
namespace Encryptor.Models
{
    public class DocxCreator
    {
        private string _text;
        private byte[] result;
        public DocxCreator(string text)
        {
            _text = text;
        }



        public  void Create(HttpContext context)
        {
            using (var stream  =new MemoryStream())
            {
                using (WordprocessingDocument document =
                    WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = document.AddMainDocumentPart();
                    mainPart.Document = new Document(
                        new Body(
                            new Paragraph(
                                new Run(
                                    new Text(_text)))));
                }

                context.Response.AppendHeader("Content-Disposition", String.Format("attachment;filename=\"{0}.docx\"", "123"));
                stream.Position = 0;
                stream.CopyTo(context.Response.OutputStream);
                context.Response.Flush();
                context.Response.End();
            }

            

        }
    }
}