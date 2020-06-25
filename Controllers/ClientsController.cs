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
    public class ClientsController : Controller
    {
        private GiteHouseEntities db = new GiteHouseEntities();

        // GET: Clients
        public ActionResult Index()
        {
            var clients = db.Clients.Include(c => c.Adresse).Include(c => c.Genre);
            return View(clients.ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            ViewBag.IdAdresse = new SelectList(db.Adresses, "IdAdresse", "Nom");
            ViewBag.IdGenre = new SelectList(db.Genres, "IdGenre", "Nom");
            return View();
        }

        // POST: Clients/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdClient,IdGenre,IdAdresse,Nom,Prenom,Login,Password,Telephone,Email,Age")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAdresse = new SelectList(db.Adresses, "IdAdresse", "Nom", client.IdAdresse);
            ViewBag.IdGenre = new SelectList(db.Genres, "IdGenre", "Nom", client.IdGenre);
            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAdresse = new SelectList(db.Adresses, "IdAdresse", "Nom", client.IdAdresse);
            ViewBag.IdGenre = new SelectList(db.Genres, "IdGenre", "Nom", client.IdGenre);
            return View(client);
        }

        // POST: Clients/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdClient,IdGenre,IdAdresse,Nom,Prenom,Login,Password,Telephone,Email,Age")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAdresse = new SelectList(db.Adresses, "IdAdresse", "Nom", client.IdAdresse);
            ViewBag.IdGenre = new SelectList(db.Genres, "IdGenre", "Nom", client.IdGenre);
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
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
