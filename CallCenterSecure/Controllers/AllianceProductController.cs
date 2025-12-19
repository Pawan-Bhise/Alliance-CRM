using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CallCenter.Models;

namespace CallCenter.Controllers
{
    public class AllianceProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AllianceProduct
        public ActionResult Index()
        {
            return View(db.AllianceProducts.ToList());
        }

        // GET: AllianceProduct/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllianceProduct allianceProduct = db.AllianceProducts.Find(id);
            if (allianceProduct == null)
            {
                return HttpNotFound();
            }
            return View(allianceProduct);
        }

        // GET: AllianceProduct/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AllianceProduct/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductCode,ProductName,ProductGroup")] AllianceProduct allianceProduct)
        {
            if (ModelState.IsValid)
            {
                db.AllianceProducts.Add(allianceProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(allianceProduct);
        }

        // GET: AllianceProduct/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllianceProduct allianceProduct = db.AllianceProducts.Find(id);
            if (allianceProduct == null)
            {
                return HttpNotFound();
            }
            return View(allianceProduct);
        }

        // POST: AllianceProduct/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductCode,ProductName,ProductGroup")] AllianceProduct allianceProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allianceProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(allianceProduct);
        }

        // GET: AllianceProduct/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllianceProduct allianceProduct = db.AllianceProducts.Find(id);
            if (allianceProduct == null)
            {
                return HttpNotFound();
            }
            return View(allianceProduct);
        }

        // POST: AllianceProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AllianceProduct allianceProduct = db.AllianceProducts.Find(id);
            db.AllianceProducts.Remove(allianceProduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
