﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using OfficeOpenXml;
using ThreatsParser.Entities;

namespace ThreatsParser.FileActions
{
    static class FileUpdater
    {
        private static bool AreFilesEqual()
        {
            using (var client = new WebClient())
            {
                client.DownloadFile("https://bdu.fstec.ru/documents/files/thrlist.xlsx", "newdata.xlsx");
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

            byte[] bin = File.ReadAllBytes("newdata.xlsx");

            using (MemoryStream stream = new MemoryStream(bin))
            using (ExcelPackage excelPackage = new ExcelPackage(stream))
            {
                int x = 0;
                var sheet = excelPackage.Workbook.Worksheets[1];
                var lines = Math.Min(sheet.Dimension.End.Row, items.Count + 2);
                for (int i = 3; i <= lines; i++)
                {
                    var row = new string[8];
                    for (int j = 1; j <= 8; j++)
                    {
                        var value = sheet.Cells[i, j].Value.ToString();
                        row[j - 1] = value;
                    }

                    Threat currentNewThreat = new Threat(row);
                    newitems.Add(currentNewThreat);
                    if (!currentNewThreat.Equals(items[x]))
                    {
                        result.Add(new ThreatsChanges(items[x], currentNewThreat));
                    }

                    x++;
                }

                if (sheet.Dimension.End.Row > lines)
                {
                    for (int i = lines + 1; i <= sheet.Dimension.End.Row; i++)
                    {
                        var row = new string[8];
                        for (int j = 1; j <= 8; j++)
                        {
                            var value = sheet.Cells[i, j].Value.ToString();
                            row[j - 1] = value;
                        }

                        Threat currentNewThreat = new Threat(row);

                        result.Add(new ThreatsChanges(null, currentNewThreat));
                        newitems.Add(currentNewThreat);


                        x++;
                    }
                }
                else if (x > lines - 3)
                {
                    for (int i = x; i < items.Count; i++)
                    {
                        result.Add(new ThreatsChanges(items[i], null));
                    }
                }
            }

            return result;
        }
    }
}