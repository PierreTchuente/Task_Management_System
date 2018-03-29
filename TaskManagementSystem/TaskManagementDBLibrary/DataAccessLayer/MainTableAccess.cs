using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementDBLibrary.Model;

namespace TaskManagementDBLibrary.DataAccessLayer
{
    public class MainTableAccess
    {

        //==================== CHECKLIST ===================================================
        //--------------- CreateChecklistDataAccess -------------------------------------
        public static void CreateChecklistDataAccess(ChecklistModel cM)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.CreateChecklistSP(
                cM.id,
                cM.taskID,
                cM.desc,
                cM.isCompleted,
                cM.weight);
        }
        //--------------- DeleteChecklistDataAccess -------------------------------------
        public static void DeleteChecklistDataAccess(ChecklistModel cM)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.DeleteChecklistSP(
                cM.id,
                cM.taskID);
        }
        //--------------- GetChecklistsWeightPerTaskDataAccess -------------------------------------
        public static IEnumerable<GetChecklistsWeightPerTaskSP_Result> GetChecklistsWeightPerTaskDataAccess(Guid taskID)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetChecklistsWeightPerTaskSP_Result> res = DatabaseEntity.GetChecklistsWeightPerTaskSP(taskID).ToList();
            return res;
        }
        //--------------- GetChecklistsPerTaskDataAccess -------------------------------------
        public static IEnumerable<GetChecklistsPerTaskSP_Result> GetChecklistsPerTaskDataAccess(Guid taskID)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetChecklistsPerTaskSP_Result> res = DatabaseEntity.GetChecklistsPerTaskSP(taskID).ToList();
            return res;
        }
        //--------------- UpdateChecklistDataAccess -------------------------------------
        public static void UpdateChecklistDataAccess(ChecklistModel cM)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.UpdateChecklistSP(
                cM.id,
                cM.isCompleted,
                cM.taskID
                );
        }




        //==================== COMMENT ===================================================
        //--------------- CreateChecklistDataAccess -------------------------------------
        public static void CreateCommentDataAccess(CommentModel cM)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.CreateCommentSP(
                Guid.NewGuid(),
                cM.taskID,
                cM.userID,
                cM.desc,
                cM.datePosted);
        }
        //--------------- GetCommentsPerTaskDataAccess -------------------------------------
        public static IEnumerable<GetCommentsPerTaskSP_Result> GetCommentsPerTaskDataAccess(Guid taskID)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetCommentsPerTaskSP_Result> res = DatabaseEntity.GetCommentsPerTaskSP(taskID).ToList();
            return res;
        }


        //==================== DEPARTMENT ===================================================




        //MANU ADDEDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD
        public static void AddMemberToDepartmentResponsibilitiesDataAccess(Guid userID, int deptID)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.AddMemberToDepartmentResponsibilitiesSP(
                userID,
                deptID);
        }
        public static void RemoveMemberFromDepartmentResponsibilitiesDataAccess(Guid userID, int deptID)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.RemoveMemberFromDepartmentResponsibilitiesSP(
                userID,
                deptID);
        }
        public static IEnumerable<GetResponsibilitiesPerDeptSP_Result> GetResponsibilitiesPerDeptDataAccess(int deptID)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetResponsibilitiesPerDeptSP_Result> res = DatabaseEntity.GetResponsibilitiesPerDeptSP(deptID).ToList();
            return res;
        }
        public static IEnumerable<GetOtherMembersInDeptRespSP_Result> GetOtherMembersInDeptRespDataAccess(int deptID)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetOtherMembersInDeptRespSP_Result> res = DatabaseEntity.GetOtherMembersInDeptRespSP(deptID).ToList();
            return res;
        }
        //MANU ADDEDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD END






        //==================== DEPARTMENT ===================================================
        //--------------- CreateDepartmentDataAccess -------------------------------------
        public static void CreateDepartmentDataAccess(String desc , Guid HOD)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.CreateDepartmentSP(
                desc,HOD);
        }
        //--------------- DeleteDepartmentDataAccess -------------------------------------
        public static void DeleteDepartmentDataAccess(int id)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.DeleteDepartmentSP(id);
        }
        //--------------- GetDepartmentDataAccess -------------------------------------
        public static GetDepartmentSP_Result GetDepartmentDataAccess(int id)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            GetDepartmentSP_Result res = DatabaseEntity.GetDepartmentSP(id).FirstOrDefault();
            return res;
        }
        //--------------- GetDepartmentsDataAccess -------------------------------------
        //Comment to test bitbucket
        //Commit 2
        //Commit 3
        public static IEnumerable<GetDepartmentsSP_Result> GetDepartmentsDataAccess()
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetDepartmentsSP_Result> res = DatabaseEntity.GetDepartmentsSP().ToList();
            return res;
        }
        //--------------- UpdateDepartmentDataAccess -------------------------------------
        public static void UpdateDepartmentDataAccess(DepartmentModel dM)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.UpdatDepartmentSP(
                dM.id,
                dM.desc,
                dM.hod);
        }




        //==================== PROJECT ===================================================
        //--------------- CreateDepartmentDataAccess -------------------------------------
        public static void CreateProjectDataAccess(ProjectModel pM)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.CreateProjectSP(
                pM.id,
                pM.name,
                pM.desc,
                pM.creatorID,
                pM.expectedStartDate,
                pM.expectedEndDate,
                pM.actualStartDate,
                pM.actualEndDate,
                pM.isPrivate);
        }
        //--------------- DeactivateProjectDataAccess -------------------------------------
        public static void DeactivateProjectDataAccess(Guid id)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.DeActivateProjectTSP(id);
        }
        //--------------- GetProjectDetailsDataAccess -------------------------------------
        public static GetProjectDetailsSP_Result GetProjectDetailsDataAccess(Guid id, Guid taskID)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            GetProjectDetailsSP_Result res = DatabaseEntity.GetProjectDetailsSP(id, taskID).FirstOrDefault();
            return res;
        }
        //--------------- GetProjectListPerCreatorDataAccess -------------------------------------
        public static IEnumerable<GetProjectsListPerCreatorSP_Result> GetProjectListPerCreatorDataAccess(Guid creatorID)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetProjectsListPerCreatorSP_Result> res = DatabaseEntity.GetProjectsListPerCreatorSP(creatorID).ToList();
            return res;
        }
        //--------------- GetProjectListDataAccess -------------------------------------
        public static IEnumerable<GetProjectsListSP_Result> GetProjectListDataAccess()
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetProjectsListSP_Result> res = DatabaseEntity.GetProjectsListSP().ToList();
            return res;
        }
        //--------------- UpdateProjectDataAccess -------------------------------------
        public static void UpdateProjectDataAccess(ProjectModel pM)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.UpdateProjectSP(
                pM.id,
                pM.name,
                pM.desc,
                pM.creatorID,
                pM.expectedStartDate,
                pM.expectedEndDate,
                pM.actualStartDate,
                pM.actualEndDate,
                pM.isPrivate);
        }




        //==================== RESPONSIBILITY ===================================================
        //--------------- CreateResponsibilityDataAccess -------------------------------------
        public static void CreateResponsibilityDataAccess(ResponsibilityModel rM)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.CreateResponsibilitySP(
                rM.Title,
                rM.Description,
                rM.deptID);
        }
        //--------------- UpdateResponsibilityDataAccess -------------------------------------
        public static void UpdateResponsibilityDataAccess(ResponsibilityModel rM)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.UpdateResponsibilitySP(
                rM.id,
                rM.Title,
                rM.Description);
        }
        //--------------- RemoveResponsibilityDataAccess -------------------------------------
        public static void RemoveResponsibilityDataAccess(int respID)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.RemoveResponsibilitySP(respID);
        }







        //==================== TASK ===================================================
        //--------------- AssignTaskToUserDataAccess -------------------------------------
        public static void AssignTaskToUserDataAccess(Guid? taskID, Guid? UserID)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.AssignTaskUserSP(taskID, UserID);
        }
        //--------------- CreateTaskDataAccess -------------------------------------
        public static void CreateTaskDataAccess(TaskModel tM)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
 

            DatabaseEntity.CreateTaskSP(
                tM.id,
                tM.name,
                tM.desc,
                tM.expectedStart,
                tM.expectedEnd,
                tM.actualStart,
                tM.actualEnd,
                tM.isPrivate,
                tM.folderPath,
                tM.weight,
                tM.priorityLevel,
                tM.creator,
                tM.status,
                tM.isCompleted,
                tM.isRepeated,
                tM.repeatIntervals,
                tM.projectID
                );


        }

        //--------------- GetTaskPerDayPerUserDataAccess -------------------------------------
        public static IEnumerable<GetTasksPerDayPerUserSP_Result> GetTaskPerDayPerUserDataAccess(Guid userID, DateTime todaysDate)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetTasksPerDayPerUserSP_Result> res = DatabaseEntity.GetTasksPerDayPerUserSP(userID, todaysDate).ToList();
            return res;
        }
        //--------------- GetTaskPerDayDataAccess -------------------------------------
        public static IEnumerable<GetTasksPerDaySP_Result> GetTaskPerDayDataAccess(DateTime todaysDate)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetTasksPerDaySP_Result> res = DatabaseEntity.GetTasksPerDaySP(todaysDate).ToList();
            return res;
        }


        // ADDED BY KEVIN FOR TASK WITH CALENDAR

        public static IEnumerable<GetTasksPerCalendarDaySP_Result> GetTaskPerCalendarDayDataAccess(DateTime todaysDate)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetTasksPerCalendarDaySP_Result> res = DatabaseEntity.GetTasksPerCalendarDaySP(todaysDate).ToList();
            return res;
        }

        public static IEnumerable<GetTasksPerCalendarDayPerUserSP_Result> GetTaskPerCalendarDayPerUserDataAccess(DateTime todaysDate, Guid UserID)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetTasksPerCalendarDayPerUserSP_Result> res = DatabaseEntity.GetTasksPerCalendarDayPerUserSP(todaysDate, UserID);
            return res;
        }

        // END OF KEVIN ADDED CODE



        //--------------- GetTaskPerDayDataAccess -------------------------------------
        public static IEnumerable<GetCompletedCalendarTasksPerDaySP_Result> GetCompletedCalendarTaskPerDayDataAccess(DateTime todaysDate)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetCompletedCalendarTasksPerDaySP_Result> res = DatabaseEntity.GetCompletedCalendarTasksPerDaySP(todaysDate).ToList();
            return res;
        }


        //ADDED BY KEVIN FOR COMPLETED TASK WITH CALENDAR
        public static IEnumerable<GetCompletedTasksPerDaySP_Result> GetCompletedTaskPerDayDataAccess(DateTime todaysDate)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetCompletedTasksPerDaySP_Result> res = DatabaseEntity.GetCompletedTasksPerDaySP(todaysDate).ToList();
            return res;
        }
        // END ADDED BY KEVIN
        //--------------- GetTaskUsersDataAccess -------------------------------------
        public static IEnumerable<GetTaskUsersSP_Result> GetTaskUsersDataAccess()
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetTaskUsersSP_Result> res = DatabaseEntity.GetTaskUsersSP().ToList();
            return res;
        }
        //--------------- ListOfUsersAssignedToTaskDataAccess -------------------------------------
        public static IEnumerable<ListOfUsersAssignedToTaskIDSP_Result> ListOfUsersAssignedToTaskDataAccess(Guid id)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<ListOfUsersAssignedToTaskIDSP_Result> res = DatabaseEntity.ListOfUsersAssignedToTaskIDSP(id).ToList();
            return res;
        }
        //--------------- UnAssignTaskToUserDataAccess -------------------------------------
        public static void UnAssignTaskToUserDataAccess(Guid userID, Guid TaskID)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.UnassignTaskToUserSP(userID, TaskID);
        }
        //--------------- UpdateIsCompletedDataAccess -------------------------------------
        public static void UpdateIsCompletedDataAccess(TaskModel tM)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.UpdateIsCompletedTaskSP(
                tM.id,
                tM.isCompleted,
                tM.actualEnd);
        }
        //--------------- UpdateTaskDataAccess -------------------------------------
        public static void UpdateTaskDataAccess(TaskModel tM)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.UpdateTaskSP(
                tM.id,
                tM.name,
                tM.desc,
                tM.expectedStart,
                tM.expectedEnd,
                tM.actualStart,
                tM.actualEnd,
                tM.isPrivate,
                tM.folderPath,
                tM.weight,
                tM.priorityLevel,
                tM.status,
                tM.isCompleted,
                tM.isRepeated,
                tM.repeatIntervals,
                tM.projectID);
        }
        //--------------- GetTaskPerDayPerUserDataAccess -------------------------------------
        public static IEnumerable<ListOfTasksAssignedToUserID_Result> ListOfTasksPerUserDataAccess(Guid userID)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<ListOfTasksAssignedToUserID_Result> res = DatabaseEntity.ListOfTasksAssignedToUserID(userID).ToList();
            return res;
        }
        //--------------- GetTaskPerDayPerUserDataAccess -------------------------------------
        public static IEnumerable<ListOfUsersAssignedToTaskIDSP_Result> ListOfUsersPerTasksDataAccess(Guid taskID)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<ListOfUsersAssignedToTaskIDSP_Result> res = DatabaseEntity.ListOfUsersAssignedToTaskIDSP(taskID).ToList();
            return res;
        }
        //--------------- GetTaskPerDayPerUserDataAccess -------------------------------------
        public static GetTaskSP_Result GetTaskDataAccess(Guid taskID)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            GetTaskSP_Result res = DatabaseEntity.GetTaskSP(taskID).FirstOrDefault();
            return res;
        }
        //--------------- DeleteTaskUserDataAccess -------------------------------------
        public static void DeleteTaskUserDataAccess(Guid taskID, Guid UserID)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.DeleteTaskUsersSP(UserID, taskID);
        }







        //==================== USER ===================================================
        //--------------- CreateUserDataAccess -------------------------------------
        public static void CreateUserDataAccess(UserModel uM)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.CreateUserTSP(
                uM.id,
                uM.name,
                uM.username,
                uM.levelId,
                uM.desc,
                uM.email,
                uM.hashPass,
                uM.photoLink,
                uM.registeredTime,
                uM.onlineStatus,
                uM.phone,
                uM.genderID,
                uM.DOB,
                uM.address,
                uM.deptID,
                uM.memberStatus);
        }
        //--------------- DeactivateActivateUserDataAccess -------------------------------------
        public static void DeactivateActivateUserDataAccess(Guid id)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.DeactivateActivateUserTSP(id);
        }
        //--------------- GetUserDataAccess -------------------------------------
        public static GetUserTSP_Result GetUserDataAccess(Guid id)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            GetUserTSP_Result res = DatabaseEntity.GetUserTSP(id).FirstOrDefault();
            return res;
        }
        //--------------- GetUsersDataAccess -------------------------------------
        public static IEnumerable<GetUserTsSP_Result> GetUsersDataAccess()
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            IEnumerable<GetUserTsSP_Result> res = DatabaseEntity.GetUserTsSP().ToList();
            return res;
        }
        //--------------- UpdateUserDataAccess -------------------------------------
        public static void UpdateUserDataAccess(UserModel uM)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            DatabaseEntity.UpdateUserTSP(
                uM.id,
                uM.name,
                uM.username,
                uM.levelId,
                uM.desc,
                uM.email,
                uM.hashPass,
                uM.photoLink,
                uM.registeredTime,
                uM.onlineStatus,
                uM.phone,
                uM.genderID,
                uM.DOB,
                uM.address,
                uM.deptID,
                uM.memberStatus);
        }
        //--------------- LogUserDataAccess -------------------------------------
        public static LogUserSP_Result LogUserDataAccess(UserModel uM)
        {
            TaskManagementDBEntities DatabaseEntity = new TaskManagementDBEntities();
            LogUserSP_Result res = DatabaseEntity.LogUserSP(uM.email).FirstOrDefault();
            return res;
        }




    }
}
