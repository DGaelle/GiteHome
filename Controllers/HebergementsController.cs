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
    public class HebergementsController : Controller
    {
        private GiteHouseEntities db = new GiteHouseEntities();

        // GET: Hebergements
        public ActionResult Index()
        {
            var hebergements = db.Hebergements.Include(h => h.Adresse).Include(h => h.Destination).Include(h => h.Label).Include(h => h.Proprietaire).Include(h => h.TypeHebergement);
            return View(hebergements.ToList());
        }

        // GET: Hebergements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hebergement hebergement = db.Hebergements.Find(id);
            if (hebergement == null)
            {
                return HttpNotFound();
            }
            return View(hebergement);
        }

        // GET: Hebergements/Create
        public ActionResult Create()
        {
            ViewBag.IdAdresse = new SelectList(db.Adresses, "IdAdresse", "Nom");
            ViewBag.IdDestination = new SelectList(db.Destinations, "IdDestination", "Nom");
            ViewBag.IdLabel = new SelectList(db.Labels, "IdLabel", "Nom");
            ViewBag.IdProprietaire = new SelectList(db.Proprietaires, "IdProprietaire", "Nom");
            ViewBag.IdTypeHebergement = new SelectList(db.TypeHebergements, "IdTypeHebergement", "Nom");
            return View();
        }

        // POST: Hebergements/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdHebergement,IdTypeHebergement,IdDestination,IdProprietaire,IdLabel,IdAdresse,Ordre,Nom,DescriptionCourte,DescriptionLongue,Surface,NombrePiece,NombreChambre,Animaux,CouleurReservation,PrixBase,Statut")] Hebergement hebergement)
        {
            if (ModelState.IsValid)
            {
                db.Hebergements.Add(hebergement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAdresse = new SelectList(db.Adresses, "IdAdresse", "Nom", hebergement.IdAdresse);
            ViewBag.IdDestination = new SelectList(db.Destinations, "IdDestination", "Nom", hebergement.IdDestination);
            ViewBag.IdLabel = new SelectList(db.Labels, "IdLabel", "Nom", hebergement.IdLabel);
            ViewBag.IdProprietaire = new SelectList(db.Proprietaires, "IdProprietaire", "Nom", hebergement.IdProprietaire);
            ViewBag.IdTypeHebergement = new SelectList(db.TypeHebergements, "IdTypeHebergement", "Nom", hebergement.IdTypeHebergement);
            return View(hebergement);
        }

        // GET: Hebergements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hebergement hebergement = db.Hebergements.Find(id);
            if (hebergement == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAdresse = new SelectList(db.Adresses, "IdAdresse", "Nom", hebergement.IdAdresse);
            ViewBag.IdDestination = new SelectList(db.Destinations, "IdDestination", "Nom", hebergement.IdDestination);
            ViewBag.IdLabel = new SelectList(db.Labels, "IdLabel", "Nom", hebergement.IdLabel);
            ViewBag.IdProprietaire = new SelectList(db.Proprietaires, "IdProprietaire", "Nom", hebergement.IdProprietaire);
            ViewBag.IdTypeHebergement = new SelectList(db.TypeHebergements, "IdTypeHebergement", "Nom", hebergement.IdTypeHebergement);
            return View(hebergement);
        }

        // POST: Hebergements/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdHebergement,IdTypeHebergement,IdDestination,IdProprietaire,IdLabel,IdAdresse,Ordre,Nom,DescriptionCourte,DescriptionLongue,Surface,NombrePiece,NombreChambre,Animaux,CouleurReservation,PrixBase,Statut")] Hebergement hebergement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hebergement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAdresse = new SelectList(db.Adresses, "IdAdresse", "Nom", hebergement.IdAdresse);
            ViewBag.IdDestination = new SelectList(db.Destinations, "IdDestination", "Nom", hebergement.IdDestination);
            ViewBag.IdLabel = new SelectList(db.Labels, "IdLabel", "Nom", hebergement.IdLabel);
            ViewBag.IdProprietaire = new SelectList(db.Proprietaires, "IdProprietaire", "Nom", hebergement.IdProprietaire);
            ViewBag.IdTypeHebergement = new SelectList(db.TypeHebergements, "IdTypeHebergement", "Nom", hebergement.IdTypeHebergement);
            return View(hebergement);
        }

        // GET: Hebergements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hebergement hebergement = db.Hebergements.Find(id);
            if (hebergement == null)
            {
                return HttpNotFound();
            }
            return View(hebergement);
        }

        // POST: Hebergements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hebergement hebergement = db.Hebergements.Find(id);
            db.Hebergements.Remove(hebergement);
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
