using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace WebApp.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Scripts
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-{version}.min.js",
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .Include("~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui")
                .Include("~/Scripts/jquery-ui-{version}.min.js",
                "~/Scripts/jquery-ui-{version}.js"));


            //Styles
            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap.css",
                "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css")
                .Include("~/Content/themes/base/jquery-ui.css"));
        }
    }
}