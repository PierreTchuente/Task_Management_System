using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskManagementDBLibrary;
using TaskManagementDBLibrary.DataAccessLayer;
using TaskManagementDBLibrary.Model;

namespace TaskManagementDBLibrary.Model
{
    public class LoadDropDown
    {
        public static List<GetDepartmentsSP_Result> ListDepartment { get; set; }
        public static List<GetLevelsSP_Result> ListLevels { get; set; }
        public static List<GetGendesrSP_Result> ListGender { get; set; }
        public static List<GetNotificationType_Result> NotificationType { get; set; }

        public static void LoadDropDownList()
        {
            ListDepartment = MasterTableAccess.GetDepartmentsDataAccess().ToList();
            ListLevels = MasterTableAccess.GetLevelsDataAccess().ToList();
            ListGender = MasterTableAccess.GetGendersDataAccess().ToList();
            NotificationType = MasterTableAccess.GetNotificationTypeDataAccess().ToList();
        }
    }
}
