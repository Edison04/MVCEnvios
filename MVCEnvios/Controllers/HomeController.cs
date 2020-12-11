using MVCEnvios.Data;
using MVCEnvios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCEnvios.Controllers
{
    public class HomeController : Controller
    {
        //private MVCEnviosEntities db = new MVCEnviosEntities();
        private ServiceLogin.ServicioLoginClient service = new ServiceLogin.ServicioLoginClient();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Seguimiento()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        public string Login(string usuario, string password)
        {
            var user = service.Login(usuario, password);

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Usuario, false);
                HttpContext.Session.Add("usuario", user.Usuario);
                HttpContext.Session.Add("rol", user.Rol);

                return Newtonsoft.Json.JsonConvert.SerializeObject(user);
            }
            else
            {
                return "{\"error\":\"El usuario no Esiste\"}";
            }

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index");
        }


    }
}