using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementDBLibrary.Model
{
    public class OrderModel
    {
        public Guid id { get; set; }
        public Guid userID { get; set; }
        public DateTime dateReceived { get; set; }
        public DateTime dateProcessed { get; set; }
        public char status { get; set; }
    }
}
