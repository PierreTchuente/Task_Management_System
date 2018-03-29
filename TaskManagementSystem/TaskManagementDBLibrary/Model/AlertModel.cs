using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementDBLibrary.Model
{
    public class AlertModel
    {
        public Guid alertID { get; set; } = new Guid();
        public Guid alertCreatedByID { get; set; }
        public string alertDescription { get; set; }
    }
}
