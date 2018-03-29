using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementDBLibrary.Model;

namespace TaskManagementDBLibrary
{
    public static class MasterTableAccess
    {

        //==================== GENDER ===================================================
        //------------- GetGendersDataAccess -------------------------------------
        public static IEnumerable<GetGendesrSP_Result> GetGendersDataAccess()
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetGendesrSP_Result> res = DatabaseEntity.GetGendesrSP();
            return res;
        }

        //==================== LEVEL ===================================================
        //------------- GetLevelsDataAccess -------------------------------------
        public static IEnumerable<GetLevelsSP_Result> GetLevelsDataAccess()
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetLevelsSP_Result> res = DatabaseEntity.GetLevelsSP();
            return res;
        }

        //==================== REPEATINTERVAL ===================================================
        //------------- GetRepeatIntervalsDataAccess -------------------------------------
        public static IEnumerable<GetRepeatIntervalsSP_Result> GetRepeatIntervalsDataAccess()
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetRepeatIntervalsSP_Result> res = DatabaseEntity.GetRepeatIntervalsSP();
            return res;
        }

        //==================== STATUS ===================================================
        //------------- GetGetStatusDataAccess -------------------------------------
        public static IEnumerable<GetStatussSP_Result> GetStatussDataAccess()
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetStatussSP_Result> res = DatabaseEntity.GetStatussSP();
            return res;
        }

        //==================== Department ===================================================
        //------------- GetGetDepartmentDataAccess -------------------------------------
        public static IEnumerable<GetDepartmentsSP_Result> GetDepartmentsDataAccess()
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetDepartmentsSP_Result> res = DatabaseEntity.GetDepartmentsSP();
            return res;
        }

        public static IEnumerable<GetNotificationType_Result> GetNotificationTypeDataAccess()
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetNotificationType_Result> res = DatabaseEntity.GetNotificationType();
            return res;
        }
    }
}
