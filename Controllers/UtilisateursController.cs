﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GiteHouse;
using GiteHouse.dao;
using GiteHouse.Models;

namespace GiteHouse.Controllers
{
    public class UtilisateursController : Controller
    {
        private GiteHouseEntities db = new GiteHouseEntities();

     
        // GET: Utilisateurs
        public ActionResult Connexion()
        {
         
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Connexion([Bind(Include = "Login,Password")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {

                var user = db.Utilisateurs.FirstOrDefault(u => u.Login == utilisateur.Login
                 && u.Password == utilisateur.Password);

                if (user != null)
                {

                    SessionUser sessionUser= new SessionUser();
                    sessionUser.User = user;
                    Session["Utilisateur"] = sessionUser;

                    return RedirectToAction("Compte", "Utilisateurs");
                }
                else
                {
                    ViewBag.Erreur = "Erreur d'authentification";
                }
            }

            return View();
        }
        public ActionResult Deconnexion()
        {
            Session["Utilisateur"] = null;

            return RedirectToAction("Connexion", "Utilisateurs");

        }

        public ActionResult Compte()
        {
            SessionUser sessionUser = (SessionUser)Session["Utilisateur"];


            return View(sessionUser.User);
        }

        public ActionResult Paiements()
        {
            SessionUser sessionUser = (SessionUser)Session["Utilisateur"];

            decimal total = 0;
            ReservationDao reservationDao = new ReservationDao();
            List<Reservation> reservations = reservationDao.GetReservations(sessionUser.User.IdUtilisateur);

            foreach (Reservation item in reservations)
            {
                total += (decimal)item.Prix;
            }
            ViewBag.total = total;
            return View();
        }

        // GET: Utilisateurs/Details/5
        public ActionResult Details()
        {
        SessionUser sessionUser = (SessionUser)Session["Utilisateur"];

            Utilisateur utilisateur = db.Utilisateurs.Find(sessionUser.User.IdUtilisateur);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // GET: Utilisateurs/Create
        public ActionResult Create()
        {
            ViewBag.IdGenre = new SelectList(db.Genres, "IdGenre", "Nom");
            return View();
        }

        // POST: Utilisateurs/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUtilisateur,IdGenre,IdAdresse,Hote,Nom,Prenom,Login,Password,Email,Tel,Age")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                db.Utilisateurs.Add(utilisateur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdGenre = new SelectList(db.Genres, "IdGenre", "Nom", utilisateur.IdGenre);
            return View(utilisateur);
        }

        // GET: Utilisateurs/Edit/5
        public ActionResult Edit(int? id)
        {
            SessionUser sessionUser = (SessionUser)Session["Utilisateur"];

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = db.Utilisateurs.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdGenre = new SelectList(db.Genres, "IdGenre", "Nom", utilisateur.IdGenre);
            return View(utilisateur);
        }

        // POST: Utilisateurs/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUtilisateur,IdGenre,IdAdresse,Hote,Nom,Prenom,Login,Password,Email,Tel,Age")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utilisateur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Compte");
            }
            ViewBag.IdGenre = new SelectList(db.Genres, "IdGenre", "Nom", utilisateur.IdGenre);
            return View(utilisateur);
        }

        // GET: Utilisateurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = db.Utilisateurs.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // POST: Utilisateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utilisateur utilisateur = db.Utilisateurs.Find(id);
            db.Utilisateurs.Remove(utilisateur);
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
