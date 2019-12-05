using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DocumentFormat.OpenXml.Wordprocessing;
using Encryptor.Controllers;
using Encryptor.Models;
using Moq;
using NUnit.Framework;


namespace EncryptorTests
{
    class HomeControllerActionsTest
    {
        private HomeController controller;
        private Mock<HttpSessionStateBase> session;

        [OneTimeSetUp]
        public void SetUp()
        {
            controller = new HomeController();
            session = new Mock<HttpSessionStateBase>();

            Mock<HttpContextBase> httpContext = new Mock<HttpContextBase>();
            httpContext.SetupGet(c => c.Session).Returns(session.Object);

            ControllerContext ctx = new ControllerContext();
            ctx.HttpContext = httpContext.Object;
            
            controller.ControllerContext = ctx;

            ctx = new ControllerContext();
            ctx.HttpContext = httpContext.Object;
        }

        [Test]
        public void IndexReturns()
        {
            session.Setup(p => p["curr"]).Returns(new TextRequest("1212", "12", true, "134"));
            session.Setup(p => p["error"]).Returns(false);
            session.Setup(p => p["save"]).Returns(true);
            session.Setup(p => p["firstactive"]).Returns(true);

            ViewResult v = controller.Index() as ViewResult;
            Assert.AreEqual("Index", v.ViewName);
        }

        [Test]
        public void AboutReturns()
        {
            ViewResult v = controller.About() as ViewResult;
            Assert.AreEqual("About", v.ViewName);
        }

        [Test]
        public void ContactReturns()
        {
            ViewResult v = controller.Contact() as ViewResult;
            Assert.AreEqual("Contact", v.ViewName);
        }

        [Test]
        public void ErrorReturns()
        {
            ViewResult v = controller.Error() as ViewResult;
            Assert.AreEqual("Error", v.ViewName);
        }
        

        [TestCase("12", TestName = "Присылается файл с заданным именем")]
        [TestCase("", TestName = "присылается файл с именем по умолчанию при пустом имени")]
        public void SendReturnsFileWhenSave(string filename)
        {
            if (filename == "") filename = "NiName.docx";
            FileContentResult v = controller.Send("123", "ыв", true, "Сохранить", filename) as FileContentResult;
            Assert.AreEqual($"{filename}.docx", v.FileDownloadName);
            Assert.AreEqual("application/vnd.openxmlformats-officedocument.wordprocessingml.document", v.ContentType);
        }

        [Test]
        public void SendReturnsRedirectToIndexWhenCalculate()
        {
            RedirectToRouteResult v = controller.Send("123", "ыв", true, "Рассчитать") as RedirectToRouteResult ;
            Assert.IsTrue(v.RouteValues.ContainsValue("Index"));
        }

        

        [Test]
        public void UploadReturnsFileIfOk()
        {
            string solution_dir = Path.GetDirectoryName(Path.GetDirectoryName(
                TestContext.CurrentContext.TestDirectory));
            var filePath = $"{solution_dir}/Files/Encrypted/Result_v5.docx";

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                Mock<HttpPostedFileBase> uploadedFile = new Mock<HttpPostedFileBase>();

                uploadedFile
                    .Setup(f => f.InputStream)
                    .Returns(fileStream);

                FileContentResult v = controller.Upload(uploadedFile.Object, "ывфы", true, "123") as FileContentResult;
                Assert.AreEqual("123.docx", v.FileDownloadName);
                Assert.AreEqual("application/vnd.openxmlformats-officedocument.wordprocessingml.document", v.ContentType);
            }
        }

        [Test]
        public void UploadReturnsRedirectIfError()
        {
            string solution_dir = Path.GetDirectoryName(Path.GetDirectoryName(
                TestContext.CurrentContext.TestDirectory));
            var filePath = $"{solution_dir}/Files/Encrypted/Result_v5.docx";

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                Mock<HttpPostedFileBase> uploadedFile = new Mock<HttpPostedFileBase>();

                uploadedFile
                    .Setup(f => f.InputStream)
                    .Returns(fileStream);

                RedirectToRouteResult v = controller.Upload(uploadedFile.Object, "", true, "123") as RedirectToRouteResult;
                Assert.IsTrue(v.RouteValues.ContainsValue("Index"));
            }
        }
    }
}
