using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementDBLibrary.Model
{
    public class EquipmentModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public int? quantity { get; set; }
        public int? quantityOrdered { get; set; }
        public bool? isActive { get; set; }
        public bool? isApproved { get; set; }
    }
}
