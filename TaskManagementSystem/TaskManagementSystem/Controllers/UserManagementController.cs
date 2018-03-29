using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Script.Serialization;
using System.Collections.Generic;


using TaskManagementDBLibrary;
using TaskManagementDBLibrary.DataAccessLayer;
using TaskManagementDBLibrary.Model;
using TaskManagementSystem.Models;
using System.IO;
using System.Net;
using System.Drawing.Imaging;
using System.Drawing;
using System.Web.UI;

namespace TaskManagementSystem.Controllers
{
    [Authorize]
    public class UserManagementController : Controller
    {
        #region Object declared to manage the user Registartion and login

        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

        public UserManagementController()
        {
        }

        public UserManagementController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        #endregion

        // GET: UserManagement/Details/5
        [OutputCache(Duration = 172800, VaryByParam = "id", Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserManagement/Details/5
        [OutputCache(Duration = 172800, VaryByParam = "id", Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult ViewProfile(Guid id)
        {
            ViewBag.UserDetails = Session["UserDetails"];
            ViewBag.calendarDate = Session["calendarDate"];

            var User = UserManagementAccess.GetUserTSP(id).FirstOrDefault();
            UserModel model = new UserModel();
            model.id = id;//User.USERT_ID;
            model.name = User.USERT_Name;
            model.email = User.USERT_Email;
            model.phone = User.USERT_PhoneNumber;
            model.photoLink = User.USERT_PhotoLink;
            model.address = User.USERT_Address;
            model.DOB = (DateTime)User.USERT_DOB;
            model.deptID = User.DEPT_ID;
            model.Department = User.DEPT_Desc;
            model.genderID = (int)User.GENDER_ID;
            model.gender = User.GENDER_Desc;
            model.UserLevel = User.LEVELT_Desc;
            model.registeredTime = (DateTime)User.USERT_Registered_Time;
            model.onlineStatus = (byte)User.USERT_OnlineStatus;
            model.memberStatus = (byte)User.USERT_MemberStatus;

            return View(model);
        }

        // GET: UserManagement/Create
        [Authorize]
        [OutputCache(Duration = 172800, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult Create()
        {
            ViewBag.UserDetails = Session["UserDetails"];
            ViewBag.calendarDate = Session["calendarDate"];

            UserModel newUserModel = new UserModel();
            var ListDepartment = LoadDropDown.ListDepartment;
            var ListLevels = LoadDropDown.ListLevels;
            var ListGender = LoadDropDown.ListGender;

            foreach (var item in ListDepartment)
            {
                newUserModel.DropDownDepartmenmt.Add(new SelectListItem { Value = item.DEPT_ID.ToString(), Text = item.DEPT_Desc });
            }

            foreach (var item in ListLevels)
            {
                newUserModel.DropDownLevels.Add(new SelectListItem { Value = item.LEVELT_ID.ToString(), Text = item.LEVELT_Desc });
            }

            foreach (var item in ListGender)
            {
                newUserModel.DropDownGender.Add(new SelectListItem { Value = item.GENDER_ID.ToString(), Text = item.GENDER_Desc });
            }
            return View(newUserModel);
        }

        [Authorize]
        //[OutputCache(Duration = 172800, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult ManageUser()
        {

            ViewBag.UserDetails = Session["UserDetails"];
            ViewBag.calendarDate = Session["calendarDate"];

            return View();
        }

        [HttpPost]
        public HttpStatusCodeResult DeactivateActivateUser(Guid id)
        {
            try
            {
                UserManagementAccess.DeactivateActivateUserMethods(id);
                return new HttpStatusCodeResult(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST: UserManagement/Create
        [HttpPost]
        public async Task<ActionResult> Create(UserModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newUser = new ApplicationUser { UserName = user.email, Email = user.email };

                    var result = await UserManager.CreateAsync(newUser, "0000");  // Create the user in the Memebership Database
                    if (result.Succeeded)
                    {
                        user.hashPass = newUser.PasswordHash;
                        user.id = Guid.Parse(newUser.Id);
                        user.username = user.name;

                        if (user.UserPicture != null && user.UserPicture.ContentLength > 0)
                        {
                            var uploadDir = "~/TaskManagementImages/" + newUser.Id;
                            Directory.CreateDirectory(Server.MapPath(uploadDir));
                            var imagePath = Path.Combine(Server.MapPath(uploadDir), user.UserPicture.FileName);
                            user.photoLink = user.UserPicture.FileName;
                            user.UserPicture.SaveAs(imagePath);
                            UserManagementAccess.CreateUserMethods(user); //Replicate the user in the TaskManagementDB
                        }
                        else
                        {
                            UserManagementAccess.CreateUserMethods(user); //Replicate the user in the TaskManagementDB
                        }
                        return RedirectToAction("Create", "UserManagement");
                    }

                    var ListDepartment = LoadDropDown.ListDepartment;
                    var ListLevels = LoadDropDown.ListLevels;
                    var ListGender = LoadDropDown.ListGender;

                    foreach (var item in ListDepartment)
                    {
                        user.DropDownDepartmenmt.Add(new SelectListItem { Value = item.DEPT_ID.ToString(), Text = item.DEPT_Desc });
                    }

                    foreach (var item in ListLevels)
                    {
                        user.DropDownLevels.Add(new SelectListItem { Value = item.LEVELT_ID.ToString(), Text = item.LEVELT_Desc });
                    }

                    foreach (var item in ListGender)
                    {
                        user.DropDownGender.Add(new SelectListItem { Value = item.GENDER_ID.ToString(), Text = item.GENDER_Desc });
                    }

                    AddErrors(result);
                    return View(user);
                }
                catch (Exception ex)
                {
                    LogMessageError(ex.Message);
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                var ListDepartment = LoadDropDown.ListDepartment;
                var ListLevels = LoadDropDown.ListLevels;
                var ListGender = LoadDropDown.ListGender;

                foreach (var item in ListDepartment)
                {
                    user.DropDownDepartmenmt.Add(new SelectListItem { Value = item.DEPT_ID.ToString(), Text = item.DEPT_Desc });
                }

                foreach (var item in ListLevels)
                {
                    user.DropDownLevels.Add(new SelectListItem { Value = item.LEVELT_ID.ToString(), Text = item.LEVELT_Desc });
                }

                foreach (var item in ListGender)
                {
                    user.DropDownGender.Add(new SelectListItem { Value = item.GENDER_ID.ToString(), Text = item.GENDER_Desc });
                }
                return View(user);
            }
        }

        [HttpGet]
        public JsonResult UpdateRole(int DeptiD, Guid SuperUserID, Guid UserID)
        {
            return Json(UserManagementAccess.UpdateRole(DeptiD, SuperUserID, UserID), JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        public HttpStatusCodeResult EditUser(UserModel user)
        {
            try
            {
                UserManagementAccess.UpdateUserMethods(user);
                //he became hod
                if(user.levelId == 3)
                {
                    UserManagementAccess.UpdatDepartmentSP(user.deptID, user.Department, user.id);
                }

                //he became hod of a new department
                if(user.oldlevelID == 3 && user.olddeptID != user.deptID)
                {
                    UserManagementAccess.UpdateRole(user.olddeptID, Guid.Parse("ECB4D29B-D391-4AC1-A5E6-967C2892F683"), user.id);
                    //UserManagementAccess.UpdatDepartmentSP(user.deptID, user.Department, Guid.Parse("B31DAA0A-B2E6-43B9-9964-B32D51C73523"));
                }

                //trying to put old hod as normal user
                if (user.oldlevelID == 3 && user.levelId == 1004)
                {
                    UserManagementAccess.UpdateRole(user.olddeptID, Guid.Parse("ECB4D29B-D391-4AC1-A5E6-967C2892F683"), user.id);
                    //UserManagementAccess.UpdatDepartmentSP(user.deptID, user.Department, Guid.Parse("B31DAA0A-B2E6-43B9-9964-B32D51C73523"));
                }

                return new HttpStatusCodeResult(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPost]
        public HttpStatusCodeResult UpdateUserPicture()
        {
            try
            {
                HttpPostedFileBase files = Request.Files["imageFile"];
                if (files != null && files.ContentLength > 0)
                {
                    Guid id = Guid.Parse(User.Identity.GetUserId());
                    var user = UserManagementAccess.GetUserTSP(id).FirstOrDefault();

                    var filePath = Server.MapPath("~/TaskManagementImages/" + user.USERT_ID + "/" + user.USERT_PhotoLink);

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    var uploadDir = "~/TaskManagementImages/" + User.Identity.GetUserId();
                    Directory.CreateDirectory(Server.MapPath(uploadDir));
                    string pictName = files.FileName;
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), pictName);
                    //user.photoLink = user.UserPicture.FileName;
                    //files.SaveAs(imagePath);
                    CompressAndSaveImage(files, imagePath);

                    UserManagementAccess.UpdateUserPictName(user.USERT_ID, pictName);
                    user.USERT_PhotoLink = pictName;
                    Session["UserDetails"] = new JavaScriptSerializer().Serialize(user);

                }

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        // Search users.
        [HttpGet]
        public JsonResult UserFullTextSearch(string inputText)
        {
            return Json(UserManagementAccess.UserFullTextMethods(inputText), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        //[OutputCache(Duration = 172800, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public JsonResult GetListOfUsers()
        {
            return Json(MainTableAccess.GetUsersDataAccess(), JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        // Helper function to add error in case a user registration fails.
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private void LogMessageError(string message)
        {
            using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(System.Web.Hosting.HostingEnvironment.MapPath("~/logs/log.txt"), true))
            {
                file.WriteLine(message);
            }
        }


        private void CompressAndSaveImage(HttpPostedFileBase files, string filePath)
        {           
            //Load image
            System.Drawing.Image imgFullSize = System.Drawing.Image.FromStream(files.InputStream);
            //Create Encoder Parameter and specify the quality
            EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, 25L);
            // Create an EncoderParameters object.
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            //imgFullSize.Save(Response.OutputStream, jpegCodec, encoderParams);
            ImageCodecInfo encoder = GetEncoder(ImageFormat.Jpeg);
            imgFullSize.Save(filePath, encoder, encoderParams);
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
