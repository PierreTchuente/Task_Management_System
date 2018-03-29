using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementDBLibrary.Model;

namespace TaskManagementDBLibrary.DataAccessLayer
{
    public static class DepartmentAccess
    {
        public static void UpdateDepartment(DepartmentModel model)
        {
            TaskManagementDBEntities Entities = new TaskManagementDBEntities();
            Entities.UpdatDepartmentSP(model.id, model.desc, model.hod);
        }

        public static void DeleteDepartment(int id)
        {
            TaskManagementDBEntities Entities = new TaskManagementDBEntities();
            Entities.DeleteDepartmentSP(id);
        }

   

        public static IEnumerable<GetCurrentDepartmentHODSP_Result> GetCurrentHOD(int id)
        {
            TaskManagementDBEntities Entities = new TaskManagementDBEntities();
            IEnumerable<GetCurrentDepartmentHODSP_Result> currentHOD = Entities.GetCurrentDepartmentHODSP(id);
            return currentHOD;
        }
    }
}
