﻿using System;
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
            ViewBag.text = ((TextRequestEntity)Session["curr"])?.Text??"";
            ViewBag.key = ((TextRequestEntity)Session["curr"])?.Key??"";
            ViewBag.isEncrypted = ((TextRequestEntity)Session["curr"])?.IsEncrypted??false;
            ViewBag.result = ((TextRequestEntity)Session["curr"])?.Result??"";
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
            if (upload != null)
            {
                using (var x = upload.InputStream)
                {
                    var a = x.Read(new byte[100], 0, 100);
                }
            }

            Session["firstactive"] = false;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Send(string text, string key,bool isEncrypted)
        {
            Session["curr"] = new TextRequestEntity(text, key, isEncrypted, new Models.Encryptor().Encrypt(text, key, isEncrypted));
            Session["firstactive"] = true;
            return RedirectToAction("Index");
        }

    }
}