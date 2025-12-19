using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CallCenter.Models;

namespace CallCenter.Controllers
{
    public class AllianceBranchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AllianceBranch
        public ActionResult Index()
        {
            return View(db.AllianceBranches.ToList());
        }

        // GET: AllianceBranch/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllianceBranch allianceBranch = db.AllianceBranches.Find(id);
            if (allianceBranch == null)
            {
                return HttpNotFound();
            }
            return View(allianceBranch);
        }

        // GET: AllianceBranch/Create
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
        public ActionResult Create([Bind(Include = "BranchCode,BranchName,BranchType,ParentBranch,BranchEmailID,BranchAddress,HeadOffice,RegionalOffice")] AllianceBranch allianceBranch)
        {
            if (ModelState.IsValid)
            {
                db.AllianceBranches.Add(allianceBranch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HeadOffices = new SelectList(AllianceBranch.HeadOffices);
            ViewBag.RegionalOffices = new SelectList(AllianceBranch.RegionalOffices);
            ViewBag.ParentBranches = new SelectList(db.AllianceBranches, "BranchCode", "BranchName", allianceBranch.ParentBranch);
            return View(allianceBranch);
        }

        // GET: AllianceBranch/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllianceBranch allianceBranch = db.AllianceBranches.Find(id);
            if (allianceBranch == null)
            {
                return HttpNotFound();
            }
            ViewBag.HeadOffices = new SelectList(AllianceBranch.HeadOffices);
            ViewBag.RegionalOffices = new SelectList(AllianceBranch.RegionalOffices);
            ViewBag.ParentBranches = new SelectList(db.AllianceBranches, "BranchCode", "BranchName", allianceBranch.ParentBranch);
            return View(allianceBranch);
        }

        // POST: AllianceBranch/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BranchCode,BranchName,BranchType,ParentBranch,BranchEmailID,BranchAddress,HeadOffice,RegionalOffice")] AllianceBranch allianceBranch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allianceBranch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HeadOffices = new SelectList(AllianceBranch.HeadOffices);
            ViewBag.RegionalOffices = new SelectList(AllianceBranch.RegionalOffices);
            ViewBag.ParentBranches = new SelectList(db.AllianceBranches, "BranchCode", "BranchName", allianceBranch.ParentBranch);
            return View(allianceBranch);
        }

        // GET: AllianceBranch/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllianceBranch allianceBranch = db.AllianceBranches.Find(id);
            if (allianceBranch == null)
            {
                return HttpNotFound();
            }
            return View(allianceBranch);
        }

        // POST: AllianceBranch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AllianceBranch allianceBranch = db.AllianceBranches.Find(id);
            db.AllianceBranches.Remove(allianceBranch);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ToggleActive(string id)
        {

            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var alliance = dbContext.AllianceBranches.FirstOrDefault(x => x.BranchCode == id);

                if (alliance == null)
                    return HttpNotFound();

                alliance.IsActive = !alliance.IsActive; // toggle

                dbContext.SaveChanges();
            }



            return RedirectToAction("Index"); // reload grid
        }
    }
}
