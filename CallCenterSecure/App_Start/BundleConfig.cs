using System.Web;
using System.Web.Optimization;

namespace CallCenterSecure
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/jquery.js"));

            // jQuery Validation
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery-validate/jquery.validate.js",
                        "~/Scripts/jquery-validate/jquery.validate.unobtrusive.js"));

            // Modernizr
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr/modernizr.js"));

            // Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap/bootstrap.js"));

            // jQuery UI
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jqueryui/jquery-ui.js"));

            // Bootstrap Datepicker
            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                        "~/Scripts/bootstrap-datepicker/bootstrap-datepicker.js"));

            // Bootstrap Datetimepicker
            bundles.Add(new ScriptBundle("~/bundles/datetimepicker").Include(
                        "~/Scripts/bootstrap-datetimepicker/bootstrap-datetimepicker.js"));

            // CSS Bundle
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/bootstrap-datepicker.css",
                      "~/Content/site.css"));
        }
    }
}
