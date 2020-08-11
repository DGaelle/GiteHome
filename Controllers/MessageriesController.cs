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
    public class MessageriesController : Controller
    {
        private GiteHouseEntities db = new GiteHouseEntities();

        // GET: Messageries
        public ActionResult Index()
        {
            var messageries = db.Messageries.Include(m => m.Hebergement).Include(m => m.Utilisateur);
            return View(messageries.ToList());
        }

        // GET: Messageries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Messagerie messagerie = db.Messageries.Find(id);
            if (messagerie == null)
            {
                return HttpNotFound();
            }
            return View(messagerie);
        }

        // GET: Messageries/Create
        public ActionResult Create()
        {
            ViewBag.IdHebergement = new SelectList(db.Hebergements, "IdHebergement", "Nom");
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateurs, "IdUtilisateur", "Nom");
            return View();
        }

        // POST: Messageries/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMessagerie,IdUtilisateur,IdHebergement,Email,Titre,Texte")] Messagerie messagerie)
        {
            if (ModelState.IsValid)
            {
                db.Messageries.Add(messagerie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdHebergement = new SelectList(db.Hebergements, "IdHebergement", "Nom", messagerie.IdHebergement);
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateurs, "IdUtilisateur", "Nom", messagerie.IdUtilisateur);
            return View(messagerie);
        }

        // GET: Messageries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Messagerie messagerie = db.Messageries.Find(id);
            if (messagerie == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdHebergement = new SelectList(db.Hebergements, "IdHebergement", "Nom", messagerie.IdHebergement);
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateurs, "IdUtilisateur", "Nom", messagerie.IdUtilisateur);
            return View(messagerie);
        }

        // POST: Messageries/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMessagerie,IdUtilisateur,IdHebergement,Email,Titre,Texte")] Messagerie messagerie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(messagerie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdHebergement = new SelectList(db.Hebergements, "IdHebergement", "Nom", messagerie.IdHebergement);
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateurs, "IdUtilisateur", "Nom", messagerie.IdUtilisateur);
            return View(messagerie);
        }

        // GET: Messageries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Messagerie messagerie = db.Messageries.Find(id);
            if (messagerie == null)
            {
                return HttpNotFound();
            }
            return View(messagerie);
        }

        // POST: Messageries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Messagerie messagerie = db.Messageries.Find(id);
            db.Messageries.Remove(messagerie);
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
