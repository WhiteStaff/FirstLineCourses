﻿using System;
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
        private readonly string _text;
        private byte[] _result;
        public DocxCreator(string text)
        {
            _text = text;
        }

        public byte[] Create()
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
                    document.MainDocumentPart.Document.Save();
                }

                _result = stream.ToArray();
            }

            return _result;
        }
    }
}