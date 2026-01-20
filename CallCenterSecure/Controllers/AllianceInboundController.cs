using CallCenter.CustomAuthentication;
using CallCenter.Models;
using CallCenter.Models.ViewModels;
using CallCenterSecure.Models;
using CallCenterSecure.Models.Inbound;
using CallCenterSecure.Repositories;
using Dapper;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;   // or Npgsql/MySql depending on DB
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CallCenter.Controllers
{
    public class AllianceInboundController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private CustomerRepository customerRepository = new CustomerRepository();

        // GET: AllianceInbound/Create
        public ActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }
        public JsonResult GetBranches(string region)
        {
            var branches = db.RegionBranches
                             .Where(r => r.Region == region)
                             .Select(r => new
                             {
                                 Value = r.Id,
                                 Text = r.BranchName
                             })
                             .ToList();
            return Json(branches, JsonRequestBehavior.AllowGet);
        }

        // POST: AllianceInbound/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AllianceInbound allianceInbound)
        {
            bool isCreate = false;
            try
            {
                allianceInbound.AgentName = ((CustomPrincipal)User).FirstName + " " + ((CustomPrincipal)User).LastName;
                allianceInbound.CallStartDateTime = DateTime.Now;
                string ticketID = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
                allianceInbound.TicketID = ticketID;

                //assign id ot region and branch
                if (allianceInbound.RegionId > 0)
                {
                    allianceInbound.Region = Convert.ToString(allianceInbound.RegionId);
                }
                if (allianceInbound.BranchId > 0)
                {
                    allianceInbound.Branch = Convert.ToString(allianceInbound.BranchId);
                }

                //Conditional NA
                if (allianceInbound.TicketType == "1" && string.IsNullOrWhiteSpace(allianceInbound.Na_Disposition))// NA
                {
                    ModelState.AddModelError("Na_Disposition", "Na disposition is required.");
                }

                if (allianceInbound.TicketType == "2")//Lead
                {
                    if (string.IsNullOrWhiteSpace(allianceInbound.Lead_CustomerName))
                    {
                        ModelState.AddModelError("Lead_CustomerName", "Lead customer name is required.");
                    }
                    if (string.IsNullOrWhiteSpace(allianceInbound.Lead_PrimaryMobileNumber))
                    {
                        ModelState.AddModelError("Lead_PrimaryMobileNumber", "Lead primary mobile number is required.");
                    }
                    if (string.IsNullOrWhiteSpace(allianceInbound.Lead_LeadStatus))
                    {
                        ModelState.AddModelError("Lead_LeadStatus", "Lead status is required.");
                    }
                }
                //CMP
                if (allianceInbound.TicketType == "3" && string.IsNullOrWhiteSpace(allianceInbound.Cmp_Disposition))//Complaint
                {
                    ModelState.AddModelError("Cmp_Disposition", "Cmp disposition is required.");
                }

                if (ModelState.IsValid)
                {
                    if (allianceInbound.File != null && allianceInbound.File.ContentLength > 0)
                    {
                        // Generate a unique file name
                        var fileName = Path.GetFileName(allianceInbound.File.FileName);
                        var uniqueFileName = fileName + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Hour +
                                            DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + Path.GetExtension(fileName);

                        // Save the file to a folder
                        var path = Path.Combine(Server.MapPath("~/Uploads"), uniqueFileName);
                        allianceInbound.File.SaveAs(path);

                        allianceInbound.FileName = uniqueFileName;
                        allianceInbound.FilePath = path;
                    }

                    db.AllianceInbounds.Add(allianceInbound);
                    db.SaveChanges();
                    isCreate = true;
                    TempData["SuccessMessage"] = "Record created successfully!";
                }

                PopulateDropdowns();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Something went wrong. Please try again.";
            }

            if (isCreate)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(allianceInbound);
            }
        }

        // GET: AllianceInbound/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AllianceInbound allianceInbound = db.AllianceInbounds.Find(id);
            if (allianceInbound == null)
            {
                return HttpNotFound();
            }

            //clear spacing
            allianceInbound.Cmp_Branch = Normalize(allianceInbound.Cmp_Branch);
            allianceInbound.Cmp_Disposition = Normalize(allianceInbound.Cmp_Disposition);

            int.TryParse(allianceInbound.Region, out int regionId);
            allianceInbound.RegionId = regionId;

            int.TryParse(allianceInbound.Branch, out int branchId);
            allianceInbound.BranchId = branchId;

            PopulateDropdowns();

            return View(allianceInbound);
        }

        // POST: AllianceInbound/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AllianceInbound allianceInbound)
        {
            allianceInbound.ModifiedOn = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    //Verify if Prev Ticket Id provided
                    //If exist create new ticket

                    //assign id ot region and branch
                    if (allianceInbound.RegionId > 0)
                    {
                        allianceInbound.Region = Convert.ToString(allianceInbound.RegionId);
                    }
                    if (allianceInbound.BranchId > 0)
                    {
                        allianceInbound.Branch = Convert.ToString(allianceInbound.BranchId);
                    }

                    if (allianceInbound.File != null && allianceInbound.File.ContentLength > 0)
                    {
                        // Generate a unique file name
                        var fileName = Path.GetFileName(allianceInbound.File.FileName);
                        var uniqueFileName = fileName.Replace(Path.GetExtension(fileName), "") + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Hour +
                                            DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + Path.GetExtension(fileName);

                        // Save the file to a folder
                        var path = Path.Combine(Server.MapPath("~/Uploads"), uniqueFileName);
                        allianceInbound.File.SaveAs(path);

                        allianceInbound.FileName = uniqueFileName;
                        allianceInbound.FilePath = path;
                    }

                    if (!string.IsNullOrWhiteSpace(allianceInbound.Prev_TicketId))
                    {
                        allianceInbound.CallStartDateTime = DateTime.Now;
                        string ticketID = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
                        allianceInbound.TicketID = ticketID;

                        //Blank prev ticket for new 
                        allianceInbound.Prev_TicketId = allianceInbound.Prev_TicketId;

                        db.Entry(allianceInbound).State = EntityState.Added;
                        db.AllianceInbounds.Add(allianceInbound);
                        db.SaveChanges();
                        TempData["SuccessMessage"] = "New ticket created successfully!";
                    }
                    else
                    {
                        db.Entry(allianceInbound).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["SuccessMessage"] = "Record updated successfully!";
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            PopulateDropdowns();
            return View(allianceInbound);
        }

        private void PopulateDropdowns()
        {
            // Load distinct region names
            //var cascadeRegion = db.RegionBranches
            //.Select(r => r.Region)
            //.Distinct()
            //.ToList();

            ViewBag.CascadeRegion = new SelectList(db.RegionBranches.Select(c => new { Value = c.Id, Text = c.Region }).Distinct(), "Value", "Text");
            //ViewBag.Branches = new SelectList(db.AllianceBranches.Select(b => new { Value = b.BranchName, Text = b.BranchName }), "Value", "Text");
            //ViewBag.Branches = new SelectList(db.RegionBranches.Select(b => new { Value = b.BranchName, Text = b.BranchName }).ToList(), "Value", "Text");

            //ViewBag.Branches = new SelectList(
            //db.RegionBranches
            //  .Where(d => d.BranchName != null && d.BranchName != "")
            //  .Select(d => d.BranchName)
            //  .Distinct()
            //  .OrderBy(d => d)
            //  .Select(d => new { Value = Normalize(d), Text = Normalize(d) }),
            //"Value",
            //"Text"
            //);
            ViewBag.Branches = new SelectList(
            db.RegionBranches
              .Where(d => d.BranchName != null && d.BranchName != "")
              .Select(d => new { d.Id, d.BranchName })
              .Distinct()
              .OrderBy(d => d)
              .ToList() // ⭐ VERY IMPORTANT (execute SQL here)
              .Select(d => new
              {
                  Value = d.Id,
                  Text = Normalize(d.BranchName)
              }),
                    "Value",
            "Text"
            );

            //ViewBag.CascadeRegion = new SelectList(cascadeRegion);

            // Branches will load dynamically via AJAX (empty initially)
            //ViewBag.CascadeBranch = new SelectList(new List<string>());

            ViewBag.CallObjectives = new SelectList(db.CallObjectives.Select(c => new { Value = c.Id, Text = c.Name }), "Value", "Text");
            ViewBag.Regions = new SelectList(db.Regions.Select(r => new { Value = r.Id, Text = r.Name }), "Value", "Text");
            ViewBag.NaDispositions = new SelectList(db.NaDisposition.Select(c => new { Value = c.Id, Text = c.Name }), "Value", "Text");
            ViewBag.CmpDispositions = new SelectList(db.CmpDisposition.Select(c => new { Value = c.Id, Text = c.Name }), "Value", "Text");

            ViewBag.Origins = new SelectList(db.Origins.Select(o => new { Value = o.Id, Text = o.Name }), "Value", "Text");
            ViewBag.Products = new SelectList(db.Products.Select(p => new { Value = p.Id, Text = p.Name }), "Value", "Text");
            ViewBag.TicketTypes = new SelectList(db.TicketTypes.Select(t => new { Value = t.Id, Text = t.Name }), "Value", "Text");
            ViewBag.TicketStatuses = new SelectList(db.TicketStatuses.Select(ts => new { Value = ts.Id, Text = ts.Name }), "Value", "Text");

            // Additional dropdowns for dependent fields
            ViewBag.Districts = new SelectList(db.Districts.Select(d => new { Value = d.DistrictCode, Text = d.DistrictName }), "Value", "Text");
            ViewBag.Cities = new SelectList(db.Cities.Select(c => new { Value = c.CityCode, Text = c.CityName }), "Value", "Text");
            ViewBag.VillageTracts = new SelectList(db.VillageTracts.Select(vt => new { Value = vt.VillageTractCode, Text = vt.VillageTractName }), "Value", "Text");
            ViewBag.Villages = new SelectList(db.Villages.Select(v => new { Value = v.VillageCode, Text = v.VillageName }), "Value", "Text");


            ViewBag.Designations = new SelectList(
            db.AllianceDesignations
              .Where(d => d.Designation != null && d.Designation != "")
              .Select(d => d.Designation)
              .Distinct()
              .OrderBy(d => d)
              .Select(d => new { Value = d, Text = d }),
            "Value",
            "Text"
            );
            ViewBag.ComplainTo = new SelectList(db.Designations.Select(d => new { Value = d.DesignationId, Text = d.Designation }).OrderBy(d => d.Text), "Value", "Text");

            ViewBag.Cmp_Master_Designations = new SelectList(db.ComplaintDesignations.Select(d => new { Value = d.ComplaintDesignationId, Text = d.Description }).OrderBy(d => d.Text), "Value", "Text");

            ViewBag.NatureOfComplaints = new SelectList(db.NatureOfComplaint.Select(d => new { Value = d.ComplaintId, Text = d.ComplaintsDescrption }).OrderBy(d => d.Text), "Value", "Text");

            ViewBag.CitizenList = new SelectList(
                  db.Citizen
                    .Select(c => new
                    {
                        Value = c.Code,
                        Text = c.Code + " - " + c.Reference
                    })
                    .ToList(),
                  "Value",
                  "Text"
              );
            ViewBag.StateDivisionList = new SelectList(
                db.StateDivision
                  .Select(s => new
                  {
                      Value = s.Id, // or s.StateDivisionCode if you prefer
                      Text = s.StateDivisionCode + " - " + s.StateDivisionName
                  })
                  .ToList(),
                "Value",
                "Text"
            );

        }
        private void PopulateDropdownsIndex()
        {
            ViewBag.TicketTypes = new SelectList(db.TicketTypes.Select(t => new { Value = t.Id, Text = t.Name }), "Value", "Text");
            ViewBag.TicketStatuses = new SelectList(db.TicketStatuses.Select(ts => new { Value = ts.Id, Text = ts.Name }), "Value", "Text");
        }

        public ActionResult Index(AllianceInboundIndexVM filter)
        {
            var query = customerRepository.GetDataAll();

            if (filter.FromDate.HasValue)
                query = query.Where(x => x.DateTime.Date >= filter.FromDate.Value.Date);

            if (filter.ToDate.HasValue)
                query = query.Where(x => x.DateTime.Date <= filter.ToDate.Value.Date);

            if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
            {
                query = query.Where(x =>
                    x.PhoneNumber != null &&
                    x.PhoneNumber.Contains(filter.PhoneNumber));
            }

            if (!string.IsNullOrWhiteSpace(filter.TicketTypeId))
                query = query.Where(x => x.TicketTypeId == filter.TicketTypeId);

            if (!string.IsNullOrWhiteSpace(filter.TicketStatusId))
                query = query.Where(x => x.TicketStatusId == filter.TicketStatusId);

            if (!string.IsNullOrEmpty(filter.TicketId))
                query = query.Where(x => x.TicketID == filter.TicketId);

            if (query != null)
            {
                filter.InboundList = query.ToList();
            }

            PopulateDropdownsIndex();

            return View(filter);
        }


        // GET: AllianceInbound/Index
        //public ActionResult Index()
        //{
        //    var inBound = db.AllianceInbounds.ToList();
        //    return View(inBound);
        //}
        //public ActionResult Index()
        //{

        //    var inBound = db.AllianceInbounds.ToList();


        //    foreach (var item in inBound)
        //    {
        //        int num = Convert.ToInt32(item.TicketType);
        //        item.TicketTypeName = db.TicketTypes.Where(tt => tt.Id == num).Select(p => p.Name).FirstOrDefault();
        //        item.BranchName = db.AllianceBranches.Where(tt => tt.BranchCode == item.Branch).Select(p => p.BranchName).FirstOrDefault();
        //        num = Convert.ToInt32(item.TicketStatus);
        //        item.TicketStatusName = db.TicketStatuses.Where(tt => tt.Id == num).Select(p => p.Name).FirstOrDefault();
        //        num = Convert.ToInt32(item.CallObjective);
        //        item.CallObjectiveName = db.CallObjectives.Where(tt => tt.Id == num).Select(p => p.Name).FirstOrDefault();
        //        num = Convert.ToInt32(item.Region);
        //        item.RegionName = db.Regions.Where(tt => tt.Id == num).Select(p => p.Name).FirstOrDefault();

        //        num = Convert.ToInt32(item.Origin);
        //        item.OriginName = db.Origins.Where(tt => tt.Id == num).Select(p => p.Name).FirstOrDefault();
        //        //Lead Branch State etc
        //        num = Convert.ToInt32(item.Lead_Branch);
        //        item.LeadBranchName = db.Branches.Where(tt => tt.Id == num).Select(p => p.Name).FirstOrDefault();

        //        num = Convert.ToInt32(item.Lead_StateRegion);
        //        item.LeadStateRegionName = db.Regions.Where(tt => tt.Id == num).Select(p => p.Name).FirstOrDefault();

        //        num = Convert.ToInt32(item.Lead_ProductInterested);
        //        item.LeadProductInterestedName = db.Products.Where(tt => tt.Id == num).Select(p => p.Name).FirstOrDefault();

        //        item.LeadDistrictName = db.Districts.Where(tt => tt.DistrictCode == item.Lead_District).Select(p => p.DistrictName).FirstOrDefault();
        //        item.LeadCityTownshipName = db.Cities.Where(tt => tt.CityCode == item.Lead_CityTownship).Select(p => p.CityName).FirstOrDefault();
        //        item.LeadVillageTractTownName = db.Villages.Where(tt => tt.VillageCode == item.Lead_VillageTractTown).Select(p => p.VillageName).FirstOrDefault();
        //        item.LeadVillageWardName = db.VillageTracts.Where(tt => tt.VillageTractCode == item.Lead_VillageWard).Select(p => p.VillageTractName).FirstOrDefault();

        //        // Complain attributes name
        //        num = Convert.ToInt32(item.Cmp_Region);
        //        item.CmpRegionName = db.Branches.Where(tt => tt.Id == num).Select(p => p.Name).FirstOrDefault();

        //        num = Convert.ToInt32(item.Cmp_Branch);
        //        item.CmpBranchName = db.Branches.Where(tt => tt.Id == num).Select(p => p.Name).FirstOrDefault();

        //        item.CmpDistrictName = db.Districts.Where(tt => tt.DistrictCode == item.Cmp_District).Select(p => p.DistrictName).FirstOrDefault();
        //        item.CmpCityTownshipName = db.Cities.Where(tt => tt.CityCode == item.Cmp_CityTownship).Select(p => p.CityName).FirstOrDefault();
        //        item.CmpVillageTractTownName = db.Villages.Where(tt => tt.VillageCode == item.Cmp_VillageTractTown).Select(p => p.VillageName).FirstOrDefault();
        //        item.CmpVillageWardName = db.VillageTracts.Where(tt => tt.VillageTractCode == item.Cmp_VillageWard).Select(p => p.VillageTractName).FirstOrDefault();

        //        num = Convert.ToInt32(item.Product);
        //        item.ProductName = db.Products.Where(tt => tt.Id == num).Select(p => p.Name).FirstOrDefault();
        //    }

        //    AllianceInbound allianceInbound = new AllianceInbound();
        //    allianceInbound.AllianceInboundList = inBound;

        //    ViewBag.TicketTypes = new SelectList(db.TicketTypes.Select(t => new { Value = t.Id, Text = t.Name }), "Value", "Text");
        //    ViewBag.TicketStatuses = new SelectList(db.TicketStatuses.Select(ts => new { Value = ts.Id, Text = ts.Name }), "Value", "Text");

        //    //return View(inBound);
        //    return View(allianceInbound);
        //}


        // GET: AllianceInbound/Index
        [HttpPost]
        public ActionResult Index(AllianceInbound allianceInbound)
        {

            var inBound = db.AllianceInbounds.ToList();

            if (!string.IsNullOrEmpty(allianceInbound.PhoneNumber))
                inBound = inBound.Where(tl => tl.PhoneNumber == allianceInbound.PhoneNumber).ToList();
            if (!string.IsNullOrEmpty(allianceInbound.TicketStatus))
                inBound = inBound.Where(tl => tl.TicketStatus == allianceInbound.TicketStatus).ToList();
            if (!string.IsNullOrEmpty(allianceInbound.TicketType))
                inBound = inBound.Where(tl => tl.TicketType == allianceInbound.TicketType).ToList();

            if (allianceInbound.FromDate != null)
                inBound = inBound.Where(tl => tl.DateTime != null && Convert.ToDateTime(tl.DateTime).Date >= Convert.ToDateTime(allianceInbound.FromDate).Date).ToList();
            if (allianceInbound.ToDate != null)
                inBound = inBound.Where(tl => tl.DateTime != null && Convert.ToDateTime(tl.DateTime).Date <= Convert.ToDateTime(allianceInbound.ToDate).Date).ToList();
            if (!string.IsNullOrWhiteSpace(allianceInbound.TicketID))
            {
                inBound = inBound.Where(tl => tl.TicketID == allianceInbound.TicketID).ToList();
            }

            foreach (var item in inBound)
            {
                int num = Convert.ToInt32(item.TicketType);
                item.TicketTypeName = db.TicketTypes.Where(tt => tt.Id == num).Select(p => p.Name).FirstOrDefault();
                item.BranchName = db.AllianceBranches.Where(tt => tt.BranchCode == item.Branch).Select(p => p.BranchName).FirstOrDefault();
                num = Convert.ToInt32(item.TicketStatus);
                item.TicketStatusName = db.TicketStatuses.Where(tt => tt.Id == num).Select(p => p.Name).FirstOrDefault();
                num = Convert.ToInt32(item.CallObjective);
                item.CallObjectiveName = db.CallObjectives.Where(tt => tt.Id == num).Select(p => p.Name).FirstOrDefault();
                num = Convert.ToInt32(item.Region);
                item.RegionName = db.Regions.Where(tt => tt.Id == num).Select(p => p.Name).FirstOrDefault();
                num = Convert.ToInt32(item.Origin);
                item.OriginName = db.Origins.Where(tt => tt.Id == num).Select(p => p.Name).FirstOrDefault();
                //Lead Branch State etc
                num = Convert.ToInt32(item.Lead_Branch);
                item.LeadBranchName = db.Branches.Where(tt => tt.Id == num).Select(p => p.Name).FirstOrDefault();

                num = Convert.ToInt32(item.Lead_StateRegion);
                item.LeadStateRegionName = db.Regions.Where(tt => tt.Id == num).Select(p => p.Name).FirstOrDefault();

                num = Convert.ToInt32(item.Lead_ProductInterested);
                item.LeadProductInterestedName = db.Products.Where(tt => tt.Id == num).Select(p => p.Name).FirstOrDefault();

                item.LeadDistrictName = db.Districts.Where(tt => tt.DistrictCode == item.Lead_District).Select(p => p.DistrictName).FirstOrDefault();
                item.LeadCityTownshipName = db.Cities.Where(tt => tt.CityCode == item.Lead_CityTownship).Select(p => p.CityName).FirstOrDefault();
                item.LeadVillageTractTownName = db.Villages.Where(tt => tt.VillageCode == item.Lead_VillageTractTown).Select(p => p.VillageName).FirstOrDefault();
                item.LeadVillageWardName = db.VillageTracts.Where(tt => tt.VillageTractCode == item.Lead_VillageWard).Select(p => p.VillageTractName).FirstOrDefault();

                // Complain attributes name
                num = Convert.ToInt32(item.Cmp_Region);
                item.CmpRegionName = db.Branches.Where(tt => tt.Id == num).Select(p => p.Name).FirstOrDefault();

                num = Convert.ToInt32(item.Cmp_Branch);
                item.CmpBranchName = db.Branches.Where(tt => tt.Id == num).Select(p => p.Name).FirstOrDefault();

                item.CmpDistrictName = db.Districts.Where(tt => tt.DistrictCode == item.Cmp_District).Select(p => p.DistrictName).FirstOrDefault();
                item.CmpCityTownshipName = db.Cities.Where(tt => tt.CityCode == item.Cmp_CityTownship).Select(p => p.CityName).FirstOrDefault();
                item.CmpVillageTractTownName = db.Villages.Where(tt => tt.VillageCode == item.Cmp_VillageTractTown).Select(p => p.VillageName).FirstOrDefault();
                item.CmpVillageWardName = db.VillageTracts.Where(tt => tt.VillageTractCode == item.Cmp_VillageWard).Select(p => p.VillageTractName).FirstOrDefault();

            }

            allianceInbound.AllianceInboundList = inBound;

            ViewBag.TicketTypes = new SelectList(db.TicketTypes.Select(t => new { Value = t.Id, Text = t.Name }), "Value", "Text");
            ViewBag.TicketStatuses = new SelectList(db.TicketStatuses.Select(ts => new { Value = ts.Id, Text = ts.Name }), "Value", "Text");

            //return View(inBound);
            return View(allianceInbound);
        }

        // GET: AllianceInbound/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var allianceInbound = customerRepository.GetData(id);

            ViewBag.TicketType = allianceInbound != null ? allianceInbound.FirstOrDefault().TicketID : "NA";

            if (allianceInbound == null)
            {
                return HttpNotFound();
            }
            return View(allianceInbound.FirstOrDefault());
        }

        // GET: AllianceInbound/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllianceInbound allianceInbound = db.AllianceInbounds.Find(id);
            if (allianceInbound == null)
            {
                return HttpNotFound();
            }
            return View(allianceInbound);
        }

        // POST: AllianceInbound/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AllianceInbound allianceInbound = db.AllianceInbounds.Find(id);
            db.AllianceInbounds.Remove(allianceInbound);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DownloadFile(string fileName)
        {
            // Specify the folder where your files are stored
            var folder = Server.MapPath("~/Uploads/");
            var path = Path.Combine(folder, fileName);

            // Check if file exists
            if (!System.IO.File.Exists(path))
            {
                return HttpNotFound(); // Return a 404 error if the file is not found
            }

            // Return the file to download
            return File(path, "application/octet-stream", fileName);
        }

        public ActionResult GetDesignations(string branchId)
        {

            var branchName = db.AllianceBranches.Where(b => b.BranchCode == branchId).Select(bn => bn.BranchType).FirstOrDefault();
            var designations = db.AllianceDesignations
                                .Where(d => d.Branch == branchName)
                                .Select(d => new
                                {
                                    DesignationID = d.DesignationID,
                                    Designation = d.Designation
                                }).ToList().OrderBy(d => d.Designation);

            return Json(designations, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ExportAllianceInboundCsv(AllianceInboundIndexVM filter)
        {
            try
            {
                var data = customerRepository.GetDataAllExcel();

                //Filter data
                if (filter.FromDate.HasValue)
                    data = data.Where(x => x.DateTime.Date >= filter.FromDate.Value.Date);

                if (filter.ToDate.HasValue)
                    data = data.Where(x => x.DateTime.Date <= filter.ToDate.Value.Date);

                if (!string.IsNullOrEmpty(filter.PhoneNumber))
                    data = data.Where(x => x.PhoneNumber.Contains(filter.PhoneNumber));

                if (!string.IsNullOrWhiteSpace(filter.TicketTypeId))
                {
                    int.TryParse(filter.TicketTypeId, out int tcktTypeId);
                    data = data.Where(x => x.TicketTypeId == tcktTypeId);
                }

                if (!string.IsNullOrWhiteSpace(filter.TicketStatusId))
                {
                    int.TryParse(filter.TicketStatusId, out int tcktStatusId);
                    data = data.Where(x => x.TicketStatusId == tcktStatusId);
                }

                if (!string.IsNullOrEmpty(filter.TicketId))
                    data = data.Where(x => x.TicketID == filter.TicketId);


                // Columns to hide
                var excludedColumns = new HashSet<string>
                {
                    "TicketTypeId",
                    "TicketStatusId"
                };

                var sb = new StringBuilder();
                var props = typeof(AllianceInboundExcelModel).GetProperties().Where(p => !excludedColumns.Contains(p.Name)).ToArray();

                // Header
                sb.AppendLine(string.Join(",", props.Select(p => p.Name)));

                // Rows
                foreach (var row in data)
                {
                    sb.AppendLine(string.Join(",", props.Select(p =>
                        $"\"{(p.GetValue(row)?.ToString() ?? string.Empty).Replace("\"", "\"\"")}\""
                    )));
                }

                var bytes = Encoding.UTF8.GetBytes(sb.ToString());

                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=AllianceInbound.csv");
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.SuppressContent = false;
            }
            catch (Exception ex)
            {

            }

            return new EmptyResult();
        }

        private string Normalize(string value)
        {
            return value?
                .Replace("\u00A0", " ")
                .Trim();
        }

        public JsonResult GetTownshipsByStateDivision(int stateDivisionCode)
        {
            var townships = db.Township
                              .Where(t => t.StateDivisionCode == stateDivisionCode)
                              .Select(t => new
                              {
                                  Value = t.TownshipCode,
                                  Text = t.TownshipCode + " - " + t.TownshipName
                              })
                              .ToList();

            return Json(townships, JsonRequestBehavior.AllowGet);
        }

    }
}
