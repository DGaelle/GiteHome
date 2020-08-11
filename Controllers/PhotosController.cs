using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GiteHouse;
using GiteHouse.Models;
using GiteHouse.ViewModels;

namespace GiteHouse.Controllers
{
    public class PhotosController : Controller
    {
        private GiteHouseEntities db = new GiteHouseEntities();

        // GET: Photos
        public ActionResult Index()
        {


            var photos = db.Photos.Include(p => p.Hebergement);
            return View(photos.ToList());
        }

 
        // GET: Photos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // GET: Photos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Photos/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdHebergement,Nom,IsDefault,NomAleatoire")] Photo photo, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.ContentLength > 0)
                    try
                    {
                        bool isFromDetails = false;
                      
                        string path = Path.Combine(Server.MapPath("~/images/"),
                                                   Path.GetFileName(ImageFile.FileName));
                        ImageFile.SaveAs(path);

                        ViewBag.MessageDwdOk = "Fichier correctement téléchargé";


                        SessionUser sessionUser = (SessionUser)Session["Utilisateur"];

                        if(photo.IdHebergement > 0)
                        {
                            isFromDetails = true;
                        }
                        else
                        {
                            photo.IdHebergement = sessionUser.Hebergement.IdHebergement;
                        }
                        photo.Nom = "/images/" + ImageFile.FileName;

                        db.Photos.Add(photo);
                        db.SaveChanges();


                        if (isFromDetails == true)
                        {
                            return RedirectToAction("Details", "Hebergements", new { id = photo.IdHebergement});

                        }
                        else
                        {
                            return RedirectToAction("Index", "Hebergements");
                        }

                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
               
            }

            return View();
        }

        // GET: Photos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdHebergement = new SelectList(db.Hebergements, "IdHebergement", "Nom", photo.IdHebergement);
            return View(photo);
        }

        // POST: Photos/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPhoto,Nom,IdHebergement,IsDefault,NomAleatoire")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdHebergement = new SelectList(db.Hebergements, "IdHebergement", "Nom", photo.IdHebergement);
            return View(photo);
        }

        // GET: Photos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            db.Photos.Remove(photo);
            db.SaveChanges();
            if (photo == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Details", "Hebergements", new { id = photo.IdHebergement });
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = db.Photos.Find(id);
            db.Photos.Remove(photo);
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
