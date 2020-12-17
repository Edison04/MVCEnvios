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
    public class LoginsController : Controller
    {
        private MVCEnviosEntities db = new MVCEnviosEntities();
        private ServiceLogin.ServicioLoginClient loginServicio = new ServiceLogin.ServicioLoginClient();

        // GET: Logins
        public ActionResult Index()
        {
            return View(loginServicio.ListarLogins());
        }

        // GET: Logins/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var login = loginServicio.BuscarLogin(id.Value);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // GET: Logins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Logins/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Usuario,Password,Rol")] ServiceLogin.Login login)
        {
            if (ModelState.IsValid)
            {
                loginServicio.AgregarLogin(login);
                return RedirectToAction("Index");
            }

            return View(login);
        }

        // GET: Logins/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var login = loginServicio.BuscarLogin(id.Value);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Logins/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Usuario,Password,Rol")] ServiceLogin.Login login)
        {
            if (ModelState.IsValid)
            {
                loginServicio.EditarLogins(login);
                return RedirectToAction("Index");
            }
            return View(login);
        }

        // GET: Logins/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var login = loginServicio.BuscarLogin(id.Value);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            loginServicio.EliminarLogins(id);
            return RedirectToAction("Index");
        }
    }
}
