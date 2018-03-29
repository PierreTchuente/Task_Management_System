using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementDBLibrary.Model;

namespace TaskManagementDBLibrary.DataAccessLayer
{
    public class EquipmentManagementTableAccess
    {
        //==================== EQUIPMENT ===================================================
        //--------------- CreateEquipmentDataAccess -------------------------------------
        public static void CreateEquipmentDataAccess(EquipmentModel eM)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.CreateEquipmentSP(
                eM.name,
                eM.desc,
                eM.quantity, eM.isApproved);
        }
        //--------------- GetEquipmentsDataAccess -------------------------------------
        public static IEnumerable<GetEquipmentsSP_Result> GetEquipmentsDataAccess()
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetEquipmentsSP_Result> res = DatabaseEntity.GetEquipmentsSP().ToList();
            return res;
        }
        //--------------- GetOrdersPerEquipmentDataAccess -------------------------------------
        public static IEnumerable<GetOrdersPerEquipmentSP_Result> GetOrdersPerEquipmentDataAccess(int eqID, DateTime date)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetOrdersPerEquipmentSP_Result> res = DatabaseEntity.GetOrdersPerEquipmentSP(eqID, date).ToList();
            return res;
        }
        //--------------- GetOrderShoppingCartDataAccess -------------------------------------
        public static IEnumerable<GetOrderShoppingCartSP_Result> GetOrderShoppingCartDataAccess(Guid uID, DateTime date)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetOrderShoppingCartSP_Result> res = DatabaseEntity.GetOrderShoppingCartSP(uID, date).ToList();
            return res;
        }
        //--------------- GetTodaysOrdersDataAccess -------------------------------------
        public static IEnumerable<GetTodaysOrdersSP_Result> GetTodaysOrdersDataAccess(DateTime date)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetTodaysOrdersSP_Result> res = DatabaseEntity.GetTodaysOrdersSP(date).ToList();
            return res;
        }
        //--------------- UpdateEquipmentDataAccess -------------------------------------
        public static void UpdateEquipmentDataAccess(EquipmentModel eM)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.UpdateEquipmentSP(
               eM.id,
               eM.name,
               eM.desc,
               eM.quantity
                );
        }
        //--------------- ApproveEquipmentDataAccess -------------------------------------
        public static void ApproveEquipmentDataAccess(int eqID)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.ApproveEquipmentsSP(eqID);
        }
        //--------------- UpdateEquipmentStatusDataAccess -------------------------------------
        public static void UpdateEquipmentStatusDataAccess(UpdateEquipmentStatusModel ueM)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.UpdateEquipmentStatusSP(
                ueM.eqID,
                ueM.statusID,
                ueM.OrderEqID,
                ueM.time);
        }

















        //==================== EQUIPMENT ===================================================
        //--------------- CreateOrderEquipmentDataAccess -------------------------------------
        public static string CreateOrderEquipmentDataAccess(OrderEquipmentModel oM)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            string res = DatabaseEntity.CreateOrderEquipmentSP(
                oM.equipmentID,
                oM.userID,
                oM.qtyOrdered,
                oM.date,
                oM.comment).ToString();
            return res;
        }

        //--------------- DeleteOrderEquipmentDataAccess -------------------------------------
        public static void DeleteOrderEquipmentDataAccess(Guid id)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.DeleteOrderEquipmentSP(id);
        }

        //--------------- ReturnOrderDataAccess -------------------------------------
        public static void ReturnOrderDataAccess(Guid id)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.ReturnOrderSP(id);
        }

        public static IEnumerable<OrdersNotReturnedSP_Result> GetOrderNotReturn()
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<OrdersNotReturnedSP_Result> ord = DatabaseEntity.OrdersNotReturnedSP();
            return ord;
        }
    }
}
