using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            
            //Add ScriptBundle
            bundles.Add(new ScriptBundle("~/Content/js").Include(
                "~/Content/js/bootstrap.js",
                "~/Content/js/custom.js",
                "~/Content/js/jquery-3.4.1.min.js"
              ));



        

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/responsive.css",
                      "~/Content/css/style.css",
                      "~/Content/css/style.css.map",
                      "~/Content/css/style.scss"
                     
                      ));
        }
    }
}
