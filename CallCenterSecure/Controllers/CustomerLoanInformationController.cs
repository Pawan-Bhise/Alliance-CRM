using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CallCenter.Models;
using CallCenterSecure.Models;
using CsvHelper.Configuration;
using OfficeOpenXml;
using ClosedXML.Excel;
using CallCenterSecure.Repositories;
using CallCenterSecure.Models.CustomerLoan;
using System.Globalization;


namespace CallCenter.Controllers
{
    public class CustomerLoanInformationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CustomerRepository customerRepository = new CustomerRepository();

        // GET: CustomerLoanInformation
        public ActionResult Index()
        {            
            return View(db.CustomerLoan.ToList());
        }

        // GET: CustomerLoanInformation/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerLoanInformation customerLoanInformation = db.CustomerLoanInformations.Find(id);
            if (customerLoanInformation == null)
            {
                return HttpNotFound();
            }
            return View(customerLoanInformation);
        }

        // GET: CustomerLoanInformation/Create
        public ActionResult Create()
        {
            ViewBag.AccountTypes = new SelectList(new[] { "Individual", "Joint" });
            ViewBag.Genders = new SelectList(new[] { "Male", "Female", "Other" });
            ViewBag.MaritalStatuses = new SelectList(new[] { "Single", "Married", "Divorced", "Widowed" });
            ViewBag.EducationalQualifications = new SelectList(new[] { "Primary", "Middle", "High School", "Graduate", "Post Graduate" });
            ViewBag.CustomerStatuses = new SelectList(new[] { "Active", "Inactive" });
            ViewBag.States = new SelectList(db.States, "StateName", "StateName");
            ViewBag.Districts = new SelectList(db.Districts, "DistrictName", "DistrictName");
            ViewBag.Cities = new SelectList(db.Cities, "CityName", "CityName");
            ViewBag.VillageTracts = new SelectList(db.VillageTracts, "VillageTractName", "VillageTractName");
            ViewBag.Villages = new SelectList(db.Villages, "VillageName", "VillageName");
            ViewBag.Areas = new SelectList(db.Areas, "AreaName", "AreaName");
            ViewBag.Branches = new SelectList(db.AllianceBranches, "BranchName", "BranchName");
            ViewBag.Products = new SelectList(db.AllianceProducts, "ProductName", "ProductName");
            return View();
        }

        // POST: CustomerLoanInformation/Upload
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                ModelState.AddModelError("", "Please select an Excel file");
                return View();
            }

            var customers = new List<CustomerLoan>();

            using (var workbook = new XLWorkbook(file.InputStream))
            {
                var ws = workbook.Worksheet(1);
                var rows = ws.RangeUsed().RowsUsed().Skip(1); // skip header

                foreach (var row in rows)
                {
                    var customer = new CustomerLoan
                    {
                        GroupCode = ExcelHelper.GetInt(ws, row.RowNumber(), 1),
                        COCashAccount = ExcelHelper.GetString(ws, row.RowNumber(), 2),
                        COStaffId = ExcelHelper.GetString(ws, row.RowNumber(), 3),
                        COName = ExcelHelper.GetString(ws, row.RowNumber(), 4),
                        ProductCode = ExcelHelper.GetString(ws, row.RowNumber(), 5),
                        ProductName = ExcelHelper.GetString(ws, row.RowNumber(), 6),
                        ProductCategory = ExcelHelper.GetString(ws, row.RowNumber(), 7),
                        CustomerCode = ExcelHelper.GetString(ws, row.RowNumber(), 8),
                        AccountNumber = ExcelHelper.GetString(ws, row.RowNumber(), 9),
                        BranchCode = ExcelHelper.GetInt(ws, row.RowNumber(), 10) ?? 0,
                        BranchName = ExcelHelper.GetString(ws, row.RowNumber(), 11),
                        ParentBranchName = ExcelHelper.GetString(ws, row.RowNumber(), 12),
                        RegionalBranchName = ExcelHelper.GetString(ws, row.RowNumber(), 13),
                        DateOfActOpening = ExcelHelper.GetDate(ws, row.RowNumber(), 14),
                        Salutation = ExcelHelper.GetInt(ws, row.RowNumber(), 15) ?? 0,
                        CustomerName = ExcelHelper.GetString(ws, row.RowNumber(), 16),
                        Gender = ExcelHelper.GetString(ws, row.RowNumber(), 17),
                        FatherName = ExcelHelper.GetString(ws, row.RowNumber(), 18),
                        AreaType = ExcelHelper.GetString(ws, row.RowNumber(), 19),
                        Area = ExcelHelper.GetString(ws, row.RowNumber(), 20),
                        VillageWard = ExcelHelper.GetString(ws, row.RowNumber(), 21),
                        VillageTractTown = ExcelHelper.GetString(ws, row.RowNumber(), 22),
                        CityTownship = ExcelHelper.GetString(ws, row.RowNumber(), 23),
                        District = ExcelHelper.GetString(ws, row.RowNumber(), 24),
                        RegionState = ExcelHelper.GetString(ws, row.RowNumber(), 25),
                        NRC = ExcelHelper.GetString(ws, row.RowNumber(), 26),
                        MobileNo1 = ExcelHelper.GetString(ws, row.RowNumber(), 27),
                        MobileNo2 = ExcelHelper.GetString(ws, row.RowNumber(), 28),
                        CustomerStatus = ExcelHelper.GetString(ws, row.RowNumber(), 29),
                        FreezeStatus = ExcelHelper.GetString(ws, row.RowNumber(), 30),
                        DisbursedAmount = ExcelHelper.GetString(ws, row.RowNumber(), 31),
                        LPFAmount = ExcelHelper.GetString(ws, row.RowNumber(), 32),
                        Installments = ExcelHelper.GetInt(ws, row.RowNumber(), 33),
                        InstallmentAmount = ExcelHelper.GetString(ws, row.RowNumber(), 34),
                        PaymentFrequency = ExcelHelper.GetString(ws, row.RowNumber(), 35),
                        PrincipleOutstanding = ExcelHelper.GetString(ws, row.RowNumber(), 36),
                        InterestReceivable = ExcelHelper.GetString(ws, row.RowNumber(), 37),
                        NonCreditCustomer = ExcelHelper.GetString(ws, row.RowNumber(), 38),
                        VoluntaryDepositor = ExcelHelper.GetString(ws, row.RowNumber(), 39),
                        PovertyScore = ExcelHelper.GetString(ws, row.RowNumber(), 40),
                        HouseholdSurplusIncome = ExcelHelper.GetString(ws, row.RowNumber(), 41),
                        Purpose = ExcelHelper.GetString(ws, row.RowNumber(), 42),
                        BusinessCategory = ExcelHelper.GetString(ws, row.RowNumber(), 43),
                        BusinessActivity = ExcelHelper.GetString(ws, row.RowNumber(), 44),
                        AccountStatus = ExcelHelper.GetString(ws, row.RowNumber(), 45),
                        MaturitydateLoan = ExcelHelper.GetDate(ws, row.RowNumber(), 46),
                        PARClient = ExcelHelper.GetString(ws, row.RowNumber(), 47),
                        DayOfOverDue = ExcelHelper.GetInt(ws, row.RowNumber(), 48),
                        AreaStatus = ExcelHelper.GetString(ws, row.RowNumber(), 49)
                        // CreatedOn will use default value
                    };

                    customers.Add(customer);
                }

                if (customers.Count > 0) {
                    //remove prev data
                    db.CustomerLoan.RemoveRange(db.CustomerLoan);
                    db.SaveChanges();
                }

                // Bulk insert                
                db.CustomerLoan.AddRange(customers);
                db.SaveChanges();                
            }

            return View(db.CustomerLoan.ToList());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerLoanInformation customerLoanInformation, HttpPostedFileBase PI_CustomerPhoto)
        {
            if (ModelState.IsValid)
            {
                //if (PI_CustomerPhoto != null && PI_CustomerPhoto.ContentLength > 0)
                //{
                //    using (var reader = new System.IO.BinaryReader(PI_CustomerPhoto.InputStream))
                //    {
                //        customerLoanInformation.PI_CustomerPhoto = reader.ReadBytes(PI_CustomerPhoto.ContentLength);
                //    }
                //}
                db.CustomerLoanInformations.Add(customerLoanInformation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountTypes = new SelectList(new[] { "Individual", "Joint" }, customerLoanInformation.AccountType);
            ViewBag.Genders = new SelectList(new[] { "Male", "Female", "Other" }, customerLoanInformation.PI_Gender);
            ViewBag.MaritalStatuses = new SelectList(new[] { "Single", "Married", "Divorced", "Widowed" }, customerLoanInformation.PI_MaritalStatus);
            ViewBag.EducationalQualifications = new SelectList(new[] { "Primary", "Middle", "High School", "Graduate", "Post Graduate" }, customerLoanInformation.PI_EducationalQualification);
            ViewBag.CustomerStatuses = new SelectList(new[] { "Active", "Inactive" }, customerLoanInformation.CustomerStatus);
            ViewBag.States = new SelectList(db.States, "StateName", "StateName", customerLoanInformation.PI_StateRegion);
            ViewBag.Districts = new SelectList(db.Districts, "DistrictName", "DistrictName", customerLoanInformation.PI_District);
            ViewBag.Cities = new SelectList(db.Cities, "CityName", "CityName", customerLoanInformation.PI_District);
            ViewBag.VillageTracts = new SelectList(db.VillageTracts, "VillageTractName", "VillageTractName", customerLoanInformation.PI_VillageTractTown);
            ViewBag.Villages = new SelectList(db.Villages, "VillageName", "VillageName", customerLoanInformation.PI_VillageWard);
            ViewBag.Areas = new SelectList(db.Areas, "AreaName", "AreaName", customerLoanInformation.PI_Area);
            ViewBag.Branches = new SelectList(db.AllianceBranches, "BranchName", "BranchName", customerLoanInformation.Branch);
            ViewBag.Products = new SelectList(db.Products, "ProductName", "ProductName", customerLoanInformation.ProductInterested);
            return View(customerLoanInformation);
        }

        // GET: CustomerLoanInformation/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerLoan customerLoan = db.CustomerLoan.Find(id);
            if (customerLoan == null)
            {
                return HttpNotFound();
            }

            //PopulateDropdowns();
            return View(customerLoan);
        }

        private void PopulateDropdowns()
        {
            ViewBag.AccountTypes = new SelectList(new[] { "Individual", "Joint" }, "Value", "Text");
            ViewBag.Genders = new SelectList(new[] { "Male", "Female", "Other" }, "Value", "Text");
            ViewBag.MaritalStatuses = new SelectList(new[] { "Single", "Married", "Divorced", "Widowed" }, "Value", "Text");
            ViewBag.EducationalQualifications = new SelectList(new[] { "Primary", "Middle", "High School", "Graduate", "Post Graduate" }, "Value", "Text");
            ViewBag.CustomerStatuses = new SelectList(new[] { "Active", "Inactive" }, "Value", "Text");

            ViewBag.States = new SelectList(db.States.Select(d => new { Value = d.StateCode, Text = d.StateCode }), "Value", "Text");
            ViewBag.Districts = new SelectList(db.Districts.Select(d => new { Value = d.DistrictCode, Text = d.DistrictName }), "Value", "Text");
            ViewBag.Cities = new SelectList(db.Cities.Select(c => new { Value = c.CityCode, Text = c.CityName }), "Value", "Text");
            ViewBag.VillageTracts = new SelectList(db.VillageTracts.Select(vt => new { Value = vt.VillageTractCode, Text = vt.VillageTractName }), "Value", "Text");
            ViewBag.Villages = new SelectList(db.Villages.Select(v => new { Value = v.VillageCode, Text = v.VillageName }), "Value", "Text");
            ViewBag.Branches = new SelectList(db.Branches.Select(b => new { Value = b.Id, Text = b.Name }), "Value", "Text");
            ViewBag.Products = new SelectList(db.Products.Select(p => new { Value = p.Id, Text = p.Name }), "Value", "Text");
            ViewBag.Areas = new SelectList(db.Areas.Select(p => new { Value = p.AreaName, Text = p.AreaName }), "Value", "Text");

        }

        // POST: CustomerLoanInformation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerLoanInformation customerLoanInformation, HttpPostedFileBase PI_CustomerPhoto)
        {
            if (ModelState.IsValid)
            {
                //if (PI_CustomerPhoto != null && PI_CustomerPhoto.ContentLength > 0)
                //{
                //    using (var reader = new System.IO.BinaryReader(PI_CustomerPhoto.InputStream))
                //    {
                //        customerLoanInformation.PI_CustomerPhoto = reader.ReadBytes(PI_CustomerPhoto.ContentLength);
                //    }
                //}
                customerLoanInformation.ModifiedOn = DateTime.Now;
                db.Entry(customerLoanInformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountTypes = new SelectList(new[] { "Individual", "Joint" }, customerLoanInformation.AccountType);
            ViewBag.Genders = new SelectList(new[] { "Male", "Female", "Other" }, customerLoanInformation.PI_Gender);
            ViewBag.MaritalStatuses = new SelectList(new[] { "Single", "Married", "Divorced", "Widowed" }, customerLoanInformation.PI_MaritalStatus);
            ViewBag.EducationalQualifications = new SelectList(new[] { "Primary", "Middle", "High School", "Graduate", "Post Graduate" }, customerLoanInformation.PI_EducationalQualification);
            ViewBag.CustomerStatuses = new SelectList(new[] { "Active", "Inactive" }, customerLoanInformation.CustomerStatus);
            ViewBag.States = new SelectList(db.States, "StateName", "StateName", customerLoanInformation.PI_StateRegion);
            ViewBag.Districts = new SelectList(db.Districts, "DistrictName", "DistrictName", customerLoanInformation.PI_District);
            ViewBag.Cities = new SelectList(db.Cities, "CityName", "CityName", customerLoanInformation.PI_City);
            ViewBag.VillageTracts = new SelectList(db.VillageTracts, "VillageTractName", "VillageTractName", customerLoanInformation.PI_VillageTractTown);
            ViewBag.Villages = new SelectList(db.Villages, "VillageName", "VillageName", customerLoanInformation.PI_VillageWard);
            ViewBag.Areas = new SelectList(db.Areas, "AreaName", "AreaName", customerLoanInformation.PI_Area);
            ViewBag.Branches = new SelectList(db.AllianceBranches, "BranchName", "BranchName", customerLoanInformation.Branch);
            ViewBag.Products = new SelectList(db.Products, "ProductName", "ProductName", customerLoanInformation.ProductInterested);
            return View(customerLoanInformation);
        }

        // GET: CustomerLoanInformation/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerLoanInformation customerLoanInformation = db.CustomerLoanInformations.Find(id);
            if (customerLoanInformation == null)
            {
                return HttpNotFound();
            }
            return View(customerLoanInformation);
        }

        // POST: CustomerLoanInformation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CustomerLoanInformation customerLoanInformation = db.CustomerLoanInformations.Find(id);
            db.CustomerLoanInformations.Remove(customerLoanInformation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private DateTime? SafeDate(DateTime? dt)
        {
            if (!dt.HasValue) return null;
            if (dt.Value < new DateTime(1753, 1, 1))
                return null;  // or set to min allowed date
            return dt;
        }

        private string GetString(ExcelWorksheet ws, int row, int col)
        {
            return ws.Cells[row, col].Text?.Trim();
        }

        public static int? GetInt(ExcelWorksheet ws, int row, int col)
        {
            var val = ws.Cells[row, col].Text;
            return int.TryParse(val, out var i) ? (int?)i : null;
        }

        private DateTime? GetDate(ExcelWorksheet ws, int row, int col)
        {
            var text = ws.Cells[row, col].Text;

            if (DateTime.TryParseExact(
                text,
                "dd-MM-yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var dt))
            {
                return dt;
            }
            return null;
        }

        private string GetAmount(ExcelWorksheet ws, int row, int col)
        {
            return ws.Cells[row, col].Text?
                .Replace("\u00A0", "") // remove non-breaking space
                .Trim();
        }

    }
}
