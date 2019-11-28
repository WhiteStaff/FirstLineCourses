using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Serialization;
using Encryptor.Models;

namespace Encryptor.Controllers
{
    public class HomeController : Controller
    {

        private string text;
        private string key;
        private bool isEncrypted;
        private string result;

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                // получаем имя файла
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                // сохраняем файл в папку Files в проекте
                upload.SaveAs(Server.MapPath("~/Files/" + fileName));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Send(string text, string key,bool isEncrypted)
        {
            ViewBag.text = text;
            ViewBag.key = key;
            ViewBag.isEncrypted = isEncrypted;
            ViewBag.result = Models.Encryptor.Encrypt(text, key);
            return RedirectToAction("Index");
        }

    }
}