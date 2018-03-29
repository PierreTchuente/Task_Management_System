using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using TaskManagementDBLibrary;
using TaskManagementDBLibrary.DataAccessLayer;
using TaskManagementDBLibrary.Model;

namespace TaskManagementSystem.Controllers
{
    public class ResponsibilityController : Controller
    {
        // GET: Responsibility
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: Responsibility/Details/5
        //public JsonResult Details(int id)
        //{
        //   var res = MainTableAccess.
        //}

        //// GET: Responsibility/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Responsibility/Create

        //[HttpPost]
        //[Authorize]
        //public ActionResult CreateResponsibility(ResponsibilityModel model)
        //{
        //    try
        //    {
        //        //MasterTableAccess.Responsibilities_Create(model);
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Responsibility/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        [HttpGet]
        [OutputCache(Duration = 172800, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public JsonResult DepartmentList()
        {
            //Guid id = new Guid(userID);
            var res = MainTableAccess.GetDepartmentsDataAccess();
            return Json(res, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [OutputCache(Duration = 172800, VaryByParam = "deptID", Location = OutputCacheLocation.Client, NoStore = true)]
        public JsonResult DepartmentDetails(int deptID)
        {
            //Guid id = new Guid(userID);
            DeptDetailsModel res = new DeptDetailsModel();
            res.deptDetails = MainTableAccess.GetDepartmentDataAccess(deptID);
            res.responsibilities = MainTableAccess.GetResponsibilitiesPerDeptDataAccess(deptID).ToList();
            res.otherMembers = MainTableAccess.GetOtherMembersInDeptRespDataAccess(deptID).ToList();
            return Json(res, JsonRequestBehavior.AllowGet);
        }



        //// POST: Responsibility/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Responsibility/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Responsibility/Delete/5
        [HttpPost]
        public HttpStatusCodeResult Remove(int id)
        {
            try
            {
                // TODO: Add delete logic here
                MainTableAccess.RemoveResponsibilityDataAccess(id);
                return new HttpStatusCodeResult(HttpStatusCode.OK);

            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
        }

        [HttpPost]
        public HttpStatusCodeResult CreateResponsibility(ResponsibilityModel rM)
        {
            try
            {
                // TODO: Add create responsibility Controller logic here
                MainTableAccess.CreateResponsibilityDataAccess(rM);

                return new HttpStatusCodeResult(HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
        }


        [HttpPost]
        public HttpStatusCodeResult AddMemberToDepartmentResponsibilitiesController(AddMemberModel aM)
        {
            try
            {
                // TODO: Add create responsibility Controller logic here
                MainTableAccess.AddMemberToDepartmentResponsibilitiesDataAccess(aM.userID, aM.deptID);

                return new HttpStatusCodeResult(HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
        }
        [HttpPost]
        public HttpStatusCodeResult RemoveMemberFromDepartmentResponsibilitiesController(AddMemberModel aM)
        {
            try
            {
                // TODO: Add create responsibility Controller logic here
                MainTableAccess.RemoveMemberFromDepartmentResponsibilitiesDataAccess(aM.userID, aM.deptID);

                return new HttpStatusCodeResult(HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
        }

    }
}
