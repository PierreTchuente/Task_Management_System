using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementDBLibrary.Model
{
    public class AddMemberModel
    {
        public Guid userID { get; set; }
        public int deptID { get; set; }
    }
}
