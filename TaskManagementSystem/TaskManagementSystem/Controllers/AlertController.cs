using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManagementDBLibrary.DataAccessLayer;
using TaskManagementDBLibrary.Model;

namespace TaskManagementSystem.Controllers
{
    [Authorize]
    public class AlertController : Controller
    {
        // GET: Notification
        [HttpGet]
        public JsonResult GetListOfTodayAlert()
        {
            var list  = JsonConvert.SerializeObject(AlertAccess.GetTodayAlert());
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public HttpStatusCodeResult CreateUserAlert(AlertModel alert)
        {
            alert.alertID = Guid.NewGuid();
            try
            {
                AlertAccess.CreateUserAlertMethods(alert);
                return new HttpStatusCodeResult(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
        }

    }
}