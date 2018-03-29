using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementDBLibrary.Model
{
    public class UpdateEquipmentStatusModel
    {
        public int eqID { get; set; }
        public int statusID { get; set; }
        public Guid OrderEqID { get; set; }
        public DateTime time { get; set; }
    }
}
