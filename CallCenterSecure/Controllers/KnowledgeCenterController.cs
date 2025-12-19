using CallCenter.BusinessLogic;
using CallCenter.DataAccess;
using CallCenter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CallCenter.CustomAuthentication;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace CallCenter.Controllers
{
    
    public class KnowledgeCenterController : Controller
    {
        // GET: KnowedgeCenter
        public ActionResult Index()
        {
            KnowledgeCenterModel knowledgeCenterModel = new KnowledgeCenterModel();
            CategoryProvider categoryProvider = new CategoryProvider();
            knowledgeCenterModel.CategoryList = categoryProvider.GetAllCategory();
            knowledgeCenterModel.SubCategoryList = knowledgeCenterModel.CategoryList.Count > 0 ? categoryProvider.GetSubCategoryByCategoryId(knowledgeCenterModel.CategoryList[0].CategoryId) : new List<SubCategoryMaster>();
            knowledgeCenterModel.SubSubCategoryList = knowledgeCenterModel.SubCategoryList.Count > 0 ? categoryProvider.GetSubSubCategoryBySubCategoryId(knowledgeCenterModel.SubCategoryList[0].SubCategoryId) : new List<SubSubCategoryMaster>();
            return View(knowledgeCenterModel);
        }
        [HttpPost]
        public ActionResult Index(KnowledgeCenterModel knowledgeCenterModel)
        {
            bool statusTicket = false;
            string messageTicket = string.Empty;

            if (ModelState.IsValid)
            {

                //Checking file is available to save.  
                if (knowledgeCenterModel.File != null)
                {
                    var InputFileName = Path.GetFileName(knowledgeCenterModel.File.FileName);
                    knowledgeCenterModel.FileURL = Path.Combine(Server.MapPath("~/Uploads/") + InputFileName);
                    //Save file to server folder  
                    knowledgeCenterModel.File.SaveAs(knowledgeCenterModel.FileURL);
                    //assigning file uploaded status to ViewBag for showing message to user.  
                    //ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                }



                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var knowledgeCenter = new KnowledgeCenter()
                    {

                        CategoryId = knowledgeCenterModel.CategoryId,
                        SubCategoryId = knowledgeCenterModel.SubCategoryId,
                        SubSubCategoryId = knowledgeCenterModel.SubSubCategoryId,
                        Description = knowledgeCenterModel.Description,
                        FileURL = knowledgeCenterModel.FileURL
                    };
                    dbContext.KnowledgeCenters.Add(knowledgeCenter);
                    dbContext.SaveChanges();
                }

                //VerificationEmail(registrationView.Email, registrationView.ActivationCode.ToString());
                messageTicket = "Knowledge Center has been created successfully.";
                statusTicket = true;
                CategoryProvider categoryProvider = new CategoryProvider();
                knowledgeCenterModel = new KnowledgeCenterModel();
                knowledgeCenterModel.CategoryList = categoryProvider.GetAllCategory();
                knowledgeCenterModel.SubCategoryList = knowledgeCenterModel.CategoryList.Count > 0 ? categoryProvider.GetSubCategoryByCategoryId(knowledgeCenterModel.CategoryList[0].CategoryId) : new List<SubCategoryMaster>();
                knowledgeCenterModel.SubSubCategoryList = knowledgeCenterModel.SubCategoryList.Count > 0 ? categoryProvider.GetSubSubCategoryBySubCategoryId(knowledgeCenterModel.SubCategoryList[0].SubCategoryId) : new List<SubSubCategoryMaster>();
            }
            else
            {
                messageTicket = "Something Wrong!";
                CategoryProvider categoryProvider = new CategoryProvider();
                knowledgeCenterModel.CategoryList = categoryProvider.GetAllCategory();
                knowledgeCenterModel.SubCategoryList = categoryProvider.GetSubCategoryByCategoryId(Convert.ToInt32(knowledgeCenterModel.CategoryId));
                knowledgeCenterModel.SubSubCategoryList = categoryProvider.GetSubSubCategoryBySubCategoryId(Convert.ToInt32(knowledgeCenterModel.SubSubCategoryId));
            }
            ViewBag.Message = messageTicket;
            ViewBag.Status = statusTicket;
            return View(knowledgeCenterModel);

        }

        public ActionResult KnowledgeList()
        {

            KnowledgeCenterModel knowledgeCenterModel = new KnowledgeCenterModel();
            CategoryProvider categoryProvider = new CategoryProvider();
            knowledgeCenterModel.CategoryList = categoryProvider.GetAllCategory();
            knowledgeCenterModel.SubCategoryList = knowledgeCenterModel.CategoryList.Count > 0 ? categoryProvider.GetSubCategoryByCategoryId(knowledgeCenterModel.CategoryList[0].CategoryId) : new List<SubCategoryMaster>();
            knowledgeCenterModel.SubSubCategoryList = knowledgeCenterModel.SubCategoryList.Count > 0 ? categoryProvider.GetSubSubCategoryBySubCategoryId(knowledgeCenterModel.SubCategoryList[0].SubCategoryId) : new List<SubSubCategoryMaster>();
            knowledgeCenterModel.CategoryList.Insert(0, new CategoryMaster { CategoryId = 0, CategoryName = "Select Category" });
            knowledgeCenterModel.SubCategoryList.Insert(0, new SubCategoryMaster { SubCategoryId = 0, SubCategoryName = "Select Sub Category" });
            knowledgeCenterModel.SubSubCategoryList.Insert(0, new SubSubCategoryMaster { SubSubCategoryId = 0, SubSubCategoryName = "Select Sub Sub Category" });

            knowledgeCenterModel.KnowledgeList = categoryProvider.GetAllKnowledgeCenter();
            if (knowledgeCenterModel.KnowledgeList != null && knowledgeCenterModel.KnowledgeList.Count > 0)
            {
                foreach (var item in knowledgeCenterModel.KnowledgeList)
                {
                    var cat = categoryProvider.GetCategoryById(Convert.ToInt32(item.CategoryId));
                    if (cat != null)
                        item.CategoryId = cat.CategoryName;
                    var subcat = categoryProvider.GetSubCategoryById(Convert.ToInt32(item.SubCategoryId));
                    if (subcat != null)
                        item.SubCategoryId = subcat.SubCategoryName;
                    var subsubCategory = categoryProvider.GetSubSubCategoryById(Convert.ToInt32(item.SubSubCategoryId));
                    if (subsubCategory != null)
                        item.SubSubCategoryId = subsubCategory.SubSubCategoryName;
                }
            }
            if (User.IsInRole("Admin") || User.IsInRole("Supervisor"))
                ViewBag.IsAccess = true;
            else
                ViewBag.IsAccess = false;
            return View(knowledgeCenterModel);
        }
        [HttpPost]
        public ActionResult KnowledgeList(KnowledgeCenterModel knowledgeCenterModel)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", null);
            }
            //TicketManagementModel ticketManagementModel = new TicketManagementModel();
            CategoryProvider categoryProvider = new CategoryProvider();
            var knowledgeCenter = categoryProvider.GetAllKnowledgeCenter();

            if (knowledgeCenterModel.CategoryId != null && knowledgeCenterModel.CategoryId != "0")
                knowledgeCenter = knowledgeCenter.Where(tl => tl.CategoryId == knowledgeCenterModel.CategoryId).ToList();
            if (knowledgeCenterModel.SubCategoryId != null && knowledgeCenterModel.SubCategoryId != "0")
                knowledgeCenter = knowledgeCenter.Where(tl => tl.SubCategoryId == knowledgeCenterModel.SubCategoryId).ToList();
            if (knowledgeCenterModel.SubSubCategoryId != null && knowledgeCenterModel.SubSubCategoryId != "0")
                knowledgeCenter = knowledgeCenter.Where(tl => tl.SubSubCategoryId == knowledgeCenterModel.SubSubCategoryId).ToList();

            if (!string.IsNullOrEmpty(knowledgeCenterModel.Description))
                knowledgeCenter = knowledgeCenter.Where(tl => tl.Description.Contains(knowledgeCenterModel.Description)).ToList();

            knowledgeCenterModel.KnowledgeList = knowledgeCenter;

            if (knowledgeCenterModel.KnowledgeList != null && knowledgeCenterModel.KnowledgeList.Count > 0)
            {
                foreach (var item in knowledgeCenterModel.KnowledgeList)
                {
                    var cat = categoryProvider.GetCategoryById(Convert.ToInt32(item.CategoryId));
                    if (cat != null)
                        item.CategoryId = cat.CategoryName;
                    var subcat = categoryProvider.GetSubCategoryById(Convert.ToInt32(item.SubCategoryId));
                    if (subcat != null)
                        item.SubCategoryId = subcat.SubCategoryName;
                    var subsubCategory = categoryProvider.GetSubSubCategoryById(Convert.ToInt32(item.SubSubCategoryId));
                    if (subsubCategory != null)
                        item.SubSubCategoryId = subsubCategory.SubSubCategoryName;
                }
            }
            knowledgeCenterModel.CategoryList = categoryProvider.GetAllCategory();
            knowledgeCenterModel.SubCategoryList = knowledgeCenterModel.CategoryList.Count > 0 ? categoryProvider.GetSubCategoryByCategoryId(knowledgeCenterModel.CategoryList[0].CategoryId) : new List<SubCategoryMaster>();
            knowledgeCenterModel.SubSubCategoryList = knowledgeCenterModel.SubCategoryList.Count > 0 ? categoryProvider.GetSubSubCategoryBySubCategoryId(knowledgeCenterModel.SubCategoryList[0].SubCategoryId) : new List<SubSubCategoryMaster>();

            knowledgeCenterModel.CategoryList.Insert(0, new CategoryMaster { CategoryId = 0, CategoryName = "Select Category" });
            knowledgeCenterModel.SubCategoryList.Insert(0, new SubCategoryMaster { SubCategoryId = 0, SubCategoryName = "Select Sub Category" });
            knowledgeCenterModel.SubSubCategoryList.Insert(0, new SubSubCategoryMaster { SubSubCategoryId = 0, SubSubCategoryName = "Select Sub Sub Category" });

            if (User.IsInRole("Admin") || User.IsInRole("Supervisor"))
                ViewBag.IsAccess = true;
            else
                ViewBag.IsAccess = false;

            return View(knowledgeCenterModel);
        }

        public ActionResult KnowledgeEdit(int id)
        {
            KnowledgeCenterModel knowledgeCenterModel = new KnowledgeCenterModel();
            CategoryProvider categoryProvider = new CategoryProvider();
            KnowledgeCenter knowledgeCenter = new KnowledgeCenter();
            knowledgeCenter = categoryProvider.GetKnowledgeById(id);

            knowledgeCenterModel.KnowledgeId = knowledgeCenter.KnowledgeId;

            knowledgeCenterModel.CategoryId = knowledgeCenter.CategoryId;

            knowledgeCenterModel.SubCategoryId = knowledgeCenter.SubCategoryId;
            knowledgeCenterModel.SubSubCategoryId = knowledgeCenter.SubSubCategoryId;
            knowledgeCenterModel.Description = knowledgeCenter.Description;
            knowledgeCenterModel.FileURL = knowledgeCenter.FileURL;

            knowledgeCenterModel.CategoryList = categoryProvider.GetAllCategory();
            knowledgeCenterModel.SubCategoryList = knowledgeCenterModel.CategoryList.Count > 0 ? categoryProvider.GetSubCategoryByCategoryId(Convert.ToInt32(knowledgeCenterModel.CategoryId)) : new List<SubCategoryMaster>();
            knowledgeCenterModel.SubSubCategoryList = knowledgeCenterModel.SubCategoryList.Count > 0 ? categoryProvider.GetSubSubCategoryBySubCategoryId(Convert.ToInt32(knowledgeCenterModel.SubCategoryId)) : new List<SubSubCategoryMaster>();

            return View(knowledgeCenterModel);
        }


        [HttpPost]
        public ActionResult KnowledgeEdit(KnowledgeCenterModel knowledgeCenterModel)
        {

            bool statusTicket = false;
            string messageTicket = string.Empty;
            if (ModelState.IsValid)
            {

                if (knowledgeCenterModel.File != null)
                {
                    var InputFileName = Path.GetFileName(knowledgeCenterModel.File.FileName);
                    knowledgeCenterModel.FileURL = Path.Combine(Server.MapPath("~/Uploads/") + InputFileName);
                    //Save file to server folder  
                    knowledgeCenterModel.File.SaveAs(knowledgeCenterModel.FileURL);
                }

                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var result = dbContext.KnowledgeCenters.SingleOrDefault(m => m.KnowledgeId == knowledgeCenterModel.KnowledgeId);
                    if (result != null)
                    {
                        result.CategoryId = knowledgeCenterModel.CategoryId;
                        result.SubCategoryId = knowledgeCenterModel.SubCategoryId;
                        result.SubSubCategoryId = knowledgeCenterModel.SubSubCategoryId;
                        result.Description = knowledgeCenterModel.Description;
                        if (knowledgeCenterModel.File != null)
                            result.FileURL = knowledgeCenterModel.FileURL;

                        dbContext.SaveChanges();
                        messageTicket = "Knowledge has been updated successfully.";
                        statusTicket = true;

                    }
                }
                return RedirectToAction("KnowledgeList", "KnowledgeCenter");
            }
            else
            {
                messageTicket = "Something Wrong!";
                CategoryProvider categoryProvider = new CategoryProvider();
                knowledgeCenterModel.CategoryList = categoryProvider.GetAllCategory();
                knowledgeCenterModel.SubCategoryList = knowledgeCenterModel.CategoryList.Count > 0 ? categoryProvider.GetSubCategoryByCategoryId(Convert.ToInt32(knowledgeCenterModel.CategoryId)) : new List<SubCategoryMaster>();
                knowledgeCenterModel.SubSubCategoryList = knowledgeCenterModel.SubCategoryList.Count > 0 ? categoryProvider.GetSubSubCategoryBySubCategoryId(Convert.ToInt32(knowledgeCenterModel.SubCategoryId)) : new List<SubSubCategoryMaster>();

            }

            return View(knowledgeCenterModel);

        }

        public ActionResult KnowledgeDownloadExcel(string categoryid, string subcategoryid, string subsubcategoryid, string description) //string opendate, string closedate)
        {
            KnowledgeCenterModel knowledgeCenterModel = new KnowledgeCenterModel();

            if (!string.IsNullOrEmpty(categoryid))
                knowledgeCenterModel.CategoryId = categoryid;

            if (!string.IsNullOrEmpty(subcategoryid))
                knowledgeCenterModel.SubCategoryId = subcategoryid;

            if (!string.IsNullOrEmpty(subsubcategoryid))
                knowledgeCenterModel.SubSubCategoryId = subsubcategoryid;

            if (!string.IsNullOrEmpty(description))
                knowledgeCenterModel.Description = description;

            CategoryProvider categoryProvider = new CategoryProvider();
            var knowledgeCenter = categoryProvider.GetAllKnowledgeCenter();

            if (knowledgeCenterModel.CategoryId != null && knowledgeCenterModel.CategoryId != "0")
                knowledgeCenter = knowledgeCenter.Where(tl => tl.CategoryId == knowledgeCenterModel.CategoryId).ToList();
            if (knowledgeCenterModel.SubCategoryId != null && knowledgeCenterModel.SubCategoryId != "0")
                knowledgeCenter = knowledgeCenter.Where(tl => tl.SubCategoryId == knowledgeCenterModel.SubCategoryId).ToList();
            if (knowledgeCenterModel.SubSubCategoryId != null && knowledgeCenterModel.SubSubCategoryId != "0")
                knowledgeCenter = knowledgeCenter.Where(tl => tl.SubSubCategoryId == knowledgeCenterModel.SubSubCategoryId).ToList();

            if (!string.IsNullOrEmpty(knowledgeCenterModel.Description))
                knowledgeCenter = knowledgeCenter.Where(tl => tl.Description.Contains(knowledgeCenterModel.Description)).ToList();

            foreach (var item in knowledgeCenter)
            {
                var cat = categoryProvider.GetCategoryById(Convert.ToInt32(item.CategoryId));
                if (cat != null)
                    item.CategoryId = cat.CategoryName;
                var subcat = categoryProvider.GetSubCategoryById(Convert.ToInt32(item.SubCategoryId));
                if (subcat != null)
                    item.SubCategoryId = subcat.SubCategoryName;
                var subsubCategory = categoryProvider.GetSubSubCategoryById(Convert.ToInt32(item.SubSubCategoryId));
                if (subsubCategory != null)
                    item.SubSubCategoryId = subsubCategory.SubSubCategoryName;
            }

          

            var gv = new GridView();
            gv.DataSource = knowledgeCenter;
            gv.DataBind();
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            byte[] bindata = System.Text.Encoding.ASCII.GetBytes(objStringWriter.ToString());
            return File(bindata, "application/ms-excel", "KnowledgeListExcel.xls");
        }

    }


}