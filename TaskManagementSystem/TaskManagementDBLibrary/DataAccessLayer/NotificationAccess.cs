using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementDBLibrary.Model;

namespace TaskManagementDBLibrary.DataAccessLayer
{
    public static class NotificationAccess
    {
        public static void CreateUserNotifMethods(NotitficationModel notif)
        {
            TaskManagementDBEntities Entities = new TaskManagementDBEntities();
            Entities.CreateUserNotification(notif.userID, notif.notiID, notif.sender_ID, notif.typeID, notif.message);
        }

        public static void CreateNotifMethods(NotitficationModel notif)
        {
            TaskManagementDBEntities Entities = new TaskManagementDBEntities();
            Entities.CreateNotification(notif.notiID, notif.sender_ID, notif.typeID, notif.message);
        }

        public static IEnumerable<GetUserNotifications_Result> GetUserNotifications(Guid userID)
        {
            TaskManagementDBEntities Entities = new TaskManagementDBEntities();
            IEnumerable<GetUserNotifications_Result> Enum = Entities.GetUserNotifications(userID);
            return Enum;
        }

        public static IEnumerable <GetListOfTodayNotification_Result> GetListOfTodayNotification()
        {
            TaskManagementDBEntities Entities = new TaskManagementDBEntities();
            IEnumerable<GetListOfTodayNotification_Result> Enum = Entities.GetListOfTodayNotification();
            return Enum;
        }

        public static void DeleteUserNotifMethods (Guid userID)
        {
            TaskManagementDBEntities Entities = new TaskManagementDBEntities();
            Entities.DeleteUserNotifications(userID);
        }

    }
}
