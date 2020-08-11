using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GiteHouse;
using GiteHouse.Core;
using GiteHouse.Models;
using GiteHouse.PersistenceInDBSqlServer;

namespace GiteHouse.Controllers
{
    public class HebergementsController : Controller
    {
        private GiteHouseEntities db = new GiteHouseEntities();
        private IUnitOfWork _unitOfWork;

        public HebergementsController()
        {
            _unitOfWork = new UnitOfWork(db);
        }

        // GET: Hebergements
        public ActionResult Index()
        {
            var hebergements = db.Hebergements.Include(h => h.Adresse).Include(h => h.Utilisateur).Include(h => h.TypeHebergement);
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
            ViewBag.idHebergement = id;
            ViewBag.photosListe = hebergement.Photos.ToList();

            if (hebergement == null)
            {
                return HttpNotFound();
            }
            return View(hebergement);
        }

     
        [HttpGet]
        public ActionResult GetRegions(string iso3)
        {
            if (!string.IsNullOrWhiteSpace(iso3) && iso3.Length == 3)
            {
                //var repo = new RegionsRepository();
                var regions = _unitOfWork.Regions.GetAll();
                //IEnumerable<SelectListItem> regions = repo.GetRegions(iso3);
                return Json(regions, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        // GET: Hebergements/Create
        public ActionResult Create()
        {
         
            ViewBag.IdTypeHebergement = new SelectList(db.TypeHebergements, "IdTypeHebergement", "Nom");

            return View();
        }

        // POST: Hebergements/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTypeHebergement,Nom,DescriptionCourte,DescriptionLongue,Surface,NombreLits,NombreChambre,Animaux,Fumeur,PrixBase")] Hebergement hebergement)
        {
            
            if (ModelState.IsValid)
            {
                SessionUser sessionUser = (SessionUser)Session["Utilisateur"];
                hebergement.IdUtilisateur = sessionUser.User.IdUtilisateur;
                sessionUser.Hebergement = hebergement;

                Session["Utilisateur"] = sessionUser;

                return RedirectToAction("Create", "Adresses");
            }
            else
            {
                ViewBag.IdTypeHebergement = new SelectList(db.TypeHebergements, "IdTypeHebergement", "Nom", hebergement.IdTypeHebergement);
                return View();
            }
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
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateurs, "IdUtilisateur", "Nom", hebergement.IdUtilisateur);
            ViewBag.IdTypeHebergement = new SelectList(db.TypeHebergements, "IdTypeHebergement", "Nom", hebergement.IdTypeHebergement);
            return View(hebergement);
        }

        // POST: Hebergements/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdHebergement,IdTypeHebergement,IdUtilisateur,IdAdresse,IdVille,Nom,DescriptionCourte,DescriptionLongue,Surface,NombreLits,NombreChambre,Animaux,Fumeur,PrixBase,Statut")] Hebergement hebergement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hebergement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAdresse = new SelectList(db.Adresses, "IdAdresse", "Nom", hebergement.IdAdresse);
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateurs, "IdUtilisateur", "Nom", hebergement.IdUtilisateur);
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
