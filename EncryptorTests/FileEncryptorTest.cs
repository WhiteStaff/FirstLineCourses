using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encryptor.Models;
using NUnit.Framework;

namespace EncryptorTests
{
    [TestFixture]
    class FileEncryptorTest
    {
        
        [TestCase("Files/Encrypted/Result_v5.docx")]
        public void CorrectlyEncryptAndDecrypt(string path)
        {
            string solution_dir = Path.GetDirectoryName(Path.GetDirectoryName(
                TestContext.CurrentContext.TestDirectory));
            var allPath = $"{solution_dir}/{path}";
            var x = File.OpenRead(allPath);
            var result1 = new DocxHandler(x, "скорпион", true).Parse();
            byte[] result2;
            x.Close();
            bool c;
            string docText = null;
            using (var stream = new MemoryStream(result1))
            {
                
                using (StreamReader sr = new StreamReader(stream))
                    docText = sr.ReadToEnd();

                


                //var file = File.Create("I:\\1.docx");
                //file.Write(stream.ToArray(), 0, (int)stream.Length);
                //file.Close();
                //result2 = new DocxHandler(stream, "скорпион", false).Parse();
            }
            //Assert.IsTrue(result2.SequenceEqual(result1));
            var x1 = new TextEncryptor("скорпион", true).Transform(docText);
            var x2 = new TextEncryptor("скорпион", false).Transform(docText);
            Assert.AreEqual(docText, x2);
            Console.WriteLine(x1.Length + " " + x2.Length);
        }

        
    }
}
