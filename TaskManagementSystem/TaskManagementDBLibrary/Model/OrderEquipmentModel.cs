using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementDBLibrary.Model
{
    public class OrderEquipmentModel
    {
        public Guid? id { get; set; }
        public int? equipmentID { get; set; }
        public Guid userID { get; set; }
        public int? qtyOrdered { get; set; }
        public DateTime date { get; set; }
        public bool? isReturned { get; set; }
        public string comment { get; set; }
        public bool? isDeleted { get; set; }
    }
}
