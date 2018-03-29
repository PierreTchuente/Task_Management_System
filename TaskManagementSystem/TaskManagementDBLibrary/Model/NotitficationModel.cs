using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementDBLibrary.Model
{
    public class NotitficationModel
    {
        public Guid userID { get; set; } = new Guid();
        public Guid notiID { get; set; }
        public Guid sender_ID { get; set; }
        public byte typeID { get; set; }
        public string message { get; set; }
    }
}
