using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementDBLibrary.Model;

namespace TaskManagementDBLibrary.DataAccessLayer
{
    public static class AlertAccess
    {
        public static void CreateUserAlertMethods(AlertModel alert)
        {
            TaskManagementDBEntities Entities = new TaskManagementDBEntities();
            Entities.CreateAlert(alert.alertID, alert.alertCreatedByID, alert.alertDescription);
        }

        public static IEnumerable<GetTodayAlert_Result> GetTodayAlert()
        {
            TaskManagementDBEntities Entities = new TaskManagementDBEntities();
            IEnumerable<GetTodayAlert_Result> Enum = Entities.GetTodayAlert();
            return Enum;
        }
    }
}
