using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementDBLibrary.Model
{
    public class ProjectModel
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public bool isActive { get; set; }
        public Guid creatorID { get; set; }
        public DateTime expectedStartDate { get; set; }
        public DateTime expectedEndDate { get; set; }
        public DateTime actualStartDate { get; set; }
        public DateTime actualEndDate { get; set; }
        public bool isPrivate { get; set; }
    }
}
