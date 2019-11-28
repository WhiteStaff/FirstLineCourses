using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using OfficeOpenXml;
using ThreatsParser.Entities;
using TreatsParser.Exceptions;
using TreatsParser.FileActions;

namespace ThreatsParser.FileActions
{
    static class FileUpdater
    {
        private static bool AreFilesEqual()
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile("https://bdu.fstec.ru/documents/files/thrlist.xlsx", "newdata.xlsx");
                }
            }
            catch (Exception e)
            {
                throw new NoConnectionException();
            }

            byte[] firstHash;
            byte[] secondHash;

            var areEqual = true;

            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead("data.xlsx"))
                {
                    firstHash = md5.ComputeHash(stream);
                }

                using (var stream = File.OpenRead("newdata.xlsx"))
                {
                    secondHash = md5.ComputeHash(stream);
                }
            }


            for (var i = 0; i < 16; i++)
            {
                areEqual = areEqual & (firstHash[i] == secondHash[i]);
            }

            return areEqual;
        }
        
        public static List<ThreatsChanges> GetDifference(List<Threat> items, out List<Threat> newitems) 
        {
            var result = new List<ThreatsChanges>();
            newitems = new List<Threat>();
            if (AreFilesEqual()) return result;

            newitems = FileParser.Parse("newdata.xlsx");

            var itemsCount = Math.Min(items.Count, newitems.Count);

            for (int i = 0; i < itemsCount; i++)
            {
                if (!items[i].Equals(newitems[i])) result.Add( new ThreatsChanges(items[i], newitems[i]));
            }

            if (items.Count > itemsCount)
            {
                for (int i = itemsCount; i < items.Count; i++)
                {
                    result.Add(new ThreatsChanges(items[i], null));
                }
            }

            if (newitems.Count > itemsCount)
            {
                for (int i = itemsCount; i < newitems.Count; i++)
                {
                    result.Add(new ThreatsChanges(null, newitems[i]));
                }
            }

            return result;
        }
    }
}