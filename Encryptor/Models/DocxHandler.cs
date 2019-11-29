using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Xceed.Words.NET;

namespace Encryptor.Models
{
    public class DocxHandler
    {
        private Stream _docxStream;
        private Stream _outdocxStream;
        private bool _isEncrypted;
        private string _key;
        private WordprocessingDocument _clone;

        public DocxHandler(Stream docxStream, string key, bool isEncrypted)
        {
            _docxStream = docxStream;
            _isEncrypted = isEncrypted;
            _key = key;
        }

        public WordDocument Parse()
        {
            var doc = new WordDocument(_docxStream, FormatType.Docx);
            foreach (WSection section in doc.Sections)
            {
                WTextBody sectionBody = section.Body;
                IterateTextBody(sectionBody);
            }

            var a = new MemoryStream();
            //doc.Save("11212.txt", FormatType.Docx, Response, HttpContentDisposition.Attachment);

            return doc;
        }

        private void IterateTextBody(WTextBody textBody)
        {
            //Iterates through each of the child items of WTextBody
            for (int i = 0; i < textBody.ChildEntities.Count; i++)
            {
                //IEntity is the basic unit in DocIO DOM. 
                //Accesses the body items (should be either paragraph or table) as IEntity
                IEntity bodyItemEntity = textBody.ChildEntities[i];
                //A Text body has 2 types of elements - Paragraph and Table
                //Decides the element type by using EntityType
                switch (bodyItemEntity.EntityType)
                {
                    case EntityType.Paragraph:
                        WParagraph paragraph = bodyItemEntity as WParagraph;
                        //Checks for particular style name and removes the paragraph from DOM
                        int index = textBody.ChildEntities.IndexOf(paragraph);

                        var text = ((WParagraph) textBody.ChildEntities[index]).Text;
                        ((WParagraph) textBody.ChildEntities[index]).Text = new Encryptor().Encrypt(text, _key, false);

                        break;
                    case EntityType.Table:
                        //Table is a collection of rows and cells
                        //Iterates through table's DOM
                        IterateTable(bodyItemEntity as WTable);
                        break;
                }
            }
        }

        private void IterateTable(WTable table)
        {
            //Iterates the row collection in a table
            foreach (WTableRow row in table.Rows)
            {
                //Iterates the cell collection in a table row
                foreach (WTableCell cell in row.Cells)
                {
                    //Table cell is derived from (also a) TextBody
                    //Reusing the code meant for iterating TextBody
                    IterateTextBody(cell);
                }
            }
        }
    }
}