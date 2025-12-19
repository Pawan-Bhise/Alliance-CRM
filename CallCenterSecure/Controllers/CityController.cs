using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CallCenter.Models;

namespace CallCenter.Controllers
{
    public class CityController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: City
        public ActionResult Index()
        {
            var cities = db.Cities.Include(c => c.District);
            return View(cities.ToList());
        }

        // GET: City/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // GET: City/Create
        public ActionResult Create()
        {
            ViewBag.DistrictCode = new SelectList(db.Districts, "DistrictCode", "DistrictName");
            return View();
        }

        // POST: City/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CityCode,CityName,DistrictCode")] City city)
        {
            if (ModelState.IsValid)
            {
                db.Cities.Add(city);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DistrictCode = new SelectList(db.Districts, "DistrictCode", "DistrictName", city.DistrictCode);
            return View(city);
        }

        // GET: City/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            ViewBag.DistrictCode = new SelectList(db.Districts, "DistrictCode", "DistrictName", city.DistrictCode);
            return View(city);
        }

        // POST: City/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CityCode,CityName,DistrictCode")] City city)
        {
            if (ModelState.IsValid)
            {
                db.Entry(city).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DistrictCode = new SelectList(db.Districts, "DistrictCode", "DistrictName", city.DistrictCode);
            return View(city);
        }

        // GET: City/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            City city = db.Cities.Find(id);
            db.Cities.Remove(city);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ToggleActive(string id)
        {

            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var city = dbContext.Cities.FirstOrDefault(x => x.CityCode == id);

                if (city == null)
                    return HttpNotFound();

                city.IsActive = !city.IsActive; // toggle

                dbContext.SaveChanges();
            }



            return RedirectToAction("Index"); // reload grid
        }
    }
}
