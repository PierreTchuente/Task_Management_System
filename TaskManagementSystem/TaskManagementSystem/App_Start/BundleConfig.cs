using System.Web;
using System.Web.Optimization;

namespace TaskManagementSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //*%* Note: Armand - Rememeber turning on this before deploying. also take care of the font location.
            BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.signalR-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularjslib").Include(
                         "~/Scripts/angular.js",
                        "~/Scripts/angular-ui-router.js",
                        "~/Scripts/moment.min.js",
                        "~/Scripts/fullcalendar.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/taskManagement_AngJS").Include(
                      "~/Scripts/TaskManagementScripts/mainApp.js",

                       //Messaging Service =====================================================================
                       "~/Scripts/TaskManagementScripts/ClientHubServices/module.js",
                       "~/Scripts/TaskManagementScripts/ClientHubServices/MessageService.js",

                       //Shared Data Service====================================================================
                       "~/Scripts/SharedServices/module.js",
                       "~/Scripts/SharedServices/SharedData/SharedDataService_Ver_2.js",

                      //Main App Controller 
                      "~/Scripts/TaskManagementScripts/MainController.js",


                      //Responsibilities========================================================================

                      //Responsibilities - module
                      "~/Scripts/TaskManagementScripts/Responsibilities/module.js",
                      //Responsibilities - Services

                      //Responsibilities - Controllers
                      "~/Scripts/TaskManagementScripts/Responsibilities/controllers/createEditController.js",




                      //Task====================================================================================

                      //Task - Module
                      "~/Scripts/TaskManagementScripts/Task/module.js",
                      //Task - Services
                      "~/Scripts/TaskManagementScripts/Task/services/createEditService.js",
                      "~/Scripts/TaskManagementScripts/Task/services/departmentService.js",
                      //Task - Controllers                     
                      "~/Scripts/TaskManagementScripts/Task/controllers/createEditController.js",
                      "~/Scripts/TaskManagementScripts/Task/controllers/completedTasksController.js",
                      "~/Scripts/TaskManagementScripts/Task/controllers/addTaskShortcutController.js",
                      "~/Scripts/TaskManagementScripts/Task/controllers/deparmentController.js",


                      //Completed Task====================================================================================

                      //Completed Task - Module
                      "~/Scripts/TaskManagementScripts/CompletedTask/module.js",
                      //Completed Task - Services
                      "~/Scripts/TaskManagementScripts/CompletedTask/services/createEditService.js",
                      //Completed Task - Controllers                     
                      "~/Scripts/TaskManagementScripts/CompletedTask/controllers/CreateEditController.js",

                      //Equipment====================================================================================

                      //Equipment - Module
                      "~/Scripts/TaskManagementScripts/Equipments/module.js",
                      //Equipment - Services
                      //"~/Scripts/TaskManagementScripts/Task/services/createEditService.js",
                      //Equipment - Controllers                     
                      "~/Scripts/TaskManagementScripts/Equipments/controllers/createEditViewController.js",

                      //Notifications And Alert==================================================================================

                      "~/Scripts/TaskManagementScripts/NotificationAlert/module.js",
                      "~/Scripts/TaskManagementScripts/NotificationAlert/services/createEditService.js",
                     "~/Scripts/TaskManagementScripts/NotificationAlert/controllers/CreateEditController.js"

                      ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryeasing").Include(
                     "~/Scripts/jquery.easing.min.js"));

            //Task Management scripts.
            bundles.Add(new ScriptBundle("~/bundles/scrollingnav").Include(
                     "~/Scripts/scrolling-nav.js"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/DateUtilities.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizrcustom").Include(
                "~/Scripts/modernizr.custom.86080.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/TaskManagementCss/cloakCss.css",
                     "~/Content/TaskManagementCss/mine.css",
                     "~/Content/bootstrap.css",
                     "~/Content/TaskManagementCss/scrolling-nav.css",
                     "~/Content/FontCss/font-awesome.css",
                     "~/Content/TaskManagementCss/taskmanager.css",  
                    "~/Content/fullcalendar.min.css"
    //"~/Content/fullcalendar.print.min.css"

                      /*"~/Content/site.css"*/));


            bundles.Add(new StyleBundle("~/Content/othercss").Include(
                            "~/Content/demo.css",
                            "~/Content/style2.css",
                           "~/Content/TaskManagementCss/login.css"));
        }
    }
}
