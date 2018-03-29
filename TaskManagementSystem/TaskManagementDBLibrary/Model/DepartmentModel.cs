using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementDBLibrary.Model
{
    public class DepartmentModel
    {
        public int id { get; set; }
        public string desc { get; set; }
        public Guid hod { get; set; }
    }
}
