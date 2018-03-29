using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementDBLibrary.Model
{
    public class CreateUserModel
    {
        [Required]
        [MinLength(4)]
        public string name { get; set; }

        [Required]
        [DisplayName("User Name")]
        public string username { get; set; }

        public int? levelID { get; set; }

        [Required]
        [MinLength(10)]
        [DisplayName("Description")]
        public string desc { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string hashPass { get; set; }

        public string photoLink { get; set; }
        
        public DateTime? registeredTime { get; set; }

        public byte? onlineStatus { get; set; }

        [Phone]
        [Required]
        public string phone { get; set; }

        public int? gender { get; set; }

        public DateTime? dOB { get; set; }

        [Required]
        [MinLength(5)]
        public string address { get; set; }

        public int? deptID { get; set; }

        public byte? memberStatus { get; set; }
    }

   
}
