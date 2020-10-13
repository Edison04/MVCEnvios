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
    public class GuiasController : Controller
    {
        private MVCEnviosEntities db = new MVCEnviosEntities();

        // GET: Guias
        public ActionResult Index()
        {
            var guia = db.Guia.Include(g => g.Cliente).Include(g => g.Sede1);
            return View(guia.ToList());
        }

        // GET: Guias/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guia guia = db.Guia.Find(id);
            if (guia == null)
            {
                return HttpNotFound();
            }
            return View(guia);
        }

        // GET: Guias/Create
        public ActionResult Create()
        {
            ViewBag.IdCliente = new SelectList(db.Cliente, "Id", "Cedula");
            ViewBag.IdSede = new SelectList(db.Sede, "Id", "Nombre");
            return View();
        }

        // POST: Guias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha,Sede,Emisor,DireccionOrigen,TelefonoEmisor,Receptor,DireccionDestino,TelefonoReceptor,TipoPaquete,CiudadDestino,PesoPaquete,ValorEnvio,Observacion,IdSede,IdCliente")] Guia guia)
        {
            if (ModelState.IsValid)
            {
                db.Guia.Add(guia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCliente = new SelectList(db.Cliente, "Id", "Cedula", guia.IdCliente);
            ViewBag.IdSede = new SelectList(db.Sede, "Id", "Nombre", guia.IdSede);
            return View(guia);
        }

        // GET: Guias/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guia guia = db.Guia.Find(id);
            if (guia == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCliente = new SelectList(db.Cliente, "Id", "Cedula", guia.IdCliente);
            ViewBag.IdSede = new SelectList(db.Sede, "Id", "Nombre", guia.IdSede);
            return View(guia);
        }

        // POST: Guias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha,Sede,Emisor,DireccionOrigen,TelefonoEmisor,Receptor,DireccionDestino,TelefonoReceptor,TipoPaquete,CiudadDestino,PesoPaquete,ValorEnvio,Observacion,IdSede,IdCliente")] Guia guia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCliente = new SelectList(db.Cliente, "Id", "Cedula", guia.IdCliente);
            ViewBag.IdSede = new SelectList(db.Sede, "Id", "Nombre", guia.IdSede);
            return View(guia);
        }

        // GET: Guias/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guia guia = db.Guia.Find(id);
            if (guia == null)
            {
                return HttpNotFound();
            }
            return View(guia);
        }

        // POST: Guias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Guia guia = db.Guia.Find(id);
            db.Guia.Remove(guia);
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
