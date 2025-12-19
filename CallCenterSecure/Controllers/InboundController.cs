using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using CallCenterSecure.Models;

namespace CallCenterSecure.Controllers
{
    public class InboundController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Inbound
        public ActionResult Index()
        {
            var inboundCalls = db.InboundCalls.ToList();
            return View(inboundCalls);
        }

        // GET: Inbound/Create
        public ActionResult Create()
        {
            // Initialize dropdown data for the view
            ViewBag.CallObjectives = new SelectList(db.CallObjectives, "Id", "Name");
            ViewBag.Regions = new SelectList(db.Regions, "Id", "Name");
            ViewBag.Branches = new SelectList(db.Branches, "Id", "Name");
            ViewBag.Origins = new SelectList(db.Origins, "Id", "Name");
            ViewBag.Products = new SelectList(db.Products, "Id", "Name");
            ViewBag.TicketTypes = new SelectList(db.TicketTypes, "Id", "Name");
            ViewBag.TicketStatuses = new SelectList(db.TicketStatuses, "Id", "Name");

            return View();
        }

        public ActionResult ExportToCSV()
        {
            var data = db.InboundCalls.ToList(); // Adapt as necessary to your data
            var output = new StringWriter();
            var csv = new CsvHelper.CsvWriter(output, System.Globalization.CultureInfo.InvariantCulture);

            csv.WriteRecords(data);
            return File(new UTF8Encoding().GetBytes(output.ToString()), "text/csv", "InboundCalls.csv");
        }

        // POST: Inbound/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InboundCall inboundCall)
        {
            if (ModelState.IsValid)
            {
                db.InboundCalls.Add(inboundCall);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Repopulate the ViewBag properties in case of an invalid model state
            ViewBag.CallObjectives = new SelectList(db.CallObjectives, "Id", "Name");
            ViewBag.Regions = new SelectList(db.Regions, "Id", "Name");
            ViewBag.Branches = new SelectList(db.Branches, "Id", "Name");
            ViewBag.Origins = new SelectList(db.Origins, "Id", "Name");
            ViewBag.Products = new SelectList(db.Products, "Id", "Name");
            ViewBag.TicketTypes = new SelectList(db.TicketTypes, "Id", "Name");
            ViewBag.TicketStatuses = new SelectList(db.TicketStatuses, "Id", "Name");

            return View(inboundCall);
        }

        // GET: Inbound/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            InboundCall inboundCall = db.InboundCalls.Find(id);
            if (inboundCall == null)
            {
                return HttpNotFound();
            }

            return View(inboundCall);
        }

        // POST: Inbound/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InboundCall inboundCall)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inboundCall).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inboundCall);
        }

        // GET: Inbound/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            InboundCall inboundCall = db.InboundCalls.Find(id);
            if (inboundCall == null)
            {
                return HttpNotFound();
            }

            return View(inboundCall);
        }

        // GET: Inbound/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            InboundCall inboundCall = db.InboundCalls.Find(id);
            if (inboundCall == null)
            {
                return HttpNotFound();
            }

            return View(inboundCall);
        }

        // POST: Inbound/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InboundCall inboundCall = db.InboundCalls.Find(id);
            db.InboundCalls.Remove(inboundCall);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
