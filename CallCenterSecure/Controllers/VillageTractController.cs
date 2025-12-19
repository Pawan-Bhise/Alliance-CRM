using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CallCenter.Models;

namespace CallCenter.Controllers
{
    public class VillageTractController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VillageTract
        public ActionResult Index()
        {
            var villageTracts = db.VillageTracts.Include(v => v.City);
            return View(villageTracts.ToList());
        }

        // GET: VillageTract/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VillageTract villageTract = db.VillageTracts.Find(id);
            if (villageTract == null)
            {
                return HttpNotFound();
            }
            return View(villageTract);
        }

        // GET: VillageTract/Create
        public ActionResult Create()
        {
            ViewBag.CityCode = new SelectList(db.Cities, "CityCode", "CityName");
            return View();
        }

        // POST: VillageTract/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VillageTractCode,VillageTractName,CityCode")] VillageTract villageTract)
        {
            if (ModelState.IsValid)
            {
                db.VillageTracts.Add(villageTract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityCode = new SelectList(db.Cities, "CityCode", "CityName", villageTract.CityCode);
            return View(villageTract);
        }

        // GET: VillageTract/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VillageTract villageTract = db.VillageTracts.Find(id);
            if (villageTract == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityCode = new SelectList(db.Cities, "CityCode", "CityName", villageTract.CityCode);
            return View(villageTract);
        }

        // POST: VillageTract/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VillageTractCode,VillageTractName,CityCode")] VillageTract villageTract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(villageTract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityCode = new SelectList(db.Cities, "CityCode", "CityName", villageTract.CityCode);
            return View(villageTract);
        }

        // GET: VillageTract/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VillageTract villageTract = db.VillageTracts.Find(id);
            if (villageTract == null)
            {
                return HttpNotFound();
            }
            return View(villageTract);
        }

        // POST: VillageTract/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            VillageTract villageTract = db.VillageTracts.Find(id);
            db.VillageTracts.Remove(villageTract);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ToggleActive(string id)
        {

            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var village = dbContext.VillageTracts.FirstOrDefault(x => x.VillageTractCode == id);

                if (village == null)
                    return HttpNotFound();

                village.IsActive = !village.IsActive; // toggle

                dbContext.SaveChanges();
            }

            return RedirectToAction("Index"); // reload grid
        }
    }
}
