using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using CallCenter.CustomAuthentication;
using CallCenter.Models;
using CallCenterSecure.Models;

namespace CallCenter.Controllers
{
    public class AllianceOutboundController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AllianceOutbound/Create
        public ActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        // POST: AllianceOutbound/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AllianceOutbound allianceOutbound)
        {
            allianceOutbound.AgentName = ((CustomPrincipal)User).FirstName + " " + ((CustomPrincipal)User).LastName;
            allianceOutbound.CallStartDateTime = DateTime.Now;
            allianceOutbound.CreatedOn = DateTime.Now;
            string ticketID = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
            allianceOutbound.TicketID = ticketID;
            if (ModelState.IsValid)
            {
                db.AllianceOutbounds.Add(allianceOutbound);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateDropdowns();
            return View(allianceOutbound);
        }

        // GET: AllianceOutbound/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllianceOutbound allianceOutbound = db.AllianceOutbounds.Find(id);
            if (allianceOutbound == null)
            {
                return HttpNotFound();
            }


            PopulateDropdowns();
            return View(allianceOutbound);
        }

        // POST: AllianceOutbound/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AllianceOutbound allianceOutbound)
        {
            allianceOutbound.ModifiedOn = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(allianceOutbound.Prev_TicketId))
                    {
                        allianceOutbound.CallStartDateTime = DateTime.Now;
                        string ticketID = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
                        allianceOutbound.TicketID = ticketID;

                        //Blank prev ticket for new 
                        allianceOutbound.Prev_TicketId = allianceOutbound.Prev_TicketId;

                        db.Entry(allianceOutbound).State = EntityState.Added;
                        db.AllianceOutbounds.Add(allianceOutbound);
                        db.SaveChanges();
                        TempData["SuccessMessage"] = "New ticket created successfully!";
                    }
                    else
                    {
                        db.Entry(allianceOutbound).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["SuccessMessage"] = "Record updated successfully!";
                    }

                    //db.Entry(allianceOutbound).State = EntityState.Modified;
                    //db.SaveChanges();
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
            return View(allianceOutbound);
        }

        private void PopulateDropdowns()
        {
            ViewBag.CallObjectives = new SelectList(db.CallObjectives.Select(c => new { Value = c.Id, Text = c.Name }), "Value", "Text");
            ViewBag.Regions = new SelectList(db.Regions.Select(r => new { Value = r.Id, Text = r.Name }), "Value", "Text");
            ViewBag.Branches = new SelectList(db.AllianceBranches.Select(b => new { Value = b.BranchCode, Text = b.BranchName }), "Value", "Text");
            ViewBag.Origins = new SelectList(db.Origins.Select(o => new { Value = o.Id, Text = o.Name }), "Value", "Text");
            ViewBag.Products = new SelectList(db.Products.Select(p => new { Value = p.Id, Text = p.Name }), "Value", "Text");
            ViewBag.TicketTypes = new SelectList(db.TicketTypes.Select(t => new { Value = t.Id, Text = t.Name }), "Value", "Text");
            ViewBag.TicketStatuses = new SelectList(db.TicketStatuses.Select(ts => new { Value = ts.Id, Text = ts.Name }), "Value", "Text");

            // Additional dropdowns for dependent fields
            ViewBag.States = new SelectList(db.States.Select(d => new { Value = d.StateCode, Text = d.StateName }), "Value", "Text");
            ViewBag.Districts = new SelectList(db.Districts.Select(d => new { Value = d.DistrictCode, Text = d.DistrictName }), "Value", "Text");
            ViewBag.Cities = new SelectList(db.Cities.Select(c => new { Value = c.CityCode, Text = c.CityName }), "Value", "Text");
            ViewBag.VillageTracts = new SelectList(db.VillageTracts.Select(vt => new { Value = vt.VillageTractCode, Text = vt.VillageTractName }), "Value", "Text");
            ViewBag.Villages = new SelectList(db.Villages.Select(v => new { Value = v.VillageCode, Text = v.VillageName }), "Value", "Text");
            ViewBag.Designations = new SelectList(db.AllianceDesignations.Select(d => new { Value = d.DesignationID, Text = d.Designation }), "Value", "Text");

            ViewBag.CitizenList = new SelectList(
                db.Citizen
                  .Select(c => new {
                      Value = c.Code,
                      Text = c.Code + " - " + c.Reference
                  })
                  .ToList(),
                "Value",
                "Text"
            );

            ViewBag.StateDivisionList = new SelectList(
                db.StateDivision
                  .Select(s => new {
                      Value = s.StateDivisionCode, // or s.StateDivisionCode if you prefer
                      Text = s.StateDivisionCode + " - " + s.StateDivisionName
                  })
                  .ToList(),
                "Value",
                "Text"
            );

        }

        // GET: AllianceOutbound/Index
        public ActionResult Index()
        {
            var outBound = db.AllianceOutbounds.OrderByDescending(o=>o.AllianceOutboundId).ToList();
            foreach (var item in outBound)
            {
                int num = 0;
                num = Convert.ToInt32(item.Branch);
                item.BranchName = db.AllianceBranches.Where(tt => tt.BranchCode == item.Branch).Select(p => p.BranchName).FirstOrDefault();
                item.StateRegionName = db.States.Where(tt => tt.StateCode == item.StateRegion).Select(p => p.StateName).FirstOrDefault();
                item.DistrictName = db.Districts.Where(tt => tt.DistrictCode == item.District).Select(p => p.DistrictName).FirstOrDefault();
                item.CityTownshipName = db.Cities.Where(tt => tt.CityCode == item.CityTownship).Select(p => p.CityName).FirstOrDefault();
                item.VillageTractTownName = db.VillageTracts.Where(tt => tt.VillageTractCode == item.VillageTractTown).Select(p => p.VillageTractName).FirstOrDefault();
                num = Convert.ToInt32(item.ProductInterested);
                item.ProductInterestedName = db.Products.Where(tt => tt.Id == num).Select(p => p.Name).FirstOrDefault();
            }

            AllianceOutbound allianceOutbound = new AllianceOutbound();
            allianceOutbound.AllianceOutboundList = outBound;
            return View(allianceOutbound);
        }
        [HttpPost]
        public ActionResult Index(AllianceOutbound allianceOutbound)
        {

            var outBound = db.AllianceOutbounds.ToList();

            if (!string.IsNullOrEmpty(allianceOutbound.CustomerCode))
                outBound = outBound.Where(tl => tl.CustomerCode == allianceOutbound.CustomerCode).ToList();
            if (!string.IsNullOrEmpty(allianceOutbound.CallType))
                outBound = outBound.Where(tl => tl.CallType == allianceOutbound.CallType).ToList();
            if (!string.IsNullOrEmpty(allianceOutbound.CallStatus))
                outBound = outBound.Where(tl => tl.CallStatus == allianceOutbound.CallStatus).ToList();
            if (!string.IsNullOrEmpty(allianceOutbound.PrimaryMobileNumberSearch))
                outBound = outBound.Where(tl => tl.PrimaryMobileNumber.Contains(allianceOutbound.PrimaryMobileNumberSearch)).ToList();
            if (!string.IsNullOrEmpty(allianceOutbound.TicketID))
                outBound = outBound.Where(tl => tl.TicketID == allianceOutbound.TicketID).ToList();

            foreach (var item in outBound)
            {
                int num = 0;
                //num = Convert.ToInt32(item.Branch);
                item.BranchName = db.AllianceBranches.Where(tt => tt.BranchCode == item.Branch).Select(p => p.BranchName).FirstOrDefault();
                item.StateRegionName = db.States.Where(tt => tt.StateCode == item.StateRegion).Select(p => p.StateName).FirstOrDefault();
                item.DistrictName = db.Districts.Where(tt => tt.DistrictCode == item.District).Select(p => p.DistrictName).FirstOrDefault();
                item.CityTownshipName = db.Cities.Where(tt => tt.CityCode == item.CityTownship).Select(p => p.CityName).FirstOrDefault();
                item.VillageTractTownName = db.VillageTracts.Where(tt => tt.VillageTractCode == item.VillageTractTown).Select(p => p.VillageTractName).FirstOrDefault();
                num = Convert.ToInt32(item.ProductInterested);
                item.ProductInterestedName = db.Products.Where(tt => tt.Id == num).Select(p => p.Name).FirstOrDefault();
            }


            allianceOutbound.AllianceOutboundList = outBound;

            return View(allianceOutbound);
        }

        // GET: AllianceOutbound/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllianceOutbound allianceOutnbound = db.AllianceOutbounds.Find(id);
            if (allianceOutnbound == null)
            {
                return HttpNotFound();
            }
            return View(allianceOutnbound);
        }

        // GET: AllianceOutbound/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllianceOutbound allianceOutbound = db.AllianceOutbounds.Find(id);
            if (allianceOutbound == null)
            {
                return HttpNotFound();
            }
            return View(allianceOutbound);
        }

        //// POST: AllianceInbound/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    AllianceOutbound allianceOutbound = db.AllianceOutbounds.Find(id);
        //    db.AllianceInbounds.Remove(allianceOutbound);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        public ActionResult GetDistricts(string stateId)
        {
            var districts = db.Districts.Where(d => d.StateCode == stateId).ToList();
            return Json(districts, JsonRequestBehavior.AllowGet);
        }

        // Get CityTownship by district id
        public ActionResult GetCityTownship(string districtId)
        {
            var villages = db.Cities.Where(v => v.DistrictCode == districtId).ToList();
            return Json(villages, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportAllianceOutboundCsv()
        {
            var data = db.AllianceOutbounds
                         .OrderByDescending(x => x.AllianceOutboundId)
                         .ToList();

            var sb = new StringBuilder();

            // ✅ FIX: correct type
            var props = typeof(AllianceOutbound).GetProperties();

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
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment; filename=AllianceOutbound.csv");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.SuppressContent = false;

            return new EmptyResult();
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
