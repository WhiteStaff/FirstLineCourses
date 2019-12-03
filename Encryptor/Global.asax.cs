using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.UI.WebControls;
using Encryptor.Controllers;

namespace Encryptor
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            var httpException = ex as HttpException ?? ex.InnerException as HttpException;
            HttpRequest request = HttpContext.Current.Request;
            HttpResponse response = HttpContext.Current.Response;
            
            HttpContext.Current.Server.TransferRequest("/Home/Error");
        }

        private void application_EndRequest(object sender, EventArgs e)
        {
            HttpRequest request = HttpContext.Current.Request;
            HttpResponse response = HttpContext.Current.Response;

            if (response.StatusCode == 404 && response.SubStatusCode != 0)
            {
                
                response.Clear();
                HttpContext.Current.Response.Redirect("/Home/Error");
            }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}