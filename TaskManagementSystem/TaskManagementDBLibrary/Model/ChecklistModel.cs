using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementDBLibrary.Model
{
    public class ChecklistModel
    {
        public Guid? id { get; set; }
        public Guid? taskID { get; set; }
        public string desc { get; set; }
        public bool isCompleted { get; set; }
        public int weight { get; set; }
    }
}
