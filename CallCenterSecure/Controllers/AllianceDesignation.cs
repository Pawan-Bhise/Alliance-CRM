using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CallCenter.Models;

namespace CallCenter.Controllers
{
    public class AllianceDesignationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AllianceDesignation
        public ActionResult Index()
        {
            return View(db.AllianceDesignations.ToList());
        }

        // GET: AllianceDesignation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllianceDesignation allianceDesignation = db.AllianceDesignations.Find(id);
            if (allianceDesignation == null)
            {
                return HttpNotFound();
            }
            return View(allianceDesignation);
        }

        // GET: AllianceDesignation/Create
        public ActionResult Create()
        {
            ViewBag.Branches = new SelectList(db.AllianceBranches, "BranchName", "BranchName");
            return View();
        }

        // POST: AllianceDesignation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeName,Designation,Branch,EmailAddress")] AllianceDesignation allianceDesignation)
        {
            if (ModelState.IsValid)
            {
                ViewBag.DuplicateRecord = "";
               var allianceDesignationList = db.AllianceDesignations;

                var data = allianceDesignationList.Where(e => e.Designation == allianceDesignation.Designation && e.Branch == allianceDesignation.Branch && e.EmailAddress == allianceDesignation.EmailAddress).FirstOrDefault();

                if (data == null)
                {
                    db.AllianceDesignations.Add(allianceDesignation);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.DuplicateRecord = "Can not create duplicate Record!";
                    ViewBag.Branches = new SelectList(db.AllianceBranches, "BranchName", "BranchName", allianceDesignation.Branch);
                    return View(allianceDesignation);
                }
               
            }

            ViewBag.Branches = new SelectList(db.AllianceBranches, "BranchName", "BranchName", allianceDesignation.Branch);
            return View(allianceDesignation);
        }

        // GET: AllianceDesignation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllianceDesignation allianceDesignation = db.AllianceDesignations.Find(id);
            if (allianceDesignation == null)
            {
                return HttpNotFound();
            }
            ViewBag.Branches = new SelectList(db.AllianceBranches, "BranchName", "BranchName", allianceDesignation.Branch);
            return View(allianceDesignation);
        }

        // POST: AllianceDesignation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DesignationID,EmployeeName,Designation,Branch,EmailAddress")] AllianceDesignation allianceDesignation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allianceDesignation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Branches = new SelectList(db.AllianceBranches, "BranchName", "BranchName", allianceDesignation.Branch);
            return View(allianceDesignation);
        }

        // GET: AllianceDesignation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllianceDesignation allianceDesignation = db.AllianceDesignations.Find(id);
            if (allianceDesignation == null)
            {
                return HttpNotFound();
            }
            return View(allianceDesignation);
        }

        // POST: AllianceDesignation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AllianceDesignation allianceDesignation = db.AllianceDesignations.Find(id);
            db.AllianceDesignations.Remove(allianceDesignation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ToggleActive(int id)
        {

            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var allianceDesignation = dbContext.AllianceDesignations.FirstOrDefault(x => x.DesignationID == id);

                if (allianceDesignation == null)
                    return HttpNotFound();

                allianceDesignation.IsActive = !allianceDesignation.IsActive; // toggle

                dbContext.SaveChanges();
            }



            return RedirectToAction("Index"); // reload grid
        }
    }
}
