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
    public class AvisController : Controller
    {
        private GiteHouseEntities db = new GiteHouseEntities();

        // GET: Avis
        public ActionResult Index()
        {
            SessionUser sessionUser = (SessionUser)Session["Utilisateur"];
            
            var avis = db.Avis.Include(a => a.Utilisateur).Include(a => a.Hebergements).Where(a => a.IdUtilisateur == sessionUser.User.IdUtilisateur).ToList();
            return View(avis.ToList());
        }

        // GET: Avis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avi avi = db.Avis.Find(id);
            if (avi == null)
            {
                return HttpNotFound();
            }
            return View(avi);
        }

        // GET: Avis/Create
        public ActionResult Create()
        {
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateurs, "IdUtilisateur", "Nom");
            return View();
        }

        // POST: Avis/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAvi,IdUtilisateur,IdHebergement,Note,Commentaire,Date,Reponse")] Avi avi)
        {
            if (ModelState.IsValid)
            {
                db.Avis.Add(avi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUtilisateur = new SelectList(db.Utilisateurs, "IdUtilisateur", "Nom", avi.IdUtilisateur);
            return View(avi);
        }

        // GET: Avis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avi avi = db.Avis.Find(id);
            if (avi == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateurs, "IdUtilisateur", "Nom", avi.IdUtilisateur);
            return View(avi);
        }

        // POST: Avis/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAvi,IdUtilisateur,IdHebergement,Note,Commentaire,Date,Reponse")] Avi avi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(avi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateurs, "IdUtilisateur", "Nom", avi.IdUtilisateur);
            return View(avi);
        }

        // GET: Avis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avi avi = db.Avis.Find(id);
            if (avi == null)
            {
                return HttpNotFound();
            }
            return View(avi);
        }

        // POST: Avis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Avi avi = db.Avis.Find(id);
            db.Avis.Remove(avi);
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
