using System.Web.Optimization;

namespace MyCVonline
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/Scripts/app/Services/qualificationsServices.js",
                        "~/Scripts/app/Controllers/qualificationsController.js",
                        "~/Scripts/app/Services/sectionServices.js",
                        "~/Scripts/app/Controllers/SectionController.js",
                        "~/Scripts/app/Services/newsServices.js",
                        "~/Scripts/app/Controllers/newsController.js",
                        "~/Scripts/app/Services/jobsServices.js",
                        "~/Scripts/app/Controllers/jobsController.js",
                        "~/Scripts/app/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/moment.js",
                        "~/Scripts/underscore-min.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootstrap-datepicker.js",
                        "~/Scripts/bootbox.min.js",
                        "~/Scripts/bootstrap-tooltip.js",
                        "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-datepicker3.css",
                      "~/Content/site.css",
                      "~/Content/Animate.css"));

            bundles.Add(new StyleBundle("~/Content/Admincss").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/fontawesome.css",
                      "~/Content/Admin.css"));



            BundleTable.EnableOptimizations = true;
        }
    }
}
