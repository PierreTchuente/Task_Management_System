using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementDBLibrary.Model
{
    public class CommentModel
    {
        public Guid id { get; set; }
        public Guid taskID { get; set; }
        public Guid userID { get; set; }
        public string desc { get; set; }
        public DateTime datePosted { get; set; }
    }
}
