using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Encryptor.Exceptions;
using Encryptor.Models;


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

        public ActionResult Error()
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
            bool saveActive;

            if (Session["save"] == null) saveActive = false;
            else saveActive = (bool) Session["save"];

            ViewBag.Save = saveActive;

            if (Session["firstactive"] == null) isFirst = true;
            else isFirst = (bool) Session["firstactive"];

            Session["curr"] = null;
            Session["error"] = null;
            Session["firstactive"] = null;
            Session["save"] = null;

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
            Session["curr"] = null;


            try
            {
                Session["error"] = false;
                if (upload == null) return RedirectToAction("Index");
                byte[] outputBytes;

                using (var uploadInputStream = upload.InputStream)
                {
                    outputBytes = new DocxHandler(uploadInputStream, key, isEncrypted).Parse();
                }

                if (string.IsNullOrEmpty(name)) name = upload.FileName.Replace(".docx", "");


                return File(outputBytes,
                    "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    $"{name}.docx");
            }
            catch (Exception)
            {
                Session["error"] = true;
            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Send(string text, string key, bool isEncrypted, string action, string fileName)
        {
            Session["firstactive"] = true;
            Session["error"] = false;
            
            if (action == "Рассчитать")
            {
                string result;
                try
                {
                    result = new Models.TextEncryptor(key, isEncrypted).Transform(text);
                    Session["save"] = true;

                }
                catch (CustomEncryptException e)
                {
                    Session["save"] = false;
                    result = $"Ошибка: {e.Message}";
                }
                catch (Exception)
                {
                    Session["save"] = false;
                    result = "Произошла непредвиденная ошибка конвертации";
                }
                Session["curr"] = new TextRequest(text, key, isEncrypted, result);
                Session["result"] = result;
            }
            else
            {
                string resultText;
                resultText = Session["result"] == null ? "" : Session["result"].ToString();
                var bytes = new DocxCreator(resultText).Create();
                if (fileName == "") fileName = "NoName";
                Session["save"] = false;
                return File(bytes,
                    "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    $"{fileName}.docx");
            }

            

            return RedirectToAction("Index");
        }
    }
}