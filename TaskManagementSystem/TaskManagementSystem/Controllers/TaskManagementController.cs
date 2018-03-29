using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using TaskManagementDBLibrary.DataAccessLayer;
using TaskManagementDBLibrary.Model;
using TaskManagementDBLibrary;
using System.Globalization;
using Newtonsoft.Json;

namespace TaskManagementSystem.Controllers
{
    [Authorize]
    public class TaskManagementController : Controller
    {
        //// GET: TaskManagement
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: TaskManagement/Details/5

        [HttpGet]
        public string Details(Guid taskID)
        {
            TaskDetailsModel res = new TaskDetailsModel();
            res.taskDetails = MainTableAccess.GetTaskDataAccess(taskID);
            res.users = MainTableAccess.ListOfUsersAssignedToTaskDataAccess(taskID).ToList();
            res.checkLists = MainTableAccess.GetChecklistsPerTaskDataAccess(taskID).ToList();
            //MANUELLA ADDED BEGIN
            res.comments = MainTableAccess.GetCommentsPerTaskDataAccess(taskID).ToList();
            //MANUELLA ADDED ENDde
            JsonResult j = Json(res, JsonRequestBehavior.AllowGet);
            string json = JsonConvert.SerializeObject(j);
            return json;
        }

        //// GET: TaskManagement/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: TaskManagement/Create
        [HttpPost]
        public HttpStatusCodeResult Create(TaskModel model)
        {
            try
            {
                // TODO: Add insert logic here
                DateTime t = new DateTime();
                TimeZoneInfo hwzone = TimeZoneInfo.FindSystemTimeZoneById("South Africa Standard Time");
                t = TimeZoneInfo.ConvertTime((DateTime)model.expectedEnd, TimeZoneInfo.Local, hwzone);
                model.expectedEnd = t;
                using (System.IO.StreamWriter file =
                 new System.IO.StreamWriter(Server.MapPath("~/logs/log.txt"), true))
                {
                    file.WriteLine("Expected Start " + model.expectedStart + "   " + "Expected End" + model.expectedEnd);
                }

                model.expectedStart = DateTime.UtcNow.AddHours(2);
                Guid TaskID = Guid.NewGuid();
                model.id = TaskID;
                MainTableAccess.CreateTaskDataAccess(model);

                if (model.UserIDs != null)
                {
                    foreach (Guid id in model.UserIDs)
                    {
                        MainTableAccess.AssignTaskToUserDataAccess(TaskID, id);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.Created);

            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
        }

        // GET: TaskManagement/Edit/5

        [HttpPost]
        public HttpStatusCodeResult Edit(TaskModel model)
        {
            try
            {
                // TODO: Add insert logic here
                DateTime t = new DateTime();
                TimeZoneInfo hwzone = TimeZoneInfo.FindSystemTimeZoneById("South Africa Standard Time");
                t = TimeZoneInfo.ConvertTime((DateTime)model.expectedEnd, TimeZoneInfo.Local, hwzone);
                model.expectedEnd = t;

                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(Server.MapPath("~/logs/log.txt"), true))
                {
                    file.WriteLine("From Edit " + "Expected Start " + model.expectedStart + "   " + "Expected End" + model.expectedEnd);
                }

                MainTableAccess.UpdateTaskDataAccess(model);
                if (model.UserIDs != null)
                {
                    foreach (Guid? id in model.UserIDs)
                    {

                        MainTableAccess.AssignTaskToUserDataAccess(model.id, id);
                    }
                }
                if (model.CheckListDescriptions != null)
                {
                    int i = 0;
                    foreach (string desc in model.CheckListDescriptions)
                    {
                        ChecklistModel cModel = new ChecklistModel();
                        cModel.desc = desc;
                        cModel.id = Guid.NewGuid();
                        cModel.isCompleted = false;
                        cModel.taskID = model.id;
                        cModel.weight = int.Parse(model.CheckListLevels.ElementAt(i));
                        i++;
                        MainTableAccess.CreateChecklistDataAccess(cModel);
                    }
                }

                return new HttpStatusCodeResult(HttpStatusCode.OK);

            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
        }


        [HttpPost]
        public HttpStatusCodeResult CompleteTask(TaskModel model)
        {
            try
            {
                // TODO: Add insert logic here
                model.actualEnd = DateTime.UtcNow.AddHours(2);
                MainTableAccess.UpdateIsCompletedDataAccess(model);
                return new HttpStatusCodeResult(HttpStatusCode.OK);

            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
        }





        [HttpPost]
        public HttpStatusCodeResult DeleteCheckList(ChecklistModel model)
        {
            try
            {
                // TODO: Add insert logic here
                MainTableAccess.DeleteChecklistDataAccess(model);

                return new HttpStatusCodeResult(HttpStatusCode.OK);

            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
        }



        [HttpPost]
        public HttpStatusCodeResult DeleteUsersAssignedToTask(Guid taskID, List<Guid> userIDs)
        {
            try
            {
                // TODO: Add insert logic here
                if (userIDs != null)
                {
                    foreach (Guid id in userIDs)
                    {
                        MainTableAccess.DeleteTaskUserDataAccess(taskID, id);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.OK);

            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
        }


        [HttpGet]
        public JsonResult ListTaskPerUserDay(Guid userID, DateTime date)
        {
            var res = MainTableAccess.GetTaskPerDayPerUserDataAccess(userID, date);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string ListTaskPerDay(DateTime date)
        {

            var res = MainTableAccess.GetTaskPerDayDataAccess(date);
            //for(int i = 0; i < res.Count(); i++)
            //{
            //    var ress = MainTableAccess.GetChecklistsWeightPerTaskDataAccess(res.ElementAt(i).TASK_ID);
            //}

            JsonResult j = Json(res, JsonRequestBehavior.AllowGet);
            string json = JsonConvert.SerializeObject(j);
            return json;
        }


        //ADDED KEVIN CODE FOR CALENDAR
        [HttpGet]
        public string ListTaskPerCalendarDay(DateTime date)
        {

            var res = MainTableAccess.GetTaskPerCalendarDayDataAccess(date);
            //for(int i = 0; i < res.Count(); i++)
            //{
            //    var ress = MainTableAccess.GetChecklistsWeightPerTaskDataAccess(res.ElementAt(i).TASK_ID);
            //}
            JsonResult j = Json(res, JsonRequestBehavior.AllowGet);
            string json = JsonConvert.SerializeObject(j);
            return json;
        }

        [HttpGet]
        public string ListCompletedCalendarTaskPerDay(DateTime date)
        {
            var res = MainTableAccess.GetCompletedCalendarTaskPerDayDataAccess(date);

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(Server.MapPath("~/logs/log.txt"), true))
            {
                foreach (var item in res)
                {
                    DateTime t = new DateTime();
                    TimeZoneInfo hwzone = TimeZoneInfo.FindSystemTimeZoneById("South Africa Standard Time");
                    t = TimeZoneInfo.ConvertTime((DateTime)item.TASK_ActualEndDate, TimeZoneInfo.Local, hwzone);
                    item.TASK_ActualEndDate = t;

                    file.WriteLine("From COmpleted Expected End Date " + item.TASK_ExpectedEndDate + "    Actual end date" + item.TASK_ActualEndDate + "   Expected start date" + item.TASK_ExpectedStartDate);

                }

            }

            JsonResult j = Json(res, JsonRequestBehavior.AllowGet);
            string json = JsonConvert.SerializeObject(j);
            return json;
        }



        [HttpGet]
        public string ListTaskPerCalendarDayPerUSer(DateTime date, Guid userid)
        {
            var res = MainTableAccess.GetTaskPerCalendarDayPerUserDataAccess(date, userid);
            JsonResult j = Json(res, JsonRequestBehavior.AllowGet);
            string json = JsonConvert.SerializeObject(j);
            return json;
        }
        // END ADDED KEVIN

        [HttpGet]
        public JsonResult ListCompletedTaskPerDay(DateTime date)
        {
            var res = MainTableAccess.GetCompletedTaskPerDayDataAccess(date);
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(Server.MapPath("~/logs/log.txt"), true))
            {
                file.WriteLine(res.ToString());
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }





        //MANUELLA ADDED
        [HttpPost]
        public HttpStatusCodeResult UpdateChecklist(ChecklistModel cM)
        {
            try
            {
                MainTableAccess.UpdateChecklistDataAccess(cM);

                return new HttpStatusCodeResult(HttpStatusCode.OK);

            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
        }

        [HttpPost]
        public HttpStatusCodeResult CreateComment(CommentModel cM)
        {
            try
            {
                MainTableAccess.CreateCommentDataAccess(cM);

                return new HttpStatusCodeResult(HttpStatusCode.OK);

            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
        }

        public ActionResult RedirectToTask(string Date)
        {
            DateTime date = DateTime.ParseExact(Date, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime newDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);


            Session["calendarDate"] = newDate;
            return RedirectToAction("MainPage", "Home");
        }


        [Authorize]
        public ActionResult Complete()
        {
            ViewBag.UserDetails = Session["UserDetails"];
            ViewBag.calendarDate = Session["calendarDate"];
            return View();
        }

    }
}
