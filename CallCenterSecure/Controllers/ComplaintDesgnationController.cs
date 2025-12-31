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
using DocumentFormat.OpenXml.Wordprocessing;

namespace CallCenter.Controllers
{
    public class ComplaintDesignationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ComplaintDesignation
        public ActionResult Index()
        {
            return View(db.ComplaintDesignations.ToList());
        }

        // GET: ComplaintDesignation/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComplaintDesignation complaintdesignation = db.ComplaintDesignations.Find(id);
            if (complaintdesignation == null)
            {
                return HttpNotFound();
            }
            return View(complaintdesignation);
        }

        // GET: ComplaintDesignation/Create
        public ActionResult Create()
        {
            ViewBag.HeadOffices = new SelectList(AllianceBranch.HeadOffices);
            ViewBag.RegionalOffices = new SelectList(AllianceBranch.RegionalOffices);
            ViewBag.ParentBranches = new SelectList(db.AllianceBranches, "BranchCode", "BranchName");
            return View();
        }

        // POST: AllianceBranch/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ComplaintDesignation complaintDesignation)
        {
            if (ModelState.IsValid)
            {
                db.ComplaintDesignations.Add(complaintDesignation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(complaintDesignation);
        }

        // GET: AllianceBranch/Edit/5
        public ActionResult Edit(int id)
        {
            if (id==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComplaintDesignation complaintDesignation = db.ComplaintDesignations.Find(id);
            if (complaintDesignation == null)
            {
                return HttpNotFound();
            }
            return View(complaintDesignation);
        }

        // POST: AllianceBranch/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ComplaintDesignation complaintDesignation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(complaintDesignation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(complaintDesignation);
        }

        //// GET: AllianceBranch/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AllianceBranch allianceBranch = db.AllianceBranches.Find(id);
        //    if (allianceBranch == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(allianceBranch);
        //}

        //// POST: AllianceBranch/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    AllianceBranch allianceBranch = db.AllianceBranches.Find(id);
        //    db.AllianceBranches.Remove(allianceBranch);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public ActionResult ToggleActive(int id)
        {

            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var complaintDesignation = dbContext.ComplaintDesignations.FirstOrDefault(x => x.ComplaintDesignationId == id);

                if (complaintDesignation == null)
                    return HttpNotFound();

                complaintDesignation.IsActive = !complaintDesignation.IsActive; // toggle

                dbContext.SaveChanges();
            }

            return RedirectToAction("Index"); // reload grid
        }
    }
}
