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
    public class SaisonsController : Controller
    {
        private GiteHouseEntities db = new GiteHouseEntities();

        // GET: Saisons
        public ActionResult Index()
        {
            var saisons = db.Saisons.Include(s => s.Utilisateur);
            return View(saisons.ToList());
        }

        // GET: Saisons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Saison saison = db.Saisons.Find(id);
            if (saison == null)
            {
                return HttpNotFound();
            }
            return View(saison);
        }

        // GET: Saisons/Create
        public ActionResult Create()
        {
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateurs, "IdUtilisateur", "Nom");
            return View();
        }

        // POST: Saisons/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSaison,IdUtilisateur,DateDebut,DateFin,Nom,PrixNuit")] Saison saison)
        {
            if (ModelState.IsValid)
            {
                db.Saisons.Add(saison);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUtilisateur = new SelectList(db.Utilisateurs, "IdUtilisateur", "Nom", saison.IdUtilisateur);
            return View(saison);
        }

        // GET: Saisons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Saison saison = db.Saisons.Find(id);
            if (saison == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateurs, "IdUtilisateur", "Nom", saison.IdUtilisateur);
            return View(saison);
        }

        // POST: Saisons/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSaison,IdUtilisateur,DateDebut,DateFin,Nom,PrixNuit")] Saison saison)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saison).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateurs, "IdUtilisateur", "Nom", saison.IdUtilisateur);
            return View(saison);
        }

        // GET: Saisons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Saison saison = db.Saisons.Find(id);
            if (saison == null)
            {
                return HttpNotFound();
            }
            return View(saison);
        }

        // POST: Saisons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Saison saison = db.Saisons.Find(id);
            db.Saisons.Remove(saison);
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
