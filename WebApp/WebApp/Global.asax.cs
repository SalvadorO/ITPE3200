using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApp.App_Start;

namespace WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Error(object sender, EventArgs e) {
            Exception exception = Server.GetLastError();
            
            // Log the exception and notify system operators
            ExceptionUtility.LogException(exception, "DefaultPage");
           
            Response.Write("<h2>Error!</h2>\n");
            Response.Write(
                "<p>" + "Exception: " + exception.Message + "</p>\n");
            Response.Write(
               "<p>" + "Stack Trace: "+  "</p>\n");
            Response.Write(
               "<p>" + exception.StackTrace + "</p>\n");




            // Clear the error from the server
            Server.ClearError();


        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.EnableOptimizations = true;
        }
    }
}
