using System.Web;
using System.Web.Optimization;

namespace EvolentCreation.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                    "~/Scripts/moment.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-animate.js",
                        "~/Scripts/angular-sanitize.js",
                        "~/Scripts/angular-ui-router.js",
                        "~/Scripts/angular-block-ui.js",
                        "~/Scripts/angular-moment.js",
                        "~/Scripts/ng-table.min.js",
                          "~/Scripts/Vkscript/vkbeautify.js"
                        //"~/Scripts/Evolent.Framework/file-directive.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/UiBootstrap").Include(
                      //"~/Scripts/angular-ui/ui-bootstrap.js",
                      "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                      "~/Scripts/angular-ui/bootstrap-datepicker.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/ApplicationScripts").Include(
                      "~/src/Main/main.controller.js",
                      "~/src/Main/main.service.js",
                        "~/src/ManageToken/token.createmodal.controller.js",
                       "~/src/ManageToken/token.editmodal.controller.js",
                      "~/src/ManageToken/managetoken.service.js",
                       "~/src/UserLogin/userLogin.js",
                       "~/src/TokenUpload/tokenupload.controller.js",
                       "~/src/TokenUpload/tokenupload.service.js",
                       "~/src/VerifyAPI/verifyapi.service.js",
                       "~/src/VerifyAPI/tokendictionary.controller.js",
                       "~/src/VerifyAPI/datadictionary.controller.js",
                       "~/src/commonService.js"
                      ));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/ui-bootstrap-csp.css",
                      "~/Content/angular-block-ui.css",
                      "~/Content/angular-toastr.css",
                      "~/Content/angular-auto-complete.css",
                      "~/Content/bootstrap-datepicker.css",
                      "~/Scripts/angular-csp.css",
                       "~/Content/ng-table.min.css"
                      ));

//#if DEBUG
//            BundleTable.EnableOptimizations = false;
//#else
//                    BundleTable.EnableOptimizations = true;
//#endif
        }
    }
}
