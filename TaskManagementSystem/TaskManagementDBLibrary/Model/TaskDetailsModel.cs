using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementDBLibrary.Model
{
    public class TaskDetailsModel
    {
        public GetTaskSP_Result taskDetails { get; set; }
        public List<ListOfUsersAssignedToTaskIDSP_Result> users { get; set; }
        public List<GetChecklistsPerTaskSP_Result> checkLists { get; set; }

        //MANUELLA ADDED
        public List<GetCommentsPerTaskSP_Result> comments { get; set; }
    }
}
