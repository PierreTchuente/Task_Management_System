using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementDBLibrary.Model
{
    public class DeptDetailsModel
    {
        public GetDepartmentSP_Result deptDetails { get; set; }
        public List<GetResponsibilitiesPerDeptSP_Result> responsibilities { get; set; }
        public List<GetOtherMembersInDeptRespSP_Result> otherMembers { get; set; }
    }
}
