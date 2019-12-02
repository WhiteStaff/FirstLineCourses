using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Encryptor
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private void application_EndRequest(object sender, EventArgs e)
        {
            HttpRequest request = HttpContext.Current.Request;
            HttpResponse response = HttpContext.Current.Response;

            if ((request.HttpMethod == "POST") &&
                (response.StatusCode == 404 && response.SubStatusCode == 13))
            {
                // Clear the response header but do not clear errors and
                // transfer back to requesting page to handle error
                response.ClearHeaders();
                
                HttpContext.Current.Server.Transfer(
                    "~/Errors/404.aspx");
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