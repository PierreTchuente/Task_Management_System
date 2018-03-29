using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Script.Serialization;
using System.Collections.Generic;


using TaskManagementDBLibrary;
using TaskManagementDBLibrary.DataAccessLayer;
using TaskManagementDBLibrary.Model;
using TaskManagementSystem.Models;
using System.IO;
using System.Net;
using System.Web.UI;

namespace TaskManagementSystem.Controllers
{
    [AllowAnonymous]
    public class DepartmentManagementController : Controller
    {
  
 

        [Authorize]
        public ActionResult ManageDeparment()
        {

            ViewBag.UserDetails = Session["UserDetails"];
            ViewBag.calendarDate = Session["calendarDate"];

            return View();
        }

        [HttpPost]
        public HttpStatusCodeResult DeleteDepartment(DepartmentModel model)
        {
            try
            {
                DepartmentAccess.DeleteDepartment(model.id);
                return new HttpStatusCodeResult(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        // POST: UserManagement/Create
        [HttpPost]
        public HttpStatusCodeResult Create(DepartmentModel model)
        {
              try
                {
                    MainTableAccess.CreateDepartmentDataAccess(model.desc , model.hod);
                    return new HttpStatusCodeResult(HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                throw new Exception(ex.Message);
            }
          
        }

        [HttpPost]
        public HttpStatusCodeResult Update(DepartmentModel model)
        {
            try
            {
                DepartmentAccess.UpdateDepartment(model);
                return new HttpStatusCodeResult(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet]
        [OutputCache(Duration =172800, VaryByParam ="none", Location = OutputCacheLocation.Client, NoStore = true)]
        public JsonResult GetListOfDepartments()
        {
            return Json(MainTableAccess.GetDepartmentsDataAccess(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(Duration = 172800, VaryByParam = "id", Location = OutputCacheLocation.Client, NoStore = true)]
        public JsonResult CurrentHOD(int id)
        {
            var res = DepartmentAccess.GetCurrentHOD(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

    }
}
