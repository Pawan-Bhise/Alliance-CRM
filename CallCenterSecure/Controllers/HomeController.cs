using CallCenter.BusinessLogic;
using CallCenter.CustomAuthentication;
using CallCenter.DataAccess;
using CallCenter.Models;
using CallCenterSecure.BusinessLogic;
using CallCenterSecure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CallCenter.Controllers
{
    [CustomAuthorize(Roles = "Admin|Supervisor|Agent")]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            User.IsInRole("Admin");

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", null);
            }
            return View();
        }
        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("Cookie1", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
        }


        public ActionResult PieChart(string filter)
        {
            CategoryProvider categoryProvider = new CategoryProvider();
            var ticket = categoryProvider.GetAllTickets();
            var sr = categoryProvider.GetAllSRs();
            ChartSRTicketModel chartSRTicketModel = new ChartSRTicketModel();

            if (filter == "today")
            {
                //chartSRTicketModel.TotalSR = sr.Where(s => Convert.ToDateTime(s.TicketOpenDate).Date == Convert.ToDateTime(DateTime.Now).Date).Count(); ;
                chartSRTicketModel.OpenSR = sr.Where(s => Convert.ToDateTime(s.TicketOpenDate).Date == Convert.ToDateTime(DateTime.Now).Date).Count();
                chartSRTicketModel.InProgressSR = sr.Where(s => s.Status == "InProgress" && Convert.ToDateTime(s.TicketOpenDate).Date == Convert.ToDateTime(DateTime.Now).Date).Count();
                chartSRTicketModel.CloseSR = sr.Where(s => s.Status == "Closed" && Convert.ToDateTime(s.TicketCloseDate).Date == Convert.ToDateTime(DateTime.Now).Date).Count();
                chartSRTicketModel.TotalSR = chartSRTicketModel.OpenSR + chartSRTicketModel.InProgressSR + chartSRTicketModel.CloseSR;

                chartSRTicketModel.TotalTicket = ticket.Where(s => Convert.ToDateTime(s.TicketOpenDate).Date == Convert.ToDateTime(DateTime.Now).Date).Count();
                chartSRTicketModel.OpenTicket = ticket.Where(s => Convert.ToDateTime(s.TicketOpenDate).Date == Convert.ToDateTime(DateTime.Now).Date).Count();
                chartSRTicketModel.InProgressTicket = ticket.Where(s => s.Status == "InProgress" && Convert.ToDateTime(s.TicketOpenDate).Date == Convert.ToDateTime(DateTime.Now).Date).Count();
                chartSRTicketModel.CloseTicket = ticket.Where(s => s.Status == "Closed" && Convert.ToDateTime(s.TicketCloseDate).Date == Convert.ToDateTime(DateTime.Now).Date).Count();
                chartSRTicketModel.TotalTicket = chartSRTicketModel.OpenTicket + chartSRTicketModel.InProgressTicket + chartSRTicketModel.CloseTicket;

            }
            else if (filter == "yesterday")
            {
                chartSRTicketModel.OpenSR = sr.Where(s => Convert.ToDateTime(s.TicketOpenDate).Date == Convert.ToDateTime(DateTime.Now).Date.AddDays(-1)).Count();
                chartSRTicketModel.InProgressSR = sr.Where(s => s.Status == "InProgress" && Convert.ToDateTime(s.TicketOpenDate).Date == Convert.ToDateTime(DateTime.Now).Date.AddDays(-1)).Count();
                chartSRTicketModel.CloseSR = sr.Where(s => s.Status == "Closed" && Convert.ToDateTime(s.TicketCloseDate).Date == Convert.ToDateTime(DateTime.Now).Date.AddDays(-1)).Count();
                chartSRTicketModel.TotalSR = chartSRTicketModel.OpenSR + chartSRTicketModel.InProgressSR + chartSRTicketModel.CloseSR;

                chartSRTicketModel.OpenTicket = ticket.Where(s => s.Status == "Open" && Convert.ToDateTime(s.TicketOpenDate).Date == Convert.ToDateTime(DateTime.Now).Date.AddDays(-1)).Count();
                chartSRTicketModel.InProgressTicket = ticket.Where(s => s.Status == "InProgress" && Convert.ToDateTime(s.TicketOpenDate).Date == Convert.ToDateTime(DateTime.Now).Date.AddDays(-1)).Count();
                chartSRTicketModel.CloseTicket = ticket.Where(s => s.Status == "Closed" && Convert.ToDateTime(s.TicketCloseDate).Date == Convert.ToDateTime(DateTime.Now).Date.AddDays(-1)).Count();
                chartSRTicketModel.TotalTicket = chartSRTicketModel.OpenTicket + chartSRTicketModel.InProgressTicket + chartSRTicketModel.CloseTicket;
            }
            else if (filter == "lastmonth")
            {
                chartSRTicketModel.OpenSR = sr.Where(s => Convert.ToDateTime(s.TicketOpenDate).Date >= Convert.ToDateTime(DateTime.Now).Date.AddMonths(-1)).Count();
                chartSRTicketModel.InProgressSR = sr.Where(s => s.Status == "InProgress" && Convert.ToDateTime(s.TicketOpenDate).Date >= Convert.ToDateTime(DateTime.Now).Date.AddMonths(-1)).Count();
                chartSRTicketModel.CloseSR = sr.Where(s => s.Status == "Closed" && Convert.ToDateTime(s.TicketCloseDate).Date >= Convert.ToDateTime(DateTime.Now).Date.AddMonths(-1)).Count();
                chartSRTicketModel.TotalSR = chartSRTicketModel.OpenSR + chartSRTicketModel.InProgressSR + chartSRTicketModel.CloseSR;

                chartSRTicketModel.TotalTicket = ticket.Count;
                chartSRTicketModel.OpenTicket = ticket.Where(s => Convert.ToDateTime(s.TicketOpenDate).Date >= Convert.ToDateTime(DateTime.Now).Date.AddMonths(-1)).Count();
                chartSRTicketModel.InProgressTicket = ticket.Where(s => s.Status == "InProgress" && Convert.ToDateTime(s.TicketOpenDate).Date >= Convert.ToDateTime(DateTime.Now).Date.AddMonths(-1)).Count();
                chartSRTicketModel.CloseTicket = ticket.Where(s => s.Status == "Closed" && Convert.ToDateTime(s.TicketCloseDate).Date >= Convert.ToDateTime(DateTime.Now).Date.AddMonths(-1)).Count();
                chartSRTicketModel.TotalTicket = chartSRTicketModel.OpenTicket + chartSRTicketModel.InProgressTicket + chartSRTicketModel.CloseTicket;
            }
            else
            {
                chartSRTicketModel.TotalSR = sr.Count;
                chartSRTicketModel.OpenSR = sr.Where(s => s.Status == "Open").Count();
                chartSRTicketModel.InProgressSR = sr.Where(s => s.Status == "InProgress").Count();
                chartSRTicketModel.CloseSR = sr.Where(s => s.Status == "Closed").Count();

                chartSRTicketModel.TotalTicket = ticket.Count;
                chartSRTicketModel.OpenTicket = ticket.Where(s => s.Status == "Open").Count();
                chartSRTicketModel.InProgressTicket = ticket.Where(s => s.Status == "InProgress").Count();
                chartSRTicketModel.CloseTicket = ticket.Where(s => s.Status == "Closed").Count();

            }

            var top3Ticket = ticket.GroupBy(t => t.Category).OrderByDescending(d => d.Count()).ToList();
            if (top3Ticket.Count() > 0)
            {
                int count = 1;
                foreach (var item in top3Ticket)
                {
                    if (count <= 3)
                    {
                        var cat = categoryProvider.GetCategoryById(Convert.ToInt32(item.Select(i => i.Category).FirstOrDefault()));
                        if (cat != null)
                            chartSRTicketModel.Top3Ticket += cat.CategoryName.Trim() + "|";
                        else
                            chartSRTicketModel.Top3Ticket += item.Select(i => i.Category).FirstOrDefault() + "|";
                        count++;
                    }
                    else
                        break;
                }
            }

            var top3SR = sr.GroupBy(t => t.Category).OrderByDescending(d => d.Count()).ToList();
            if (top3SR.Count() > 0)
            {
                int count = 1;
                foreach (var item in top3SR)
                {
                    if (count <= 3)
                    {
                        var cat = categoryProvider.GetCategoryById(Convert.ToInt32(item.Select(i => i.Category).FirstOrDefault()));
                        if (cat != null)
                            chartSRTicketModel.Top3SR += cat.CategoryName.Trim() + "|";
                        else
                            chartSRTicketModel.Top3SR += item.Select(i => i.Category).FirstOrDefault() + "|";
                        count++;
                    }
                    else
                        break;
                }
            }

            return Json(chartSRTicketModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LineChart(string chartshow)
        {
            CategoryProvider categoryProvider = new CategoryProvider();
            var ticket = categoryProvider.GetAllTickets();
            ChartSRTicketModel chartSRTicketModel = new ChartSRTicketModel();

            chartSRTicketModel.TotalSR = ticket.Count;
            chartSRTicketModel.OpenSR = ticket.Where(sr => sr.Status == "Open").Count();
            chartSRTicketModel.InProgressSR = ticket.Where(sr => sr.Status == "InProgress").Count();
            chartSRTicketModel.CloseSR = ticket.Where(sr => sr.Status == "Closed").Count();



            return Json(chartSRTicketModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TicketList()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", null);
            }
            TicketManagementModel ticketManagementModel = new TicketManagementModel();
            CategoryProvider categoryProvider = new CategoryProvider();
            ticketManagementModel.TicketList = categoryProvider.GetAllTickets();

            //if (ticketManagementModel.TicketOpenDate != null && ticketManagementModel.TicketCloseDate != null)
            ticketManagementModel.TicketList = ticketManagementModel.TicketList.Where(tl => Convert.ToDateTime(tl.TicketOpenDate).Date >= DateTime.Now.Date.AddDays(-7)).ToList();


            if (ticketManagementModel.TicketList != null && ticketManagementModel.TicketList.Count > 0)
            {
                foreach (var item in ticketManagementModel.TicketList)
                {
                    var cat = categoryProvider.GetCategoryById(Convert.ToInt32(item.Category));
                    if (cat != null)
                        item.Category = cat.CategoryName;
                    var subcat = categoryProvider.GetSubCategoryById(Convert.ToInt32(item.SubCategory));
                    if (subcat != null)
                        item.SubCategory = subcat.SubCategoryName;
                    var subsubCategory = categoryProvider.GetSubSubCategoryById(Convert.ToInt32(item.SubSubCategory));
                    if (subsubCategory != null)
                        item.SubSubCategory = subsubCategory.SubSubCategoryName;

                    if (item.TicketOpenDate != null)
                    {
                        if (item.TicketCloseDate != null)
                            item.Duration = Convert.ToInt32((Convert.ToDateTime(item.TicketCloseDate) - Convert.ToDateTime(item.TicketOpenDate)).TotalMinutes);
                        else
                            item.Duration = Convert.ToInt32((DateTime.Now - Convert.ToDateTime(item.TicketOpenDate)).TotalMinutes);
                    }

                }
            }
            if (User.IsInRole("Admin") || User.IsInRole("Supervisor"))
                ViewBag.IsAccess = true;
            else
                ViewBag.IsAccess = false;

            return View(ticketManagementModel);
        }

        [HttpPost]
        public ActionResult TicketList(TicketManagementModel ticketManagementModel)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", null);
            }
            //TicketManagementModel ticketManagementModel = new TicketManagementModel();
            CategoryProvider categoryProvider = new CategoryProvider();
            var ticketList = categoryProvider.GetAllTickets();

            if (ticketManagementModel.TicketOpenDate != null && ticketManagementModel.TicketCloseDate != null)
                ticketList = ticketList.Where(tl => Convert.ToDateTime(tl.TicketOpenDate).Date >= Convert.ToDateTime(ticketManagementModel.TicketOpenDate).Date && Convert.ToDateTime(tl.TicketOpenDate).Date <= Convert.ToDateTime(ticketManagementModel.TicketCloseDate).Date).ToList();
            else
            {
                if (ticketManagementModel.TicketOpenDate != null)
                    ticketList = ticketList.Where(tl => tl.TicketOpenDate != null && Convert.ToDateTime(tl.TicketOpenDate).Date == Convert.ToDateTime(ticketManagementModel.TicketOpenDate).Date).ToList();
                if (ticketManagementModel.TicketCloseDate != null)
                    ticketList = ticketList.Where(tl => tl.TicketCloseDate != null && Convert.ToDateTime(tl.TicketCloseDate).Date == Convert.ToDateTime(ticketManagementModel.TicketCloseDate).Date).ToList();
            }
            if (!string.IsNullOrEmpty(ticketManagementModel.CallingNumber))
                ticketList = ticketList.Where(tl => tl.CallingNumber == ticketManagementModel.CallingNumber).ToList();
            if (!string.IsNullOrEmpty(ticketManagementModel.Status) && ticketManagementModel.Status.ToLower() != "select status")
                ticketList = ticketList.Where(tl => tl.Status == ticketManagementModel.Status).ToList();

            if (!string.IsNullOrEmpty(ticketManagementModel.Name))
                ticketList = ticketList.Where(tl => tl.Name.Contains(ticketManagementModel.Name) ).ToList();

            if (!string.IsNullOrEmpty(ticketManagementModel.PolicyNumber))
                ticketList = ticketList.Where(tl => !string.IsNullOrEmpty(tl.PolicyNumber) && tl.PolicyNumber.Contains(ticketManagementModel.PolicyNumber)).ToList();

            ticketManagementModel.TicketList = ticketList;

            if (ticketManagementModel.TicketList != null && ticketManagementModel.TicketList.Count > 0)
            {
                foreach (var item in ticketManagementModel.TicketList)
                {
                    var cat = categoryProvider.GetCategoryById(Convert.ToInt32(item.Category));
                    if (cat != null)
                        item.Category = cat.CategoryName;
                    var subcat = categoryProvider.GetSubCategoryById(Convert.ToInt32(item.SubCategory));
                    if (subcat != null)
                        item.SubCategory = subcat.SubCategoryName;
                    var subsubCategory = categoryProvider.GetSubSubCategoryById(Convert.ToInt32(item.SubSubCategory));
                    if (subsubCategory != null)
                        item.SubSubCategory = subsubCategory.SubSubCategoryName;

                    if (item.TicketOpenDate != null)
                    {
                        if (item.TicketCloseDate != null)
                            item.Duration = Convert.ToInt32((Convert.ToDateTime(item.TicketCloseDate) - Convert.ToDateTime(item.TicketOpenDate)).TotalMinutes);
                        else
                            item.Duration = Convert.ToInt32((DateTime.Now - Convert.ToDateTime(item.TicketOpenDate)).TotalMinutes);
                    }
                }
            }

            //if (((CustomPrincipal)User).UserRoleId == 1 || ((CustomPrincipal)User).UserRoleId==2)
            if (User.IsInRole("Admin") || User.IsInRole("Supervisor"))
                ViewBag.IsAccess = true;
            else
                ViewBag.IsAccess = false;
            return View(ticketManagementModel);
        }
        public ActionResult TicketManagement()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", null);
            }

            TicketManagementModel ticketManagementModel = new TicketManagementModel();
            CategoryProvider categoryProvider = new CategoryProvider();
            ticketManagementModel.CategoryList = categoryProvider.GetAllCategory();
            ticketManagementModel.SubCategoryList = ticketManagementModel.CategoryList.Count > 0 ? categoryProvider.GetSubCategoryByCategoryId(ticketManagementModel.CategoryList[0].CategoryId) : new List<SubCategoryMaster>();
            ticketManagementModel.SubSubCategoryList = ticketManagementModel.SubCategoryList.Count > 0 ? categoryProvider.GetSubSubCategoryBySubCategoryId(ticketManagementModel.SubCategoryList[0].SubCategoryId) : new List<SubSubCategoryMaster>();

            return View(ticketManagementModel);
        }
        [HttpPost]
        public ActionResult TicketManagement(TicketManagementModel ticketManagementModel)
        {
            bool statusTicket = false;
            string messageTicket = string.Empty;

            if (ModelState.IsValid)
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var ticketManagement = new TicketManagement()
                    {
                        Name = ticketManagementModel.Name,
                        CallingNumber = ticketManagementModel.CallingNumber,
                        TypeOfCaller = ticketManagementModel.TypeOfCaller,
                        CustomerSegment = ticketManagementModel.CustomerSegment,
                        TypeOfCall = ticketManagementModel.TypeOfCall,
                        Category = ticketManagementModel.Category,
                        SubCategory = ticketManagementModel.SubCategory,
                        SubSubCategory = ticketManagementModel.SubSubCategory,
                        Remark = ticketManagementModel.Remark,
                        TicketOpenDate = DateTime.Now,
                        TicketOpenAgentName = ((CustomPrincipal)User).FirstName + " " + ((CustomPrincipal)User).LastName,
                        Status = "Open",
                        PolicyNumber = ticketManagementModel.PolicyNumber,
                        ClientType = ticketManagementModel.ClientType

                    };
                    dbContext.TicketManagements.Add(ticketManagement);
                    dbContext.SaveChanges();
                }

                //VerificationEmail(registrationView.Email, registrationView.ActivationCode.ToString());
                messageTicket = "Ticket has been created successfully.";
                statusTicket = true;
                return RedirectToAction("TicketList", "Home");
            }
            else
            {
                messageTicket = "Something Wrong!";
                CategoryProvider categoryProvider = new CategoryProvider();
                ticketManagementModel.CategoryList = categoryProvider.GetAllCategory();
                ticketManagementModel.SubCategoryList = categoryProvider.GetSubCategoryByCategoryId(Convert.ToInt32(ticketManagementModel.Category));
                ticketManagementModel.SubSubCategoryList = categoryProvider.GetSubSubCategoryBySubCategoryId(Convert.ToInt32(ticketManagementModel.SubSubCategory));
            }
            ViewBag.Message = messageTicket;
            ViewBag.Status = statusTicket;
            return View(ticketManagementModel);
        }

        public ActionResult TicketManagementEdit(int id)
        {
            TicketManagementModel ticketManagementModel = new TicketManagementModel();
            CategoryProvider categoryProvider = new CategoryProvider();
            TicketManagement ticket = new TicketManagement();
            ticket = categoryProvider.GetTicketsById(id);

            ticketManagementModel.TicketId = ticket.TicketId;

            ticketManagementModel.Name = ticket.Name;

            ticketManagementModel.CallingNumber = ticket.CallingNumber;
            ticketManagementModel.TypeOfCaller = ticket.TypeOfCaller;
            ticketManagementModel.CustomerSegment = ticket.CustomerSegment;
            ticketManagementModel.TypeOfCall = ticket.TypeOfCall;
            ticketManagementModel.Category = ticket.Category;
            ticketManagementModel.SubCategory = ticket.SubCategory;
            ticketManagementModel.SubSubCategory = ticket.SubSubCategory;
            ticketManagementModel.Remark = ticket.Remark;
            ticketManagementModel.Status = ticket.Status;
            ticketManagementModel.CategoryList = categoryProvider.GetAllCategory();
            ticketManagementModel.SubCategoryList = ticketManagementModel.CategoryList.Count > 0 ? categoryProvider.GetSubCategoryByCategoryId(Convert.ToInt32(ticketManagementModel.Category)) : new List<SubCategoryMaster>();
            ticketManagementModel.SubSubCategoryList = ticketManagementModel.SubCategoryList.Count > 0 ? categoryProvider.GetSubSubCategoryBySubCategoryId(Convert.ToInt32(ticketManagementModel.SubCategory)) : new List<SubSubCategoryMaster>();
            ticketManagementModel.PolicyNumber = ticket.PolicyNumber;
            ticketManagementModel.ClientType = ticket.ClientType;
            return View(ticketManagementModel);
        }

        [HttpPost]
        public ActionResult TicketManagementEdit(TicketManagementModel ticketManagementModel)
        {

            bool statusTicket = false;
            string messageTicket = string.Empty;
            if (ModelState.IsValid)
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var result = dbContext.TicketManagements.SingleOrDefault(m => m.TicketId == ticketManagementModel.TicketId);
                    if (result != null)
                    {
                        result.Name = ticketManagementModel.Name;
                        result.CallingNumber = ticketManagementModel.CallingNumber;
                        result.TypeOfCaller = ticketManagementModel.TypeOfCaller;
                        result.CustomerSegment = ticketManagementModel.CustomerSegment;
                        result.TypeOfCall = ticketManagementModel.TypeOfCall;
                        result.Category = ticketManagementModel.Category;
                        result.SubCategory = ticketManagementModel.SubCategory;
                        result.SubSubCategory = ticketManagementModel.SubSubCategory;
                        result.Remark = ticketManagementModel.Remark;
                        result.Status = ticketManagementModel.Status;
                        result.TicketCloseDate = ticketManagementModel.Status.ToLower() == "closed" ? DateTime.Now : ticketManagementModel.TicketCloseDate;
                        result.TicketCloseAgentName = ((CustomPrincipal)User).FirstName + " " + ((CustomPrincipal)User).LastName;
                        result.PolicyNumber = ticketManagementModel.PolicyNumber;
                        result.ClientType = ticketManagementModel.ClientType;
                        dbContext.SaveChanges();
                        messageTicket = "Category has been updated successfully.";
                        statusTicket = true;

                    }
                }
                return RedirectToAction("TicketList", "Home");
            }
            else
            {
                messageTicket = "Something Wrong!";
                CategoryProvider categoryProvider = new CategoryProvider();
                ticketManagementModel.CategoryList = categoryProvider.GetAllCategory();
                ticketManagementModel.SubCategoryList = ticketManagementModel.CategoryList.Count > 0 ? categoryProvider.GetSubCategoryByCategoryId(Convert.ToInt32(ticketManagementModel.Category)) : new List<SubCategoryMaster>();
                ticketManagementModel.SubSubCategoryList = ticketManagementModel.SubCategoryList.Count > 0 ? categoryProvider.GetSubSubCategoryBySubCategoryId(Convert.ToInt32(ticketManagementModel.SubCategory)) : new List<SubSubCategoryMaster>();

            }

            return View(ticketManagementModel);
        }


        public ActionResult TicketDownloadExcel(string opendate, string closedate, string callingno, string status) //string opendate, string closedate)
        {
            TicketManagementModel ticketManagementModel = new TicketManagementModel();

            if (!string.IsNullOrEmpty(opendate))
                ticketManagementModel.TicketOpenDate = Convert.ToDateTime(opendate);

            if (!string.IsNullOrEmpty(closedate))
                ticketManagementModel.TicketCloseDate = Convert.ToDateTime(closedate);

            if (!string.IsNullOrEmpty(callingno))
                ticketManagementModel.CallingNumber = callingno;

            if (!string.IsNullOrEmpty(status))
                ticketManagementModel.Status = status;

            CategoryProvider categoryProvider = new CategoryProvider();
            var ticketList = categoryProvider.GetAllTickets();

            foreach (var item in ticketList)
            {
                var cat = categoryProvider.GetCategoryById(Convert.ToInt32(item.Category));
                if (cat != null)
                    item.Category = cat.CategoryName;
                var subcat = categoryProvider.GetSubCategoryById(Convert.ToInt32(item.SubCategory));
                if (subcat != null)
                    item.SubCategory = subcat.SubCategoryName;
                var subsubCategory = categoryProvider.GetSubSubCategoryById(Convert.ToInt32(item.SubSubCategory));
                if (subsubCategory != null)
                    item.SubSubCategory = subsubCategory.SubSubCategoryName;

                if (item.TicketOpenDate != null)
                {
                    if (item.TicketCloseDate != null)
                        item.Duration = Convert.ToInt32((Convert.ToDateTime(item.TicketCloseDate) - Convert.ToDateTime(item.TicketOpenDate)).TotalMinutes);
                    else
                        item.Duration = Convert.ToInt32((DateTime.Now - Convert.ToDateTime(item.TicketOpenDate)).TotalMinutes);
                }

            }

            if (ticketManagementModel.TicketOpenDate != null && ticketManagementModel.TicketCloseDate != null)
                ticketList = ticketList.Where(tl => Convert.ToDateTime(tl.TicketOpenDate).Date >= Convert.ToDateTime(ticketManagementModel.TicketOpenDate).Date && Convert.ToDateTime(tl.TicketCloseDate).Date <= Convert.ToDateTime(ticketManagementModel.TicketCloseDate).Date).ToList();
            else
            {
                if (ticketManagementModel.TicketOpenDate != null)
                    ticketList = ticketList.Where(tl => tl.TicketCloseDate != null && Convert.ToDateTime(tl.TicketOpenDate).Date == Convert.ToDateTime(ticketManagementModel.TicketOpenDate).Date).ToList();
                if (ticketManagementModel.TicketCloseDate != null)
                    ticketList = ticketList.Where(tl => tl.TicketCloseDate != null && Convert.ToDateTime(tl.TicketCloseDate).Date == Convert.ToDateTime(ticketManagementModel.TicketCloseDate).Date).ToList();
            }
            if (!string.IsNullOrEmpty(ticketManagementModel.CallingNumber))
                ticketList = ticketList.Where(tl => tl.CallingNumber == ticketManagementModel.CallingNumber).ToList();
            if (!string.IsNullOrEmpty(ticketManagementModel.Status) && ticketManagementModel.Status.ToLower() != "select status")
                ticketList = ticketList.Where(tl => tl.Status == ticketManagementModel.Status).ToList();

            var gv = new GridView();
            gv.DataSource = ticketList.OrderByDescending(m => m.TicketOpenDate);
            gv.DataBind();
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            byte[] bindata = System.Text.Encoding.ASCII.GetBytes(objStringWriter.ToString());
            return File(bindata, "application/ms-excel", "TicketListExcel.xls");
        }

        public ActionResult SRList()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", null);
            }
            SRManagementModel sRManagementModel = new SRManagementModel();
            CategoryProvider categoryProvider = new CategoryProvider();
            sRManagementModel.SRList = categoryProvider.GetAllSRs();

            sRManagementModel.SRList = sRManagementModel.SRList.Where(tl => Convert.ToDateTime(tl.TicketOpenDate).Date >= DateTime.Now.Date.AddDays(-7)).ToList();

            foreach (var item in sRManagementModel.SRList)
            {
                var cat = categoryProvider.GetCategoryById(Convert.ToInt32(item.Category));
                if (cat != null)
                    item.Category = cat.CategoryName;
                var subcat = categoryProvider.GetSubCategoryById(Convert.ToInt32(item.SubCategory));
                if (subcat != null)
                    item.SubCategory = subcat.SubCategoryName;
                var subsubCategory = categoryProvider.GetSubSubCategoryById(Convert.ToInt32(item.SubSubCategory));
                if (subsubCategory != null)
                    item.SubSubCategory = subsubCategory.SubSubCategoryName;

                if (item.TicketOpenDate != null)
                {
                    if (item.TicketCloseDate != null)
                    {
                        if (Convert.ToInt32((Convert.ToDateTime(item.TicketCloseDate) - Convert.ToDateTime(item.TicketOpenDate)).TotalHours) > Convert.ToInt32(ConfigurationManager.AppSettings["RubySegmentSLAInHour"]) && item.CustomerSegment != null && item.CustomerSegment.ToLower() == "ruby")
                        {
                            item.SlaBridge = "YES";
                        }
                        else if (Convert.ToInt32((Convert.ToDateTime(item.TicketCloseDate) - Convert.ToDateTime(item.TicketOpenDate)).TotalHours) > Convert.ToInt32(ConfigurationManager.AppSettings["NonRubySegmentSLAInHour"]) && item.CustomerSegment != null)
                        {
                            item.SlaBridge = "YES";
                        }
                        else
                            item.SlaBridge = "NO";
                    }
                    else
                    {
                        if (Convert.ToInt32((DateTime.Now - Convert.ToDateTime(item.TicketOpenDate)).TotalHours) > Convert.ToInt32(ConfigurationManager.AppSettings["RubySegmentSLAInHour"]) && item.CustomerSegment != null && item.CustomerSegment.ToLower() == "ruby")
                        {
                            item.SlaBridge = "YES";
                        }
                        else if (Convert.ToInt32((DateTime.Now - Convert.ToDateTime(item.TicketOpenDate)).TotalHours) > Convert.ToInt32(ConfigurationManager.AppSettings["NonRubySegmentSLAInHour"]) && item.CustomerSegment != null)
                        {
                            item.SlaBridge = "YES";
                        }
                        else
                            item.SlaBridge = "NO";
                    }
                }
            }
            if (User.IsInRole("Admin") || User.IsInRole("Supervisor"))
                ViewBag.IsAccess = true;
            else
                ViewBag.IsAccess = false;
            return View(sRManagementModel);
        }
        [HttpPost]
        public ActionResult SRList(SRManagementModel sRManagementModel)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", null);
            }
            //SRManagementModel sRManagementModel = new SRManagementModel();
            CategoryProvider categoryProvider = new CategoryProvider();
            var SRList = categoryProvider.GetAllSRs();

            if (sRManagementModel.TicketOpenDate != null && sRManagementModel.TicketCloseDate != null)
                SRList = SRList.Where(tl => Convert.ToDateTime(tl.TicketOpenDate).Date >= Convert.ToDateTime(sRManagementModel.TicketOpenDate).Date && Convert.ToDateTime(tl.TicketOpenDate).Date <= Convert.ToDateTime(sRManagementModel.TicketCloseDate).Date).ToList();
            else
            {
                if (sRManagementModel.TicketOpenDate != null)
                    SRList = SRList.Where(tl => tl.TicketOpenDate != null && Convert.ToDateTime(tl.TicketOpenDate).Date == Convert.ToDateTime(sRManagementModel.TicketOpenDate).Date).ToList();
                if (sRManagementModel.TicketCloseDate != null)
                    SRList = SRList.Where(tl => tl.TicketCloseDate != null && Convert.ToDateTime(tl.TicketCloseDate).Date == Convert.ToDateTime(sRManagementModel.TicketCloseDate).Date).ToList();
            }
            if (!string.IsNullOrEmpty(sRManagementModel.PhoneNo))
                SRList = SRList.Where(tl => tl.PhoneNo == sRManagementModel.PhoneNo).ToList();
            if (!string.IsNullOrEmpty(sRManagementModel.Status) && sRManagementModel.Status.ToLower() != "select status")
                SRList = SRList.Where(tl => tl.Status == sRManagementModel.Status).ToList();

            if (!string.IsNullOrEmpty(sRManagementModel.CustomerName))
                SRList = SRList.Where(tl => !string.IsNullOrEmpty(tl.CustomerName) && tl.CustomerName.Contains(sRManagementModel.CustomerName)).ToList();

            if (!string.IsNullOrEmpty(sRManagementModel.Remark))
                SRList = SRList.Where(tl => !string.IsNullOrEmpty(tl.Remark) && tl.Remark.Contains(sRManagementModel.Remark)).ToList();

            sRManagementModel.SRList = SRList;

            foreach (var item in sRManagementModel.SRList)
            {
                var cat = categoryProvider.GetCategoryById(Convert.ToInt32(item.Category));
                if (cat != null)
                    item.Category = cat.CategoryName;
                var subcat = categoryProvider.GetSubCategoryById(Convert.ToInt32(item.SubCategory));
                if (subcat != null)
                    item.SubCategory = subcat.SubCategoryName;
                var subsubCategory = categoryProvider.GetSubSubCategoryById(Convert.ToInt32(item.SubSubCategory));
                if (subsubCategory != null)
                    item.SubSubCategory = subsubCategory.SubSubCategoryName;

                if (item.TicketOpenDate != null)
                {
                    if (item.TicketCloseDate != null)
                    {
                        if (Convert.ToInt32((Convert.ToDateTime(item.TicketCloseDate) - Convert.ToDateTime(item.TicketOpenDate)).TotalHours) > Convert.ToInt32(ConfigurationManager.AppSettings["RubySegmentSLAInHour"]) && item.CustomerSegment != null && item.CustomerSegment.ToLower() == "ruby")
                        {
                            item.SlaBridge = "YES";
                        }
                        else if (Convert.ToInt32((Convert.ToDateTime(item.TicketCloseDate) - Convert.ToDateTime(item.TicketOpenDate)).TotalHours) > Convert.ToInt32(ConfigurationManager.AppSettings["NonRubySegmentSLAInHour"]) && item.CustomerSegment != null)
                        {
                            item.SlaBridge = "YES";
                        }
                        else
                            item.SlaBridge = "NO";
                    }
                    else
                    {
                        if (Convert.ToInt32((DateTime.Now - Convert.ToDateTime(item.TicketOpenDate)).TotalHours) > Convert.ToInt32(ConfigurationManager.AppSettings["RubySegmentSLAInHour"]) && item.CustomerSegment != null && item.CustomerSegment.ToLower() == "ruby")
                        {
                            item.SlaBridge = "YES";
                        }
                        else if (Convert.ToInt32((DateTime.Now - Convert.ToDateTime(item.TicketOpenDate)).TotalHours) > Convert.ToInt32(ConfigurationManager.AppSettings["NonRubySegmentSLAInHour"]) && item.CustomerSegment != null)
                        {
                            item.SlaBridge = "YES";
                        }
                        else
                            item.SlaBridge = "NO";
                    }
                }
            }
            if (User.IsInRole("Admin") || User.IsInRole("Supervisor"))
                ViewBag.IsAccess = true;
            else
                ViewBag.IsAccess = false;
            return View(sRManagementModel);
        }

        public ActionResult SRManagement()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", null);
            }

            SRManagementModel sRManagementModel = new SRManagementModel();
            CategoryProvider categoryProvider = new CategoryProvider();
            sRManagementModel.CategoryList = categoryProvider.GetAllCategory();
            sRManagementModel.SubCategoryList = sRManagementModel.CategoryList.Count > 0 ? categoryProvider.GetSubCategoryByCategoryId(sRManagementModel.CategoryList[0].CategoryId) : new List<SubCategoryMaster>();
            sRManagementModel.SubSubCategoryList = sRManagementModel.SubCategoryList.Count > 0 ? categoryProvider.GetSubSubCategoryBySubCategoryId(sRManagementModel.SubCategoryList[0].SubCategoryId) : new List<SubSubCategoryMaster>();
            return View(sRManagementModel);
        }

        [HttpPost]
        public ActionResult SRManagement(SRManagementModel sRManagementModel)
        {
            bool statusTicket = false;
            string messageTicket = string.Empty;

            if (ModelState.IsValid)
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var sRManagement = new SRManagement()
                    {
                        CustomerName = sRManagementModel.CustomerName,
                        PhoneNo = sRManagementModel.PhoneNo,
                        //Email = sRManagementModel.Email,
                        AgentName = sRManagementModel.AgentName,
                        RequestComplaintDetails = sRManagementModel.RequestComplaintDetails,
                        Category = sRManagementModel.Category,
                        SubCategory = sRManagementModel.SubCategory,
                        SubSubCategory = sRManagementModel.SubSubCategory,
                        Remark = sRManagementModel.Remark,
                        TicketOpenDate = DateTime.Now,
                        TicketOpenAgentName = ((CustomPrincipal)User).FirstName + " " + ((CustomPrincipal)User).LastName,
                        Status = "Open",
                        TypeOfCall = sRManagementModel.TypeOfCall,
                        TypeOfCaller = sRManagementModel.TypeOfCaller,
                        CustomerSegment = sRManagementModel.CustomerSegment,
                        ClientType = sRManagementModel.ClientType
                    };
                    dbContext.SRManagements.Add(sRManagement);
                    dbContext.SaveChanges();
                }

                return RedirectToAction("SRList", "Home");
                //VerificationEmail(registrationView.Email, registrationView.ActivationCode.ToString());
                messageTicket = "SR has been created successfully.";
                statusTicket = true;
            }
            else
            {
                messageTicket = "Something Wrong!";
                CategoryProvider categoryProvider = new CategoryProvider();
                sRManagementModel.CategoryList = categoryProvider.GetAllCategory();
                sRManagementModel.SubCategoryList = categoryProvider.GetSubCategoryByCategoryId(Convert.ToInt32(sRManagementModel.Category));
                sRManagementModel.SubSubCategoryList = categoryProvider.GetSubSubCategoryBySubCategoryId(Convert.ToInt32(sRManagementModel.SubSubCategory));
            }
            ViewBag.Message = messageTicket;
            ViewBag.Status = statusTicket;
            return View(sRManagementModel);
        }


        public ActionResult SRManagementEdit(int Id)
        {

            SRManagementModel sRManagementModel = new SRManagementModel();
            CategoryProvider categoryProvider = new CategoryProvider();
            SRManagement sr = new SRManagement();
            sr = categoryProvider.GetSRById(Id);

            sRManagementModel.SRId = sr.SRId;
            sRManagementModel.CustomerName = sr.CustomerName;
            sRManagementModel.PhoneNo = sr.PhoneNo;
            sRManagementModel.Email = sr.Email;
            sRManagementModel.AgentName = sr.AgentName;
            sRManagementModel.RequestComplaintDetails = sr.RequestComplaintDetails;
            sRManagementModel.Category = sr.Category;
            sRManagementModel.SubCategory = sr.SubCategory;
            sRManagementModel.SubSubCategory = sr.SubSubCategory;
            sRManagementModel.Remark = sr.Remark;
            sRManagementModel.Status = sr.Status;
            sRManagementModel.TypeOfCall = sr.TypeOfCall;
            sRManagementModel.TypeOfCaller = sr.TypeOfCaller;
            sRManagementModel.CustomerSegment = sr.CustomerSegment;
            sRManagementModel.ResoluctionFeedback = sr.ResoluctionFeedback;
            sRManagementModel.ClientType = sr.ClientType;

            sRManagementModel.CategoryList = categoryProvider.GetAllCategory();
            sRManagementModel.SubCategoryList = sRManagementModel.CategoryList.Count > 0 ? categoryProvider.GetSubCategoryByCategoryId(Convert.ToInt32(sr.Category)) : new List<SubCategoryMaster>();
            sRManagementModel.SubSubCategoryList = sRManagementModel.SubCategoryList.Count > 0 ? categoryProvider.GetSubSubCategoryBySubCategoryId(Convert.ToInt32(sr.SubCategory)) : new List<SubSubCategoryMaster>();

            return View(sRManagementModel);
        }
        [HttpPost]
        public ActionResult SRManagementEdit(SRManagementModel sRManagementModel)
        {
            bool statusTicket = false;
            string messageTicket = string.Empty;

            if (ModelState.IsValid)
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var result = dbContext.SRManagements.SingleOrDefault(m => m.SRId == sRManagementModel.SRId);
                    if (result != null)
                    {

                        result.CustomerName = sRManagementModel.CustomerName;
                        result.PhoneNo = sRManagementModel.PhoneNo;
                        result.Email = sRManagementModel.Email;
                        result.AgentName = sRManagementModel.AgentName;
                        result.RequestComplaintDetails = sRManagementModel.RequestComplaintDetails;
                        result.Category = sRManagementModel.Category;
                        result.SubCategory = sRManagementModel.SubCategory;
                        result.SubSubCategory = sRManagementModel.SubSubCategory;
                        result.Remark = sRManagementModel.Remark;
                        result.Status = sRManagementModel.Status; ;
                        result.TicketCloseDate = sRManagementModel.Status.ToLower() == "closed" ? DateTime.Now : sRManagementModel.TicketCloseDate;
                        result.TicketCloseAgentName = ((CustomPrincipal)User).FirstName + " " + ((CustomPrincipal)User).LastName;
                        result.TypeOfCall = sRManagementModel.TypeOfCall;
                        result.TypeOfCaller = sRManagementModel.TypeOfCaller;
                        result.CustomerSegment = sRManagementModel.CustomerSegment;
                        result.ResoluctionFeedback = sRManagementModel.ResoluctionFeedback;
                        result.ClientType = sRManagementModel.ClientType;

                        dbContext.SaveChanges();
                        messageTicket = "SR has been updated successfully.";
                        statusTicket = true;

                    }
                }
                return RedirectToAction("SRList", "Home");
            }
            else
            {
                messageTicket = "Something Wrong!";
                CategoryProvider categoryProvider = new CategoryProvider();
                sRManagementModel.CategoryList = categoryProvider.GetAllCategory();
                sRManagementModel.SubCategoryList = categoryProvider.GetSubCategoryByCategoryId(Convert.ToInt32(sRManagementModel.Category));
                sRManagementModel.SubSubCategoryList = categoryProvider.GetSubSubCategoryBySubCategoryId(Convert.ToInt32(sRManagementModel.SubSubCategory));
            }
            ViewBag.Message = messageTicket;
            ViewBag.Status = statusTicket;
            return View(sRManagementModel);
        }


        public ActionResult SRDownloadExcel(string opendate, string closedate, string callingno, string status)
        {

            SRManagementModel sRManagementModel = new SRManagementModel();

            if (!string.IsNullOrEmpty(opendate))
                sRManagementModel.TicketOpenDate = Convert.ToDateTime(opendate);

            if (!string.IsNullOrEmpty(closedate))
                sRManagementModel.TicketCloseDate = Convert.ToDateTime(closedate);

            if (!string.IsNullOrEmpty(callingno))
                sRManagementModel.PhoneNo = callingno;
            if (!string.IsNullOrEmpty(status))
                sRManagementModel.Status = status;

            CategoryProvider categoryProvider = new CategoryProvider();
            var SRList = categoryProvider.GetAllSRs();

            foreach (var item in SRList)
            {
                var cat = categoryProvider.GetCategoryById(Convert.ToInt32(item.Category));
                if (cat != null)
                    item.Category = cat.CategoryName;
                var subcat = categoryProvider.GetSubCategoryById(Convert.ToInt32(item.SubCategory));
                if (subcat != null)
                    item.SubCategory = subcat.SubCategoryName;
                var subsubCategory = categoryProvider.GetSubSubCategoryById(Convert.ToInt32(item.SubSubCategory));
                if (subsubCategory != null)
                    item.SubSubCategory = subsubCategory.SubSubCategoryName;

                if (item.TicketOpenDate != null)
                {
                    if (item.TicketCloseDate != null)
                    {
                        if (Convert.ToInt32((Convert.ToDateTime(item.TicketCloseDate) - Convert.ToDateTime(item.TicketOpenDate)).TotalHours) > Convert.ToInt32(ConfigurationManager.AppSettings["RubySegmentSLAInHour"]) && item.CustomerSegment != null && item.CustomerSegment.ToLower() == "ruby")
                        {
                            item.SlaBridge = "YES";
                        }
                        else if (Convert.ToInt32((Convert.ToDateTime(item.TicketCloseDate) - Convert.ToDateTime(item.TicketOpenDate)).TotalHours) > Convert.ToInt32(ConfigurationManager.AppSettings["NonRubySegmentSLAInHour"]) && item.CustomerSegment != null)
                        {
                            item.SlaBridge = "YES";
                        }
                        else
                            item.SlaBridge = "NO";
                    }
                    else
                    {
                        if (Convert.ToInt32((DateTime.Now - Convert.ToDateTime(item.TicketOpenDate)).TotalMinutes) > Convert.ToInt32(ConfigurationManager.AppSettings["RubySegmentSLAInHour"]) && item.CustomerSegment != null && item.CustomerSegment.ToLower() == "ruby")
                        {
                            item.SlaBridge = "YES";
                        }
                        else if (Convert.ToInt32((DateTime.Now - Convert.ToDateTime(item.TicketOpenDate)).TotalMinutes) > Convert.ToInt32(ConfigurationManager.AppSettings["NonRubySegmentSLAInHour"]) && item.CustomerSegment != null)
                        {
                            item.SlaBridge = "YES";
                        }
                        else
                            item.SlaBridge = "NO";
                    }
                }
            }

            if (sRManagementModel.TicketOpenDate != null && sRManagementModel.TicketCloseDate != null)
                SRList = SRList.Where(tl => Convert.ToDateTime(tl.TicketOpenDate).Date >= Convert.ToDateTime(sRManagementModel.TicketOpenDate).Date && Convert.ToDateTime(tl.TicketOpenDate).Date <= Convert.ToDateTime(sRManagementModel.TicketOpenDate).Date).ToList();
            else
            {
                if (sRManagementModel.TicketOpenDate != null)
                    SRList = SRList.Where(tl => tl.TicketOpenDate != null && Convert.ToDateTime(tl.TicketOpenDate).Date == Convert.ToDateTime(sRManagementModel.TicketOpenDate).Date).ToList();
                if (sRManagementModel.TicketCloseDate != null)
                    SRList = SRList.Where(tl => tl.TicketCloseDate != null && Convert.ToDateTime(tl.TicketCloseDate).Date == Convert.ToDateTime(sRManagementModel.TicketCloseDate).Date).ToList();
            }
            if (!string.IsNullOrEmpty(sRManagementModel.PhoneNo))
                SRList = SRList.Where(tl => tl.PhoneNo == sRManagementModel.PhoneNo).ToList();
            if (!string.IsNullOrEmpty(sRManagementModel.Status) && sRManagementModel.Status.ToLower() != "select status")
                SRList = SRList.Where(tl => tl.Status == sRManagementModel.Status).ToList();

            var gv = new GridView();
            gv.DataSource = SRList;
            gv.DataBind();
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            byte[] bindata = System.Text.Encoding.ASCII.GetBytes(objStringWriter.ToString());
            return File(bindata, "application/ms-excel", "SRListExcel.xls");
        }
        public ActionResult KnowledgeCenter()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", null);
            }
            return View();
        }

        public string GetSubCategoryByCategory(string CategoryId)
        {

            CategoryProvider categoryProvider = new CategoryProvider();
            return (JsonConvert.SerializeObject(categoryProvider.GetSubCategoryByCategoryId(Convert.ToInt32(CategoryId)))).ToString();
        }
        public string GetSubSubCategoryBySubCategory(string SubCategoryId)
        {

            CategoryProvider categoryProvider = new CategoryProvider();
            if (!string.IsNullOrEmpty(SubCategoryId) && SubCategoryId != "null")
                return (JsonConvert.SerializeObject(categoryProvider.GetSubSubCategoryBySubCategoryId(Convert.ToInt32(SubCategoryId)))).ToString();
            else
                return "";
        }

        public string Encrypt(string clearText)
        {
            string EncryptionKey = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }



        public ActionResult ResetPassword(int? id)
        {
            RegistrationViewModel registrationViewModel = new RegistrationViewModel();
            CategoryProvider categoryProvider = new CategoryProvider();
            CustomRoleProvider customRoleProvider = new CustomRoleProvider();
            User user = new User();

            if (id != null)
                user = categoryProvider.GetUserById(Convert.ToInt32(id));
            else
                user = categoryProvider.GetUserByUserName(User.Identity.Name);

            registrationViewModel.UserId = user.UserId;

            registrationViewModel.FirstName = user.FirstName;
            registrationViewModel.LastName = user.LastName;
            registrationViewModel.Username = user.Username;
            registrationViewModel.RoleId = user.RoleId;
            registrationViewModel.Email = user.Email;

            return View(registrationViewModel);

        }

        [HttpPost]
        public ActionResult ResetPassword(RegistrationViewModel registrationViewModel)
        {
            bool statusRegistration = false;
            string messageRegistration = string.Empty;
            if (ModelState.IsValid)
            {

                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var result = dbContext.Users.SingleOrDefault(m => m.UserId == registrationViewModel.UserId);
                    if (result != null)
                    {
                        result.Password = EncryptionProvider.Encrypt(registrationViewModel.Password);
                        dbContext.SaveChanges();
                    }
                }
                return RedirectToAction("Index", "Home");

            }
            return View(registrationViewModel);
        }

    }
}