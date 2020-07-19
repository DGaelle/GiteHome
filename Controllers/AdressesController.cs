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
    public class AdressesController : Controller
    {
        private GiteHouseEntities db = new GiteHouseEntities();

        // GET: Adresses
        public ActionResult Index()
        {
            return View(db.Adresses.ToList());
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
            return View();
        }

        // POST: Adresses/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAdresse,Nom,Adresse1,IdVille")] Adresse adresse)
        {
            if (ModelState.IsValid)
            {
                db.Adresses.Add(adresse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adresse);
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
            return View(adresse);
        }

        // POST: Adresses/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAdresse,Nom,Adresse1,IdVille")] Adresse adresse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adresse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
