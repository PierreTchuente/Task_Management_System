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
    public class NotificationController : Controller
    {
        // GET: Notification
        [HttpGet]
        public JsonResult GetListOfTodayNotitif()
        {
            var listTodayNotif = JsonConvert.SerializeObject(NotificationAccess.GetListOfTodayNotification());
            return Json(listTodayNotif, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUserNotif(Guid userID)
        {
            var ListNotif = JsonConvert.SerializeObject(NotificationAccess.GetUserNotifications(userID));
            return Json(ListNotif, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public HttpStatusCodeResult CreateNotification(NotitficationModel notif)
        {
            notif.notiID = Guid.NewGuid();
            try
            {
                NotificationAccess.CreateNotifMethods(notif);
                return new HttpStatusCodeResult(HttpStatusCode.Created);
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
        }

        [HttpPost]
        public HttpStatusCodeResult CreateUserNotification(NotitficationModel notif)
        {
            notif.notiID = Guid.NewGuid();
            try
            {
                NotificationAccess.CreateUserNotifMethods(notif);
                return new HttpStatusCodeResult(HttpStatusCode.Created);
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
        }

        [HttpPost]
        public HttpStatusCodeResult DeleteUserNotifications(userID userid)
        {
            
            try
            {
                NotificationAccess.DeleteUserNotifMethods(userid.id);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
        }
    }
    public class userID
    {
        public Guid id { get; set; }
    }
}