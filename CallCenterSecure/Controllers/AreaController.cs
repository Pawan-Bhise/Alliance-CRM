using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CallCenter.Models;

namespace CallCenter.Controllers
{
    public class AreaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Area
        public ActionResult Index()
        {
            var areas = db.Areas.Include(a => a.Village);
            return View(areas.ToList());
        }

        // GET: Area/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area area = db.Areas.Find(id);
            if (area == null)
            {
                return HttpNotFound();
            }
            return View(area);
        }

        // GET: Area/Create
        public ActionResult Create()
        {
            ViewBag.VillageCode = new SelectList(db.Villages, "VillageCode", "VillageName");
            return View();
        }

        // POST: Area/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AreaCode,AreaName,VillageCode")] Area area)
        {
            if (ModelState.IsValid)
            {
                db.Areas.Add(area);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VillageCode = new SelectList(db.Villages, "VillageCode", "VillageName", area.VillageCode);
            return View(area);
        }

        // GET: Area/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area area = db.Areas.Find(id);
            if (area == null)
            {
                return HttpNotFound();
            }
            ViewBag.VillageCode = new SelectList(db.Villages, "VillageCode", "VillageName", area.VillageCode);
            return View(area);
        }

        // POST: Area/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AreaCode,AreaName,VillageCode")] Area area)
        {
            if (ModelState.IsValid)
            {
                db.Entry(area).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VillageCode = new SelectList(db.Villages, "VillageCode", "VillageName", area.VillageCode);
            return View(area);
        }

        // GET: Area/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area area = db.Areas.Find(id);
            if (area == null)
            {
                return HttpNotFound();
            }
            return View(area);
        }

        // POST: Area/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Area area = db.Areas.Find(id);
            db.Areas.Remove(area);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ToggleActive(string id)
        {

            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var area = dbContext.Areas.FirstOrDefault(x => x.AreaCode == id);

                if (area == null)
                    return HttpNotFound();

                area.IsActive = !area.IsActive; // toggle

                dbContext.SaveChanges();
            }



            return RedirectToAction("Index"); // reload grid
        }
    }
}
