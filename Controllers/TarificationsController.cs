using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GiteHouse;

namespace GiteHouse.Controllers
{
    public class TarificationsController : Controller
    {
        private GiteHouseEntities db = new GiteHouseEntities();

        // GET: Tarifications
        public ActionResult Index()
        {
            var tarifications = db.Tarifications.Include(t => t.Hebergement).Include(t => t.Saison);
            return View(tarifications.ToList());
        }

        // GET: Tarifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarification tarification = db.Tarifications.Find(id);
            if (tarification == null)
            {
                return HttpNotFound();
            }
            return View(tarification);
        }

        // GET: Tarifications/Create
        public ActionResult Create()
        {
            ViewBag.IdHebergement = new SelectList(db.Hebergements, "IdHebergement", "Nom");
            ViewBag.IdSaison = new SelectList(db.Saisons, "IdSaison", "Nom");
            return View();
        }

        // POST: Tarifications/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTarification,IdHebergement,IdSaison,PrixNuit")] Tarification tarification)
        {
            if (ModelState.IsValid)
            {
                db.Tarifications.Add(tarification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdHebergement = new SelectList(db.Hebergements, "IdHebergement", "Nom", tarification.IdHebergement);
            ViewBag.IdSaison = new SelectList(db.Saisons, "IdSaison", "Nom", tarification.IdSaison);
            return View(tarification);
        }

        // GET: Tarifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarification tarification = db.Tarifications.Find(id);
            if (tarification == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdHebergement = new SelectList(db.Hebergements, "IdHebergement", "Nom", tarification.IdHebergement);
            ViewBag.IdSaison = new SelectList(db.Saisons, "IdSaison", "Nom", tarification.IdSaison);
            return View(tarification);
        }

        // POST: Tarifications/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTarification,IdHebergement,IdSaison,PrixNuit")] Tarification tarification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tarification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdHebergement = new SelectList(db.Hebergements, "IdHebergement", "Nom", tarification.IdHebergement);
            ViewBag.IdSaison = new SelectList(db.Saisons, "IdSaison", "Nom", tarification.IdSaison);
            return View(tarification);
        }

        // GET: Tarifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarification tarification = db.Tarifications.Find(id);
            if (tarification == null)
            {
                return HttpNotFound();
            }
            return View(tarification);
        }

        // POST: Tarifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tarification tarification = db.Tarifications.Find(id);
            db.Tarifications.Remove(tarification);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
