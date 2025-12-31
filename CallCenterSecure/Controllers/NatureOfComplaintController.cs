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
    public class NatureOfComplaintsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ComplaintDesignation
        public ActionResult Index()
        {
            return View(db.NatureOfComplaint.ToList());
        }

        // GET: ComplaintDesignation/Details/5
        public ActionResult Details(int id)
        {
            if (id ==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NatureOfComplaints natureOfComplaints = db.NatureOfComplaint.Find(id);
            if (natureOfComplaints == null)
            {
                return HttpNotFound();
            }
            return View(natureOfComplaints);
        }

        // GET: ComplaintDesignation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AllianceBranch/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NatureOfComplaints natureOfComplaints)
        {
            if (ModelState.IsValid)
            {
                db.NatureOfComplaint.Add(natureOfComplaints);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(natureOfComplaints);
        }

        // GET: AllianceBranch/Edit/5
        public ActionResult Edit(int id)
        {
            if (id ==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NatureOfComplaints natureOfComplaints = db.NatureOfComplaint.Find(id);
            if (natureOfComplaints == null)
            {
                return HttpNotFound();
            }
            return View(natureOfComplaints);
        }

        // POST: AllianceBranch/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NatureOfComplaints natureOfComplaints)
        {
            if (ModelState.IsValid)
            {
                db.Entry(natureOfComplaints).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(natureOfComplaints);
        }

        public ActionResult ToggleActive(int id)
        {

            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var natureOfComplaints = dbContext.NatureOfComplaint.FirstOrDefault(x => x.ComplaintId == id);

                if (natureOfComplaints == null)
                    return HttpNotFound();

                natureOfComplaints.IsActive = !natureOfComplaints.IsActive; // toggle

                dbContext.SaveChanges();
            }

            return RedirectToAction("Index"); // reload grid
        }
    }
}
