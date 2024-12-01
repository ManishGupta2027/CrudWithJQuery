using System.Web;
using System.Web.Optimization;

namespace CrudOperation
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
      //      bundles.Add(new ScriptBundle("~/bundles/tray-js")
      //     .Include("~/assets/jsv2/app.js")
      //     .Include("~/assets/jsv2/controllers/globalCtrl.js")
      //     .Include("~/assets/jsv2/controllers/trayCtrl.js")
      //     .Include("~/assets/jsv2/directives/loader.js")
      //     .Include("~/assets/jsv2/directives/ng-table.js")
      //      .Include("~/assets/jsv2/directives/btAutoComplete.js")
      //      .Include("~/assets/jsv2/directives/ng-file-upload.js")
      //     .Include("~/assets/jsv2/directives/futureCalendar.js")
      //     .Include("~/assets/jsv2/directives/ngBootbox.js")
      //     .Include("~/assets/jsv2/directives/paging.js")
      //     .Include("~/assets/jsv2/directives/select.js")
      //     .Include("~/assets/jsv2/filters/ocxDate.js")
      //     .Include("~/assets/jsv2/filters/ocxDateTime.js")
      //     .Include("~/assets/jsv2/services/AlertService.js")
      //     .Include("~/assets/jsv2/services/GlobalData.js")
      //     .Include("~/assets/jsv2/services/Logger.js")
      //);
        }
    }
}
