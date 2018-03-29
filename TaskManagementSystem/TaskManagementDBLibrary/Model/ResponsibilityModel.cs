using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementDBLibrary.Model
{
    public class ResponsibilityModel
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int isActive { get; set; }
        public int deptID { get; set; }
    }
}
