using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GiteHouse;
using GiteHouse.Models;

namespace GiteHouse.Controllers
{
    public class AdressesController : Controller
    {
        private GiteHouseEntities db = new GiteHouseEntities();

        // GET: Adresses
        public ActionResult Index()
        {
            var adresses = db.Adresses.Include(a => a.Departement).Include(a => a.Region).Include(a => a.Ville);
            return View(adresses.ToList());
        }

        // GET: Adresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adresse adresse = db.Adresses.Find(id);
            if (adresse == null)
            {
                return HttpNotFound();
            }
            return View(adresse);
        }

        // GET: Adresses/Create
        public ActionResult Create()
        {
            ViewBag.IdDepartement = new SelectList(db.Departements, "IdDepartement", "Nom");
            ViewBag.IdRegion = new SelectList(db.Regions, "IdRegion", "Nom");
            ViewBag.IdVille = new SelectList(db.Villes, "IdVille", "Nom");
            return View();
        }

        // POST: Adresses/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAdresse,Nom,Libelle,IdVille,IdRegion,IdDepartement")] Adresse adresse)
        {
            if (ModelState.IsValid)
            {
                SessionUser sessionUser = (SessionUser)Session["Utilisateur"];
                sessionUser.Hebergement.Adresse = adresse;

                db.Hebergements.Add(sessionUser.Hebergement);
                db.SaveChanges();

                int id = sessionUser.Hebergement.IdHebergement;
                Session["Utilisateur"] = sessionUser;
                return RedirectToAction("Create", "Photos");
            }

            ViewBag.IdDepartement = new SelectList(db.Departements, "IdDepartement", "Nom", adresse.IdDepartement);
            ViewBag.IdRegion = new SelectList(db.Regions, "IdRegion", "Nom", adresse.IdRegion);
            ViewBag.IdVille = new SelectList(db.Villes, "IdVille", "IdDepartement", adresse.IdVille);
            return View();
        }

        // GET: Adresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adresse adresse = db.Adresses.Find(id);
            if (adresse == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDepartement = new SelectList(db.Departements, "IdDepartement", "Nom", adresse.IdDepartement);
            ViewBag.IdRegion = new SelectList(db.Regions, "IdRegion", "Nom", adresse.IdRegion);
            ViewBag.IdVille = new SelectList(db.Villes, "IdVille", "IdDepartement", adresse.IdVille);
            return View(adresse);
        }

        // POST: Adresses/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAdresse,Nom,Libelle,IdVille,IdRegion,IdDepartement")] Adresse adresse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adresse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDepartement = new SelectList(db.Departements, "IdDepartement", "Nom", adresse.IdDepartement);
            ViewBag.IdRegion = new SelectList(db.Regions, "IdRegion", "Nom", adresse.IdRegion);
            ViewBag.IdVille = new SelectList(db.Villes, "IdVille", "IdDepartement", adresse.IdVille);
            return View(adresse);
        }

        // GET: Adresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adresse adresse = db.Adresses.Find(id);
            if (adresse == null)
            {
                return HttpNotFound();
            }
            return View(adresse);
        }

        // POST: Adresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Adresse adresse = db.Adresses.Find(id);
            db.Adresses.Remove(adresse);
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
