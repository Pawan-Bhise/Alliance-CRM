using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CallCenter.Models;

namespace CallCenter.Controllers
{
    public class DistrictController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: District
        public ActionResult Index()
        {
            var districts = db.Districts.Include(d => d.State);
            return View(districts.ToList());
        }

        // GET: District/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            District district = db.Districts.Find(id);
            if (district == null)
            {
                return HttpNotFound();
            }
            return View(district);
        }

        // GET: District/Create
        public ActionResult Create()
        {
            ViewBag.StateCode = new SelectList(db.States, "StateCode", "StateName");
            return View();
        }

        // POST: District/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DistrictCode,DistrictName,StateCode")] District district)
        {
            if (ModelState.IsValid)
            {
                db.Districts.Add(district);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StateCode = new SelectList(db.States, "StateCode", "StateName", district.StateCode);
            return View(district);
        }

        // GET: District/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            District district = db.Districts.Find(id);
            if (district == null)
            {
                return HttpNotFound();
            }
            ViewBag.StateCode = new SelectList(db.States, "StateCode", "StateName", district.StateCode);
            return View(district);
        }

        // POST: District/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DistrictCode,DistrictName,StateCode")] District district)
        {
            if (ModelState.IsValid)
            {
                db.Entry(district).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateCode = new SelectList(db.States, "StateCode", "StateName", district.StateCode);
            return View(district);
        }

        // GET: District/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            District district = db.Districts.Find(id);
            if (district == null)
            {
                return HttpNotFound();
            }
            return View(district);
        }

        // POST: District/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            District district = db.Districts.Find(id);
            db.Districts.Remove(district);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ToggleActive(string id)
        {

            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var district = dbContext.Districts.FirstOrDefault(x => x.DistrictCode == id);

                if (district == null)
                    return HttpNotFound();

                district.IsActive = !district.IsActive; // toggle

                dbContext.SaveChanges();
            }



            return RedirectToAction("Index"); // reload grid
        }
    }
}
