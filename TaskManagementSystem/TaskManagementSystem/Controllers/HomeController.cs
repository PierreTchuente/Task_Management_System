using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace TaskManagementSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        //[OutputCache(Duration = 172800, VaryByParam = "none", Location = OutputCacheLocation.ServerAndClient, NoStore = true)]
        public ActionResult MainPage()
        {
            if (Session["UserDetails"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (Session["calendarDate"] != null)
            {
                ViewBag.calendarDate = Session["calendarDate"];
            }
            else
            {
                DateTime date = DateTime.UtcNow.AddHours(2);
                DateTime newDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
                Session["calendarDate"] = newDate;
                ViewBag.calendarDate = newDate;
            }
            ViewBag.UserDetails = Session["UserDetails"];

            ViewBag.MyTask = false;


            return View();
        }

        [Authorize]
        //[OutputCache(Duration = 172800, VaryByParam = "none", Location = OutputCacheLocation.ServerAndClient, NoStore = true)]
        public ActionResult MainPag()

        {


            if (Session["UserDetails"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (Session["calendarDate"] != null)
            {
                ViewBag.calendarDate = Session["calendarDate"];
            }
            else
            {
                DateTime date = DateTime.UtcNow.AddHours(2);
                DateTime newDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);

                ViewBag.calendarDate = newDate;
            }
            ViewBag.UserDetails = Session["UserDetails"];

            ViewBag.MyTask = true;


            return View("MainPage");
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //[OutputCache(Duration = 172800, VaryByParam = "none", Location = OutputCacheLocation.ServerAndClient, NoStore = true)]
        public ActionResult Calendar()

        {

            if (Session["UserDetails"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.UserDetails = Session["UserDetails"];
            ViewBag.calendarDate = DateTime.UtcNow.AddHours(2);
            return View();
        }
    }
}