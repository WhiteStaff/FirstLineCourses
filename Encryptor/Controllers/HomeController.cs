using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Serialization;
using DocumentFormat.OpenXml.Packaging;
using Encryptor.Models;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Xceed.Words.NET;

namespace Encryptor.Controllers
{
    public class HomeController : Controller
    {
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
            ViewBag.text = ((TextRequest)Session["curr"])?.Text??"";
            ViewBag.key = ((TextRequest)Session["curr"])?.Key??"";
            ViewBag.isEncrypted = ((TextRequest)Session["curr"])?.IsEncrypted??false;
            ViewBag.result = ((TextRequest)Session["curr"])?.Result??"";
            ViewBag.firstActive = "";
            ViewBag.secondActive = "";
            bool isFirst;
            if (Session["firstactive"] == null) isFirst = true;
            else isFirst = (bool) Session["firstactive"];

            if (isFirst)
            {
                ViewBag.firstActive = "active in";
            }
            else
                ViewBag.secondActive = "active in";

            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            WordDocument a;
            if (upload != null)
            {
                MemoryStream stream = new MemoryStream();
                using (var x = upload.InputStream)
                {
                    a = new DocxHandler(x, "скорпион", true).Parse(); 
                }
                
                Session["firstactive"] = false;
                a.Save(stream, FormatType.Docx);
                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    "123.docx");
                
            }
            
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Send(string text, string key,bool isEncrypted)
        {
            Session["curr"] = new TextRequest(text, key, isEncrypted, new Models.Encryptor().Encrypt(text, key, isEncrypted));
            Session["firstactive"] = true;
            return RedirectToAction("Index");
        }

    }
}