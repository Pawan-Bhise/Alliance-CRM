using CallCenter.BusinessLogic;
using CallCenter.DataAccess;
using CallCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CallCenter.CustomAuthentication;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using CallCenterSecure.BusinessLogic;

namespace CallCenter.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        #region Category
        public ActionResult CategoryMaster()
        {
            CategoryMasterModel categoryMasterModel = new CategoryMasterModel();
            CategoryProvider categoryProvider = new CategoryProvider();
            categoryMasterModel.CategoryList = categoryProvider.GetAllCategory();
            return View(categoryMasterModel);
        }
        public ActionResult CategoryMasterAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CategoryMasterAdd(CategoryMasterModel categoryMasterModel)
        {
            bool status = false;
            string messageCateory = string.Empty;

            if (ModelState.IsValid)
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var categoryMaster = new CategoryMaster()
                    {
                        CategoryName = categoryMasterModel.CategoryName
                    };
                    dbContext.CategoryMasters.Add(categoryMaster);
                    dbContext.SaveChanges();
                }
                messageCateory = "Category has been created successfully.";
                status = true;
                categoryMasterModel.CategoryName = "";
            }
            else
            {
                messageCateory = "Something Wrong!";
            }
            ViewBag.Status = status;
            ViewBag.Message = messageCateory;
            return View(categoryMasterModel);
        }
        public ActionResult CategoryMasterEdit(int Id)
        {
            CategoryMasterModel categoryMasterModel = new CategoryMasterModel();
            CategoryProvider categoryProvider = new CategoryProvider();
            var category = categoryProvider.GetCategoryById(Id);
            if (category != null)
            {
                categoryMasterModel.CategoryId = category.CategoryId;
                categoryMasterModel.CategoryName = category.CategoryName;
            }

            return View(categoryMasterModel);
        }
        [HttpPost]
        public ActionResult CategoryMasterEdit(CategoryMasterModel categoryMasterModel)
        {
            bool status = false;
            string messageCateory = string.Empty;

            if (ModelState.IsValid)
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var result = dbContext.CategoryMasters.SingleOrDefault(m => m.CategoryId == categoryMasterModel.CategoryId);
                    if (result != null)
                    {
                        result.CategoryName = categoryMasterModel.CategoryName;
                        dbContext.SaveChanges();
                        messageCateory = "Category has been updated successfully.";
                        status = true;
                        categoryMasterModel.CategoryName = "";
                    }
                }

            }
            else
            {
                messageCateory = "Something Wrong!";
            }
            ViewBag.Status = status;
            ViewBag.Message = messageCateory;
            return RedirectToAction("CategoryMaster", "Admin");
        }
        public ActionResult CategoryMasterDelete(int Id)
        {
            bool status = false;
            string messageCateory = string.Empty;

            if (ModelState.IsValid)
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var result = dbContext.CategoryMasters.SingleOrDefault(m => m.CategoryId == Id);
                    if (result != null)
                    {
                        dbContext.CategoryMasters.Remove(result);
                        dbContext.SaveChanges();
                        messageCateory = "Category has been deleted successfully.";
                        status = true;
                    }
                }

            }
            else
            {
                messageCateory = "Something Wrong!";
            }
            ViewBag.Status = status;
            ViewBag.Message = messageCateory;
            return RedirectToAction("CategoryMaster", "Admin");
        }

        #endregion

        #region SubCategory
        public ActionResult SubCategoryMaster()
        {
            SubCategoryMasterModel subCategoryMasterModel = new SubCategoryMasterModel();
            CategoryProvider categoryProvider = new CategoryProvider();
            subCategoryMasterModel.SubCategoryList = categoryProvider.GetAllSubCategoryWithCategory();
            return View(subCategoryMasterModel);
        }
        public ActionResult SubCategoryMasterAdd()
        {
            SubCategoryMasterModel subCategoryMasterModel = new SubCategoryMasterModel();
            CategoryProvider categoryProvider = new CategoryProvider();
            subCategoryMasterModel.CategoryList = categoryProvider.GetAllCategory();
            return View(subCategoryMasterModel);
        }
        [HttpPost]
        public ActionResult SubCategoryMasterAdd(SubCategoryMasterModel subCategoryMasterModel)
        {
            bool status = false;
            string messageCateory = string.Empty;

            if (ModelState.IsValid)
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var subCategoryMaster = new SubCategoryMaster()
                    {
                        SubCategoryName = subCategoryMasterModel.SubCategoryName,
                        CategoryId = subCategoryMasterModel.CategoryId
                    };
                    dbContext.SubCategoryMasters.Add(subCategoryMaster);
                    dbContext.SaveChanges();
                }
                messageCateory = "SubCategory has been created successfully.";
                status = true;
                subCategoryMasterModel.SubCategoryName = "";
                CategoryProvider categoryProvider = new CategoryProvider();
                subCategoryMasterModel.CategoryList = categoryProvider.GetAllCategory();
            }
            else
            {
                messageCateory = "Something Wrong!";
            }
            ViewBag.Status = status;
            ViewBag.Message = messageCateory;
            return View(subCategoryMasterModel);
        }
        public ActionResult SubCategoryMasterEdit(int Id)
        {
            SubCategoryMasterModel subCategoryMasterModel = new SubCategoryMasterModel();
            CategoryProvider categoryProvider = new CategoryProvider();
            var subCategory = categoryProvider.GetSubCategoryById(Id);
            if (subCategory != null)
            {
                subCategoryMasterModel.SubCategoryId = subCategory.SubCategoryId;
                subCategoryMasterModel.SubCategoryName = subCategory.SubCategoryName;
                subCategoryMasterModel.CategoryId = subCategory.CategoryId;
            }
            subCategoryMasterModel.CategoryList = categoryProvider.GetAllCategory();
            return View(subCategoryMasterModel);
        }
        [HttpPost]
        public ActionResult SubCategoryMasterEdit(SubCategoryMasterModel subCategoryMasterModel)
        {
            bool status = false;
            string messageCateory = string.Empty;

            if (ModelState.IsValid)
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var result = dbContext.SubCategoryMasters.SingleOrDefault(m => m.CategoryId == subCategoryMasterModel.SubCategoryId);
                    if (result != null)
                    {
                        result.CategoryName = subCategoryMasterModel.SubCategoryName;
                        result.CategoryId = subCategoryMasterModel.CategoryId;
                        dbContext.SaveChanges();
                        messageCateory = "Category has been updated successfully.";
                        status = true;
                        subCategoryMasterModel.CategoryName = "";
                    }
                }
            }
            else
            {
                messageCateory = "Something Wrong!";
            }
            ViewBag.Status = status;
            ViewBag.Message = messageCateory;
            return RedirectToAction("SubCategoryMaster", "Admin");
        }
        public ActionResult SubCategoryMasterDelete(int Id)
        {
            bool status = false;
            string messageCateory = string.Empty;

            if (ModelState.IsValid)
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var result = dbContext.SubCategoryMasters.SingleOrDefault(m => m.SubCategoryId == Id);
                    if (result != null)
                    {
                        dbContext.SubCategoryMasters.Remove(result);
                        dbContext.SaveChanges();
                        messageCateory = "SubCategory has been deleted successfully.";
                        status = true;
                    }
                }

            }
            else
            {
                messageCateory = "Something Wrong!";
            }
            ViewBag.Status = status;
            ViewBag.Message = messageCateory;
            return RedirectToAction("SubCategoryMaster", "Admin");
        }
        #endregion


        #region SubSubCategory

        public ActionResult SubSubCategoryMaster()
        {
            SubSubCategoryMasterModel subSubCategoryMasterModel = new SubSubCategoryMasterModel();
            CategoryProvider categoryProvider = new CategoryProvider();
            subSubCategoryMasterModel.SubSubCategoryList = categoryProvider.GetAllSubSubCategoryWithSubCategory();
            return View(subSubCategoryMasterModel);
        }

        public ActionResult SubSubCategoryMasterAdd()
        {
            SubSubCategoryMasterModel subSubCategoryMasterModel = new SubSubCategoryMasterModel();
            CategoryProvider categoryProvider = new CategoryProvider();
            subSubCategoryMasterModel.SubCategoryList = categoryProvider.GetAllSubCategory();
            return View(subSubCategoryMasterModel);
        }
        [HttpPost]
        public ActionResult SubSubCategoryMasterAdd(SubSubCategoryMasterModel subSubCategoryMasterModel)
        {
            bool status = false;
            string messageCateory = string.Empty;

            if (ModelState.IsValid)
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var subSubCategoryMaster = new SubSubCategoryMaster()
                    {
                        SubSubCategoryName = subSubCategoryMasterModel.SubSubCategoryName,
                        SubCategoryId = subSubCategoryMasterModel.SubCategoryId
                    };
                    dbContext.SubSubCategoryMasters.Add(subSubCategoryMaster);
                    dbContext.SaveChanges();
                }
                messageCateory = "Sub Sub Category has been created successfully.";
                status = true;
                subSubCategoryMasterModel.SubCategoryName = "";
                CategoryProvider categoryProvider = new CategoryProvider();
                subSubCategoryMasterModel.SubCategoryList = categoryProvider.GetAllSubCategory();
            }
            else
            {
                messageCateory = "Something Wrong!";
            }
            ViewBag.Status = status;
            ViewBag.Message = messageCateory;
            return View(subSubCategoryMasterModel);
        }

        public ActionResult SubSubCategoryMasterEdit(int Id)
        {
            SubSubCategoryMasterModel subSubCategoryMasterModel = new SubSubCategoryMasterModel();
            CategoryProvider categoryProvider = new CategoryProvider();
            var subSubCategory = categoryProvider.GetSubSubCategoryById(Id);
            if (subSubCategory != null)
            {
                subSubCategoryMasterModel.SubSubCategoryId = subSubCategory.SubSubCategoryId;
                subSubCategoryMasterModel.SubSubCategoryName = subSubCategory.SubSubCategoryName;
                subSubCategoryMasterModel.SubCategoryId = subSubCategory.SubCategoryId;
            }
            subSubCategoryMasterModel.SubCategoryList = categoryProvider.GetAllSubCategory();
            return View(subSubCategoryMasterModel);
        }
        [HttpPost]
        public ActionResult SubSubCategoryMasterEdit(SubSubCategoryMasterModel subSubCategoryMasterModel)
        {
            bool status = false;
            string messageCateory = string.Empty;

            if (ModelState.IsValid)
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var result = dbContext.SubSubCategoryMasters.SingleOrDefault(m => m.SubSubCategoryId == subSubCategoryMasterModel.SubSubCategoryId);
                    if (result != null)
                    {
                        result.SubSubCategoryName = subSubCategoryMasterModel.SubSubCategoryName;
                        result.SubCategoryId = subSubCategoryMasterModel.SubCategoryId;
                        dbContext.SaveChanges();
                        messageCateory = "Sub Sub Category has been updated successfully.";
                        status = true;
                        subSubCategoryMasterModel.SubSubCategoryName = "";
                    }
                }
            }
            else
            {
                messageCateory = "Something Wrong!";
            }
            ViewBag.Status = status;
            ViewBag.Message = messageCateory;
            return RedirectToAction("SubSubCategoryMaster", "Admin");
        }
        public ActionResult SubSubCategoryMasterDelete(int Id)
        {
            bool status = false;
            string messageCateory = string.Empty;

            if (ModelState.IsValid)
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var result = dbContext.SubSubCategoryMasters.SingleOrDefault(m => m.SubSubCategoryId == Id);
                    if (result != null)
                    {
                        dbContext.SubSubCategoryMasters.Remove(result);
                        dbContext.SaveChanges();
                        messageCateory = "SubSubCategory has been deleted successfully.";
                        status = true;
                    }
                }

            }
            else
            {
                messageCateory = "Something Wrong!";
            }
            ViewBag.Status = status;
            ViewBag.Message = messageCateory;
            return RedirectToAction("SubSubCategoryMaster", "Admin");
        }

        #endregion

        [HttpGet]
        public ActionResult Registration()
        {
            RegistrationViewModel registrationViewModel = new RegistrationViewModel();
            CustomRoleProvider customRoleProvider = new CustomRoleProvider();
            registrationViewModel.RoleList = customRoleProvider.GetAllRoles();
            return View(registrationViewModel);
        }
        [HttpPost]
        public ActionResult Registration(RegistrationViewModel registrationView)
        {
            bool statusRegistration = false;
            string messageRegistration = string.Empty;
            if (ModelState.IsValid)
            {

                string userName = Membership.GetUserNameByEmail(registrationView.Email);
                if (!string.IsNullOrEmpty(userName))
                {
                    ModelState.AddModelError("Warning Email", "Sorry: Email already Exists");
                    return View(registrationView);
                }
                registrationView.ActivationCode = Convert.ToString(Guid.NewGuid());
                //Save User Data 
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var user = new User()
                    {
                        Username = registrationView.Username,
                        FirstName = registrationView.FirstName,
                        LastName = registrationView.LastName,
                        Email = registrationView.Email,
                        Password = EncryptionProvider.Encrypt(registrationView.Password),
                        RoleId = registrationView.RoleId,
                        IsActive = true,
                        ActivationCode = new Guid(registrationView.ActivationCode),
                    };
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                }

                //VerificationEmail(registrationView.Email, registrationView.ActivationCode.ToString());
                messageRegistration = "Your account has been created successfully.";
                statusRegistration = true;
            }
            else
            {
                messageRegistration = "Something Wrong!";
                CustomRoleProvider customRoleProvider = new CustomRoleProvider();
                registrationView.RoleList = customRoleProvider.GetAllRoles();
            }
            ViewBag.Message = messageRegistration;
            ViewBag.Status = statusRegistration;
            return View(registrationView);
        }


        

        public ActionResult UserList()
        {
            RegistrationViewModel registrationViewModel = new RegistrationViewModel();
            CustomRoleProvider customRoleProvider = new CustomRoleProvider();
            CategoryProvider categoryProvider = new CategoryProvider();

            registrationViewModel.UsersList = categoryProvider.GetAllUsers();
            //registrationViewModel.RoleList = customRoleProvider.GetAllRoles();

            foreach (var item in registrationViewModel.UsersList)
            {
                item.Password = customRoleProvider.GetRoleNameById(item.RoleId);
            }

            return View(registrationViewModel);
        }

        public ActionResult UserEdit(int id)
        {
            RegistrationViewModel registrationViewModel = new RegistrationViewModel();
            CategoryProvider categoryProvider = new CategoryProvider();
            CustomRoleProvider customRoleProvider = new CustomRoleProvider();
            User user = new User();
            user = categoryProvider.GetUserById(id);

            registrationViewModel.UserId = user.UserId;

            registrationViewModel.FirstName = user.FirstName;
            registrationViewModel.LastName = user.LastName;
            registrationViewModel.Username = user.Username;
            registrationViewModel.RoleId = user.RoleId;
            registrationViewModel.Email = user.Email;
            registrationViewModel.RoleList = customRoleProvider.GetAllRoles();
            return View(registrationViewModel);
        }

        [HttpPost]
        public ActionResult UserEdit(RegistrationViewModel registrationViewModel)
        {

            CustomRoleProvider customRoleProvider = new CustomRoleProvider();
            string messageTicket = string.Empty;
            
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var result = dbContext.Users.SingleOrDefault(m => m.UserId == registrationViewModel.UserId);
                    if (result != null)
                    {
                        result.FirstName = registrationViewModel.FirstName;
                        result.LastName = registrationViewModel.LastName;
                        result.Email = registrationViewModel.Email;
                        result.RoleId = registrationViewModel.RoleId;
                        result.Username = registrationViewModel.Username;
                        dbContext.SaveChanges();
                        messageTicket = "User has been updated successfully.";
                    }
                }
                return RedirectToAction("UserList", "Admin");
        }

        public ActionResult UserDelete(int id)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var result = dbContext.Users.SingleOrDefault(m => m.UserId == id);
                if (result != null)
                {
                    result.IsActive = false; ;
                    dbContext.SaveChanges();
                }
            }
            return RedirectToAction("UserList", "Admin");

        }

        public ActionResult ToggleActive(int id)
        {

            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var user = dbContext.Users.FirstOrDefault(x => x.UserId == id);

                if (user == null)
                    return HttpNotFound();

                user.IsActive = !user.IsActive; // toggle

                dbContext.SaveChanges();
            }

            

            return RedirectToAction("UserList"); // reload grid
        }
    }
}