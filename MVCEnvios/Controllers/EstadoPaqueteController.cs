using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCEnvios.Models;

namespace MVCEnvios.Controllers
{
    public class EstadoPaqueteController : Controller
    {
        private MVCEnviosEntities db = new MVCEnviosEntities();

        // GET: EstadoPaquete
        public ActionResult Index()
        {
            return View(db.EstadoPaquete.ToList());
        }

        // GET: EstadoPaquete/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoPaquete estadoPaquete = db.EstadoPaquete.Find(id);
            if (estadoPaquete == null)
            {
                return HttpNotFound();
            }
            return View(estadoPaquete);
        }

        // GET: EstadoPaquete/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoPaquete/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Estado")] EstadoPaquete estadoPaquete)
        {
            if (ModelState.IsValid)
            {
                db.EstadoPaquete.Add(estadoPaquete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estadoPaquete);
        }

        // GET: EstadoPaquete/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoPaquete estadoPaquete = db.EstadoPaquete.Find(id);
            if (estadoPaquete == null)
            {
                return HttpNotFound();
            }
            return View(estadoPaquete);
        }

        // POST: EstadoPaquete/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Estado")] EstadoPaquete estadoPaquete)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadoPaquete).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estadoPaquete);
        }

        // GET: EstadoPaquete/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoPaquete estadoPaquete = db.EstadoPaquete.Find(id);
            if (estadoPaquete == null)
            {
                return HttpNotFound();
            }
            return View(estadoPaquete);
        }

        // POST: EstadoPaquete/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            EstadoPaquete estadoPaquete = db.EstadoPaquete.Find(id);
            db.EstadoPaquete.Remove(estadoPaquete);
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
