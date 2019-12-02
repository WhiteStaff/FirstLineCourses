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
            ViewBag.Message = "Описание приложения";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.text = (Session["curr"] as TextRequest)?.Text ?? "";
            ViewBag.key = (Session["curr"] as TextRequest)?.Key ?? "";
            ViewBag.result = (Session["curr"] as TextRequest)?.Result ?? "";

            if (Session["error"] == null) Session["error"] = false;

            ViewBag.error = Session["error"];
            ViewBag.firstActive = "";
            ViewBag.secondActive = "";
            bool isFirst;

            if (Session["firstactive"] == null) isFirst = true;
            else isFirst = (bool) Session["firstactive"];

            Session["curr"] = null;
            Session["error"] = null;
            Session["firstactive"] = null;

            if (isFirst)
            {
                ViewBag.firstActive = "active in";
            }
            else
                ViewBag.secondActive = "active in";


            return View();
        }
        
        [HttpPost]
        
        public ActionResult Upload(HttpPostedFileBase upload, string key, bool isEncrypted, string name)
        {
            Session["firstactive"] = false;

            try
            {
                Session["error"] = false;
                if (upload != null)
                {
                    byte[] outputBytes;
                    using (var newFileStream = new MemoryStream())
                    {
                        using (var uploadInputStream = upload.InputStream)
                        {
                            outputBytes = new DocxHandler(uploadInputStream, key, isEncrypted).Parse();
                        }

                        Session["firstactive"] = false;
                        if (string.IsNullOrEmpty(name)) name = upload.FileName.Replace(".docx", "");
                    }

                    return File(outputBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                        $"{name}.docx");
                }
            }
            catch (Exception exception)
            {
                Session["error"] = true;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Send(string text, string key, bool isEncrypted)
        {
            Session["curr"] = new TextRequest(text, key, isEncrypted,
                new Models.Encryptor(key, isEncrypted).Transform(text));
            Session["error"] = false;
            Session["firstactive"] = true;
            return RedirectToAction("Index");
        }
    }
}