using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CallCenter.Models;

namespace CallCenter.Controllers
{
    public class MIMULocationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MIMULocation
        public ActionResult Index()
        {
            return View(db.MIMULocations.ToList());
        }

        // GET: MIMULocation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MIMULocation mIMULocation = db.MIMULocations.Find(id);
            if (mIMULocation == null)
            {
                return HttpNotFound();
            }
            return View(mIMULocation);
        }

        // GET: MIMULocation/Create
        public ActionResult Create()
        {
            ViewBag.Countries = new SelectList(db.Countries, "CountryCode", "CountryName");
            return View();
        }

        // POST: MIMULocation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CountryCode,CountryName,StateCode,StateName,DistrictCode,DistrictName,CityCode,CityName,VillageTractCode,VillageTractName,VillageCode,VillageName,AreaCode,AreaName")] MIMULocation mIMULocation)
        {
            if (ModelState.IsValid)
            {
                db.MIMULocations.Add(mIMULocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Countries = new SelectList(db.Countries, "CountryCode", "CountryName", mIMULocation.CountryCode);
            return View(mIMULocation);
        }

        // GET: MIMULocation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MIMULocation mIMULocation = db.MIMULocations.Find(id);
            if (mIMULocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.Countries = new SelectList(db.Countries, "CountryCode", "CountryName", mIMULocation.CountryCode);
            return View(mIMULocation);
        }

        // POST: MIMULocation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CountryCode,CountryName,StateCode,StateName,DistrictCode,DistrictName,CityCode,CityName,VillageTractCode,VillageTractName,VillageCode,VillageName,AreaCode,AreaName")] MIMULocation mIMULocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mIMULocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Countries = new SelectList(db.Countries, "CountryCode", "CountryName", mIMULocation.CountryCode);
            return View(mIMULocation);
        }

        // GET: MIMULocation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MIMULocation mIMULocation = db.MIMULocations.Find(id);
            if (mIMULocation == null)
            {
                return HttpNotFound();
            }
            return View(mIMULocation);
        }

        // POST: MIMULocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MIMULocation mIMULocation = db.MIMULocations.Find(id);
            db.MIMULocations.Remove(mIMULocation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // AJAX Methods for dependent dropdowns
        public JsonResult GetStates(string countryCode)
        {
            var states = db.States.Where(s => s.CountryCode == countryCode).Select(s => new { s.StateCode, s.StateName }).ToList();
            return Json(states, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDistricts(string stateCode)
        {
            var districts = db.Districts.Where(d => d.StateCode == stateCode).Select(d => new { d.DistrictCode, d.DistrictName }).ToList();
            return Json(districts, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCities(string districtCode)
        {
            var cities = db.Cities.Where(c => c.DistrictCode == districtCode).Select(c => new { c.CityCode, c.CityName }).ToList();
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVillageTracts(string cityCode)
        {
            var villageTracts = db.VillageTracts.Where(v => v.CityCode == cityCode).Select(v => new { v.VillageTractCode, v.VillageTractName }).ToList();
            return Json(villageTracts, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVillages(string villageTractCode)
        {
            var villages = db.Villages.Where(v => v.VillageTractCode == villageTractCode).Select(v => new { v.VillageCode, v.VillageName }).ToList();
            return Json(villages, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAreas(string villageCode)
        {
            var areas = db.Areas.Where(a => a.VillageCode == villageCode).Select(a => new { a.AreaCode, a.AreaName }).ToList();
            return Json(areas, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ToggleActive(int id)
        {

            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var mimu = dbContext.MIMULocations.FirstOrDefault(x => x.Id== id);

                if (mimu == null)
                    return HttpNotFound();

                mimu.IsActive = !mimu.IsActive; // toggle

                dbContext.SaveChanges();
            }



            return RedirectToAction("Index"); // reload grid
        }
    }
}
