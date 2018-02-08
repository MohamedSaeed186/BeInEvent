using System.Web;
using System.Web.Optimization;

namespace BeInEvent
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/vendor").Include(
                "~/vendor/bootstrap/css/bootstrap.min.css",
                "~/vendor/font-awesome/css/font-awesome.min.css",
                "~/css/sb-admin.css"

                ));
            bundles.Add(new ScriptBundle("~/bundles/fvendor").Include(
              "~/js/sb-admin.min.js","~/js/sb-admin.js",
              "~/vendor/jquery-easing/jquery.easing.min.js",
              "~/vendor/bootstrap/js/bootstrap.bundle.min.js",
              "~/vendor/jquery/jquery.min.js"

              ));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js", "~/Scripts/jquery.easing.min.js", "~/Scripts/scripts.js", "~/Scripts/sb-admin.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/style.css"));
        }
    }
}
