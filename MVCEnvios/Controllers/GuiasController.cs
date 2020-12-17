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
        private ServiceGuia.ServicioGuiaClient guiaServicio = new ServiceGuia.ServicioGuiaClient();
        private ServiceCliente.ServicioClienteClient clienteServicio = new ServiceCliente.ServicioClienteClient();
        private ServiceSede.ServicioSedeClient sedeServicio = new ServiceSede.ServicioSedeClient();

        // GET: Guias
        public ActionResult Index()
        {            
            return View(guiaServicio.ListarGuias());
        }

        [HttpPost]
        public ActionResult Cedula(string cedula)
        {
            //var guia = db.Guia.Where(g => g.Cliente.Cedula.Equals(cedula));

            var cliente = (from client in clienteServicio.ListarClientes()
                           where client.Cedula.Trim() == cedula
                           select client).FirstOrDefault();

            var guia = (from ced in guiaServicio.ListarGuias()
                        where ced.IdCliente == cliente.Id
                        select ced).ToList();
            return View(guia.ToList());
        }

        // GET: Guias/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var guia = guiaServicio.BuscarGuias(id.Value);
            if (guia == null)
            {
                return HttpNotFound();
            }
            return View(guia);
        }

        // GET: Guias/Create
        public ActionResult Create()
        {
            var cliente = (from c in clienteServicio.ListarClientes()
                           select new
                           {
                               id = c.Id,
                               NombreCompleto = c.Cedula + " - " + c.Nombre + " " + c.Apellidos
                           });
            ViewBag.IdCliente = new SelectList(cliente, "Id", "NombreCompleto");
            ViewBag.IdSede = new SelectList(sedeServicio.ListarSedes(), "Id", "Nombre");
            return View();
        }

        // POST: Guias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha,DireccionOrigen,TelefonoEmisor,Receptor,DireccionDestino,TelefonoReceptor,TipoPaquete,CiudadDestino,PesoPaquete,ValorEnvio,Observacion,IdSede,IdCliente")] ServiceGuia.Guia guia)
        {
            if (ModelState.IsValid)
            {
                guia.ValorEnvio = 0;
                var vTotal = guia.PesoPaquete * 5000;
                guia.ValorEnvio = vTotal;
                guiaServicio.AgregarGuia(guia);
                return RedirectToAction("Index");
            }

            ViewBag.IdCliente = new SelectList(clienteServicio.ListarClientes(), "Id", "Cedula", guia.IdCliente);
            ViewBag.IdSede = new SelectList(sedeServicio.ListarSedes(), "Id", "Nombre", guia.IdSede);
            return View(guia);
        }

        // GET: Guias/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var guia = guiaServicio.BuscarGuias(id.Value);
            if (guia == null)
            {
                return HttpNotFound();
            }
            var cliente = (from c in clienteServicio.ListarClientes()
                           select new
                           {
                               id = c.Id,
                               NombreCompleto = c.Cedula + " - " + c.Nombre + " " + c.Apellidos
                           });
            ViewBag.IdCliente = new SelectList(cliente, "Id", "NombreCompleto", guia.IdCliente);
            ViewBag.IdSede = new SelectList(sedeServicio.ListarSedes(), "Id", "Nombre", guia.IdSede);
            return View(guia);
        }

        // POST: Guias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha,DireccionOrigen,TelefonoEmisor,Receptor,DireccionDestino,TelefonoReceptor,TipoPaquete,CiudadDestino,PesoPaquete,ValorEnvio,Observacion,IdSede,IdCliente")] ServiceGuia.Guia guia)
        {
            if (ModelState.IsValid)
            {
                var vTotal = guia.PesoPaquete * 5000;
                guia.ValorEnvio = vTotal;
                guiaServicio.EditarGuias(guia);
                return RedirectToAction("Index");
            }
            ViewBag.IdCliente = new SelectList(clienteServicio.ListarClientes(), "Id", "Cedula", guia.IdCliente);
            ViewBag.IdSede = new SelectList(sedeServicio.ListarSedes(), "Id", "Nombre", guia.IdSede);
            return View(guia);
        }

        // GET: Guias/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var guia = guiaServicio.BuscarGuias(id.Value);
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
            guiaServicio.EliminarGuias(id);
            return RedirectToAction("Index");
        }
    }
}
