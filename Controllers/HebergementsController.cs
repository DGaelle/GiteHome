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
            if (hebergement == null)
            {
                return HttpNotFound();
            }
            return View(hebergement);
        }

        // GET: Hebergements/Create
        public ActionResult Create()
        {
            var regions = _unitOfWork.Regions.GetAll();

            //return View(regions);

            var hebergements = db.Hebergements.Include(h => h.Adresse).Include(h => h.Utilisateur).Include(h => h.IdRegion);
            ViewBag.IdAdresse = new SelectList(db.Adresses, "IdAdresse", "Nom");
            ViewBag.IdRegion = new SelectList(regions, "IdRegion", "Nom"); //new SelectList(db.Regions, "IdRegion", "Nom");
            ViewBag.IdDepartement = new SelectList(db.Departements, "IdDepartement", "Nom");
            ViewBag.IdVille = new SelectList(db.Villes, "IdVille", "Nom");
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateurs, "IdUtilisateur", "Nom");
            ViewBag.IdTypeHebergement = new SelectList(db.TypeHebergements, "IdTypeHebergement", "Nom");
            return View();
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

        // POST: Hebergements/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdHebergement,IdTypeHebergement,IdUtilisateur,IdAdresse,IdVille,Nom,DescriptionCourte,DescriptionLongue,Surface,NombreLits,NombreChambre,Animaux,Fumeur,PrixBase,Statut")] Hebergement hebergement)
        {
            ViewBag.IdRegion = new SelectList(db.Regions, "IdRegion", "Nom");
            ViewBag.IdDepartement = new SelectList(db.Departements, "IdDepartement", "Nom");
            ViewBag.IdVille = new SelectList(db.Villes, "IdVille", "Nom");


            return View();


            //if (ModelState.IsValid)
            //{
            //    db.Hebergements.Add(hebergement);
            //    db.SaveChanges();
            //    return RedirectToAction("CreateImage");
            //}

            //ViewBag.IdAdresse = new SelectList(db.Adresses, "IdAdresse", "Nom", hebergement.IdAdresse);
            //ViewBag.IdUtilisateur = new SelectList(db.Utilisateurs, "IdUtilisateur", "Nom", hebergement.IdUtilisateur);
            //ViewBag.IdTypeHebergement = new SelectList(db.TypeHebergements, "IdTypeHebergement", "Nom", hebergement.IdTypeHebergement);
            //return View("CreateImage");
        }

        // GET: Hebergements/Create
        public ActionResult CreateImage()
        {
            ViewBag.IdAdresse = new SelectList(db.Adresses, "IdAdresse", "Nom");
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateurs, "IdUtilisateur", "Nom");
            ViewBag.IdTypeHebergement = new SelectList(db.TypeHebergements, "IdTypeHebergement", "Nom");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateImage([Bind(Include = "IdPhoto, Nom, IdHebergement, IsDefault, NomAleatoire")] Hebergement hebergement)
        {
            if (ModelState.IsValid)
            {
                db.Hebergements.Add(hebergement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAdresse = new SelectList(db.Adresses, "IdAdresse", "Nom", hebergement.IdAdresse);
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateurs, "IdUtilisateur", "Nom", hebergement.IdUtilisateur);
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
