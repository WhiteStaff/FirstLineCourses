using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ThreatsParser.Exceptions;

namespace ThreatsParser.FileActions
{
    class FileLoader
    {
        public static void Download(string name)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile("https://bdu.fstec.ru/files/documents/thrlist.xlsx", name);
                }
            }
            catch (Exception e)
            {
                throw new NoConnectionException();
            }

        }
    }
}
