using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementDBLibrary.DataAccessLayer;
using TaskManagementDBLibrary.Model;
using TaskManagementDBLibrary;
using System.Net;
using System.Web.UI;

namespace TaskManagementSystem.Controllers
{
    public class EquipmentManagementController : Controller
    {
        #region Equipement API

        [HttpGet]
        public JsonResult OrdersPerEquipment(int eqID, DateTime date)
        {
            var res = EquipmentManagementTableAccess.GetOrdersPerEquipmentDataAccess(eqID, date);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetOrderShoppingCart(Guid userID, DateTime date)
        {
            var res = EquipmentManagementTableAccess.GetOrderShoppingCartDataAccess(userID, date);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetTodaysOrders(DateTime date)
        {
            var res = EquipmentManagementTableAccess.GetTodaysOrdersDataAccess(date);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(Duration = 172800, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public JsonResult GetEquipments()
        {
            var res = EquipmentManagementTableAccess.GetEquipmentsDataAccess();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        // POST: EquipmentManagement/Create
        [HttpPost]
        public HttpStatusCodeResult CreateEquipment(EquipmentModel eM)
        {
            try
            {
                EquipmentManagementTableAccess.CreateEquipmentDataAccess(eM);
                return new HttpStatusCodeResult(HttpStatusCode.Created);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
        }

        [HttpPost]
        public HttpStatusCodeResult UpdateEquipmentStatus(UpdateEquipmentStatusModel ueM)
        {
            try
            {
                EquipmentManagementTableAccess.UpdateEquipmentStatusDataAccess(ueM);
                return new HttpStatusCodeResult(HttpStatusCode.Created);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
        }

        // POST: EquipmentManagement/Create
        [HttpPost]
        public JsonResult CreateOrderEquipment(OrderEquipmentModel[] oM)
        {
            try
            {
                for(int i = 0; i <= oM.Length; i++)
                {
                    oM[i].date = DateTime.UtcNow.AddHours(2);
                    var res = EquipmentManagementTableAccess.CreateOrderEquipmentDataAccess(oM[i]);
                }

                return Json(new HttpStatusCodeResult(HttpStatusCode.OK), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed), JsonRequestBehavior.AllowGet);
            }
        }


        // GET: EquipmentManagement/Edit/5
        [HttpPost]
        public HttpStatusCodeResult UpdateEquipment(EquipmentModel eM)
        {
            try
            {
                EquipmentManagementTableAccess.UpdateEquipmentDataAccess(eM);
                return new HttpStatusCodeResult(HttpStatusCode.Created);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
        }

        // POST: EquipmentManagement/Edit/5
        [HttpPost]
        public HttpStatusCodeResult DeleteOrderEquipment(Guid id)
        {
            try
            {
                EquipmentManagementTableAccess.DeleteOrderEquipmentDataAccess(id);
                return new HttpStatusCodeResult(HttpStatusCode.Created);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
        }

        // POST: EquipmentManagement/Edit/5
        [HttpPost]
        public HttpStatusCodeResult ReturnOrder(Guid id)
        {
            try
            {
                EquipmentManagementTableAccess.ReturnOrderDataAccess(id);
                return new HttpStatusCodeResult(HttpStatusCode.Created);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
        }

        #endregion

  
        [Authorize]
        public ActionResult Equipement()
        {
            ViewBag.UserDetails = Session["UserDetails"];
            ViewBag.calendarDate = Session["calendarDate"];
            return View();
        }
    }
}
