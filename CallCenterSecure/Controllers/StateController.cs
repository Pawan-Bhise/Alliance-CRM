using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CallCenter.Models;

namespace CallCenter.Controllers
{
    public class StateController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: State
        public ActionResult Index()
        {
            var states = db.States.Include(s => s.Country);
            return View(states.ToList());
        }

        // GET: State/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            State state = db.States.Find(id);
            if (state == null)
            {
                return HttpNotFound();
            }
            return View(state);
        }

        // GET: State/Create
        public ActionResult Create()
        {
            ViewBag.CountryCode = new SelectList(db.Countries, "CountryCode", "CountryName");
            return View();
        }

        // POST: State/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StateCode,StateName,CountryCode")] State state)
        {
            if (ModelState.IsValid)
            {
                db.States.Add(state);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryCode = new SelectList(db.Countries, "CountryCode", "CountryName", state.CountryCode);
            return View(state);
        }

        // GET: State/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            State state = db.States.Find(id);
            if (state == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryCode = new SelectList(db.Countries, "CountryCode", "CountryName", state.CountryCode);
            return View(state);
        }

        // POST: State/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StateCode,StateName,CountryCode")] State state)
        {
            if (ModelState.IsValid)
            {
                db.Entry(state).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryCode = new SelectList(db.Countries, "CountryCode", "CountryName", state.CountryCode);
            return View(state);
        }

        // GET: State/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            State state = db.States.Find(id);
            if (state == null)
            {
                return HttpNotFound();
            }
            return View(state);
        }

        // POST: State/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            State state = db.States.Find(id);
            db.States.Remove(state);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ToggleActive(string id)
        {

            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var state = dbContext.States.FirstOrDefault(x => x.StateCode == id);

                if (state == null)
                    return HttpNotFound();

                state.IsActive = !state.IsActive; // toggle

                dbContext.SaveChanges();
            }



            return RedirectToAction("Index"); // reload grid
        }
    }
}
