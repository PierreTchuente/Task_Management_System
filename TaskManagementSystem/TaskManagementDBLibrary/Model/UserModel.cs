using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace TaskManagementDBLibrary.Model
{
    public class UserModel
    {
        public UserModel()
        {
            registeredTime = DateTime.UtcNow.AddHours(2);
            isActive = true;
            onlineStatus = 1;
            memberStatus = 1;
            photoLink = ""; // Later can be set to  a default picture path.
            username = "";
            DropDownDepartmenmt = new List<SelectListItem>();
            DropDownGender = new List<SelectListItem>();
            DropDownLevels = new List<SelectListItem>();

        }

        [HiddenInput(DisplayValue = false)]
        public Guid id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string hashPass { get; set; }

        [Required(ErrorMessage = "Full Name Field is Required")]
        [MinLength(4)]
        [DisplayName("Full Name")]
        public string name { get; set; }

        //[Required(ErrorMessage ="User Name Field is Required")]
        [DisplayName("User Name")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Reg Date")]
        public DateTime registeredTime { get; set; }

        [Required(ErrorMessage ="Phone Number Field is Required")]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Ph Number")]
        [RegularExpression("0((60[3-9]|64[0-5])\\d{6}|(7[1-4689]|6[1-3]|8[1-4])\\d{7})", ErrorMessage ="Invalid Phone Number")]
        public string phone { get; set; }

        [Required(ErrorMessage ="Please Select the User Gender")]
        [DisplayName("Gender")]
        public int genderID { get; set; } // Drop Down
        public List<SelectListItem> DropDownGender { get; set; }

        [DisplayName("Gender")]
        public string gender { get; set; }

        [DisplayName("User Level")]
        public string UserLevel { get; set; }

        [DisplayName("Department")]
        public string Department { get; set; }


        [DataType(DataType.Date)]
        [DisplayName("Date Of Birth")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Physycal Address Field is Required")]
        [DisplayName("Address")]
        public string address { get; set; }

        [Required(ErrorMessage = "Please chose The User  Level")]
        [DisplayName("User Level")]
        public int levelId { get; set; } // Drop Down
        public List<SelectListItem> DropDownLevels { get; set; }

        [Required(ErrorMessage ="Please chose a Department for the User")]
        [DisplayName("Department")]
        public int deptID { get; set; } // Drop Down
        public List<SelectListItem> DropDownDepartmenmt { get; set; }

        [DisplayName("Memeber Status")]
        public byte memberStatus { get; set; }

        [DisplayName("Activate")]  // think on how to set the default.
        public bool isActive { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [DisplayName("Description")]
        public string desc { get; set; }

        [DisplayName("oldDepartment")]
        public int olddeptID { get; set; } // when updating user.


        [DisplayName("oldLevel")]
        public int oldlevelID { get; set; } // when updating user.

        [DisplayName("Online Status")]
        public byte onlineStatus { get; set; } // what is the purpose of online status.


        [DataType(DataType.Upload)]
        public HttpPostedFileBase UserPicture { get; set; }

        public string photoLink { get; set; }

    }

}
