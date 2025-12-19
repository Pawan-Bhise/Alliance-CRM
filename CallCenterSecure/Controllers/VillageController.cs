using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CallCenter.Models;

namespace CallCenter.Controllers
{
    public class VillageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Village
        public ActionResult Index()
        {
            var villages = db.Villages.Include(v => v.VillageTract);
            return View(villages.ToList());
        }

        // GET: Village/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Village village = db.Villages.Find(id);
            if (village == null)
            {
                return HttpNotFound();
            }
            return View(village);
        }

        // GET: Village/Create
        public ActionResult Create()
        {
            ViewBag.VillageTractCode = new SelectList(db.VillageTracts, "VillageTractCode", "VillageTractName");
            return View();
        }

        // POST: Village/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VillageCode,VillageName,VillageTractCode")] Village village)
        {
            if (ModelState.IsValid)
            {
                db.Villages.Add(village);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VillageTractCode = new SelectList(db.VillageTracts, "VillageTractCode", "VillageTractName", village.VillageTractCode);
            return View(village);
        }

        // GET: Village/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Village village = db.Villages.Find(id);
            if (village == null)
            {
                return HttpNotFound();
            }
            ViewBag.VillageTractCode = new SelectList(db.VillageTracts, "VillageTractCode", "VillageTractName", village.VillageTractCode);
            return View(village);
        }

        // POST: Village/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VillageCode,VillageName,VillageTractCode")] Village village)
        {
            if (ModelState.IsValid)
            {
                db.Entry(village).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VillageTractCode = new SelectList(db.VillageTracts, "VillageTractCode", "VillageTractName", village.VillageTractCode);
            return View(village);
        }

        // GET: Village/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Village village = db.Villages.Find(id);
            if (village == null)
            {
                return HttpNotFound();
            }
            return View(village);
        }

        // POST: Village/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Village village = db.Villages.Find(id);
            db.Villages.Remove(village);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ToggleActive(string id)
        {

            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var village = dbContext.Villages.FirstOrDefault(x => x.VillageCode == id);

                if (village == null)
                    return HttpNotFound();

                village.IsActive = !village.IsActive; // toggle

                dbContext.SaveChanges();
            }



            return RedirectToAction("Index"); // reload grid
        }
    }
}
