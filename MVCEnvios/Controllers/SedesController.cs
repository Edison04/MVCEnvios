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
    public class SedesController : Controller
    {
        private MVCEnviosEntities db = new MVCEnviosEntities();
        private ServiceSede.ServicioSedeClient sedeServicio = new ServiceSede.ServicioSedeClient();

        // GET: Sedes
        public ActionResult Index()
        {
            return View(sedeServicio.ListarSedes());
        }

        // GET: Sedes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sede = sedeServicio.BuscarSede(id.Value);
            if (sede == null)
            {
                return HttpNotFound();
            }
            return View(sede);
        }

        // GET: Sedes/Create
        public ActionResult Create()
        {
            ViewBag.IdEstadoSede = new SelectList(db.EstadoSede, "Id", "Estado");
            return View();
        }

        // POST: Sedes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Direccion,Telefono,IdEstadoSede")] ServiceSede.Sede sede)
        {
            if (ModelState.IsValid)
            {
                sedeServicio.AgregarSede(sede);
                return RedirectToAction("Index");
            }

            return View(sede);
        }

        // GET: Sedes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sede = sedeServicio.BuscarSede(id.Value);
            if (sede == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEstadoSede = new SelectList(db.EstadoSede, "Id", "Estado", sede.IdEstadoSede);
            return View(sede);
        }

        // POST: Sedes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Direccion,Telefono,IdEstadoSede")] ServiceSede.Sede sede)
        {
            if (ModelState.IsValid)
            {
                sedeServicio.EditarSedes(sede);
                return RedirectToAction("Index");
            }
            return View(sede);
        }

        // GET: Sedes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sede = sedeServicio.BuscarSede(id.Value);
            if (sede == null)
            {
                return HttpNotFound();
            }
            return View(sede);
        }

        // POST: Sedes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Sede sede = db.Sede.Find(id);
            db.Sede.Remove(sede);
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
