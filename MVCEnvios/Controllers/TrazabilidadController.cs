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
    public class TrazabilidadController : Controller
    {
        private MVCEnviosEntities db = new MVCEnviosEntities();
        private ServiceTrazabilidad.ServicioTrazabilidadClient trazabilidadServicio = new ServiceTrazabilidad.ServicioTrazabilidadClient();

        // GET: Trazabilidad
        public ActionResult Index()
        {
            //var trazabilidad = db.Trazabilidad.Include(t => t.EstadoPaquete).Include(t => t.Guia);

            return View(trazabilidadServicio.ListarTrazabilidades());
        }

        [HttpPost]
        public ActionResult Guia(string guia)
        {
            var numGuia = Convert.ToInt32(guia);
            var traza = db.Trazabilidad.Where(t => t.Guia.Id.Equals(numGuia));
            return View(traza.ToList());
        }

        // GET: Trazabilidad/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trazabilidad trazabilidad = db.Trazabilidad.Find(id);
            if (trazabilidad == null)
            {
                return HttpNotFound();
            }
            return View(trazabilidad);
        }

        // GET: Trazabilidad/Create
        public ActionResult Create()
        {
            ViewBag.IdEstadoPaquete = new SelectList(db.EstadoPaquete, "Id", "Estado");
            ViewBag.IdGuia = new SelectList(db.Guia, "Id", "Id");
            return View();
        }

        // POST: Trazabilidad/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha,Origen,Destino,Observaciones,IdGuia,IdEstadoPaquete")] Trazabilidad trazabilidad)
        {
            if (ModelState.IsValid)
            {
                db.Trazabilidad.Add(trazabilidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEstadoPaquete = new SelectList(db.EstadoPaquete, "Id", "Estado", trazabilidad.IdEstadoPaquete);
            ViewBag.IdGuia = new SelectList(db.Guia, "Id", "Sede", trazabilidad.IdGuia);
            return View(trazabilidad);
        }

        // GET: Trazabilidad/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trazabilidad trazabilidad = db.Trazabilidad.Find(id);
            if (trazabilidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEstadoPaquete = new SelectList(db.EstadoPaquete, "Id", "Estado", trazabilidad.IdEstadoPaquete);
            ViewBag.IdGuia = new SelectList(db.Guia, "Id", "Id", trazabilidad.IdGuia);
            return View(trazabilidad);
        }

        // POST: Trazabilidad/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha,Origen,Destino,Observaciones,IdGuia,IdEstadoPaquete")] Trazabilidad trazabilidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trazabilidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEstadoPaquete = new SelectList(db.EstadoPaquete, "Id", "Estado", trazabilidad.IdEstadoPaquete);
            ViewBag.IdGuia = new SelectList(db.Guia, "Id", "Sede", trazabilidad.IdGuia);
            return View(trazabilidad);
        }

        // GET: Trazabilidad/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trazabilidad trazabilidad = db.Trazabilidad.Find(id);
            if (trazabilidad == null)
            {
                return HttpNotFound();
            }
            return View(trazabilidad);
        }

        // POST: Trazabilidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Trazabilidad trazabilidad = db.Trazabilidad.Find(id);
            db.Trazabilidad.Remove(trazabilidad);
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
