using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementDBLibrary.Model;

namespace TaskManagementDBLibrary.DataAccessLayer
{
    public static class UserManagementAccess
    {
        public static void CreateUserMethods(UserModel user)
        {
            TaskManagementDBEntities Entities = new TaskManagementDBEntities();
            Entities.CreateUserTSP(user.id, user.name, user.username, user.levelId, user.desc, user.email
                , user.hashPass, user.photoLink, user.registeredTime, user.onlineStatus, user.phone,
                user.genderID, user.DOB, user.address, user.deptID, user.memberStatus);
        }


        public static void UpdateUserMethods(UserModel user)
        {
            TaskManagementDBEntities Entities = new TaskManagementDBEntities();
            Entities.UpdateUserTSP(user.id, user.name, user.username, user.levelId, user.desc, user.email
                , user.hashPass, user.photoLink, user.registeredTime, user.onlineStatus, user.phone,
                user.genderID, user.DOB, user.address, user.deptID, user.memberStatus);
        }

        public static IEnumerable<UpdateRoleSP_Result> UpdateRole(int DeptiD, Guid SuperUserID, Guid UserID)
        {
            TaskManagementDBEntities Entities = new TaskManagementDBEntities();
            System.Data.Entity.Core.Objects.ObjectResult objectUserRes;
            objectUserRes = Entities.UpdateRoleSP(DeptiD, UserID, SuperUserID);
            IEnumerable<UpdateRoleSP_Result> userDetailsEnum = objectUserRes.Cast<UpdateRoleSP_Result>();
            return userDetailsEnum;
            
        }

        public static IEnumerable<LogUserSP_Result> LogUserMethods(string email, string passWord)
        {
            TaskManagementDBEntities Entities = new TaskManagementDBEntities();
            System.Data.Entity.Core.Objects.ObjectResult objectUserRes;
            objectUserRes = Entities.LogUserSP(email);
            IEnumerable<LogUserSP_Result> userDetailsEnum = objectUserRes.Cast<LogUserSP_Result>();
            return userDetailsEnum;
        }


        public static IEnumerable<UerFullTextSearch_Result> UserFullTextMethods(string searchTerm)
        {
            TaskManagementDBEntities Entities = new TaskManagementDBEntities();
            IEnumerable<UerFullTextSearch_Result> usersEnum = Entities.UerFullTextSearch(searchTerm);
            return usersEnum;
        }

        public static void DeactivateFirsTimeLogInMethods(Guid UserID)
        {
            TaskManagementDBEntities Entities = new TaskManagementDBEntities();
            Entities.DeactivateFistimeLogIn(UserID);
        }

        public static void DeactivateActivateUserMethods(Guid UserID)
        {
            TaskManagementDBEntities Entities = new TaskManagementDBEntities();
            Entities.DeactivateActivateUserTSP(UserID);
        }

        public static void UpdateUserPictName(Guid UserID, string pictName)
        {
            TaskManagementDBEntities Entities = new TaskManagementDBEntities();
            Entities.ChangeUserPictName(UserID, pictName);
        }

        public static IEnumerable<GetUserDetails_Result> GetUserTSP(Guid UserID)
        {
            TaskManagementDBEntities Entities = new TaskManagementDBEntities();
            System.Data.Entity.Core.Objects.ObjectResult objResult = Entities.GetUserDetails(UserID);
            IEnumerable<GetUserDetails_Result> res = objResult.Cast<GetUserDetails_Result>();
            return res;
        }

        public static void UpdatDepartmentSP(int iD, string desc, Guid hOD)
        {
            TaskManagementDBEntities Entities = new TaskManagementDBEntities();
            Entities.UpdatDepartmentSP(iD, desc, hOD);
        }
    }
}
