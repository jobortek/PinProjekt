using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PinProjekt.Models;
namespace PinProjekt.Controllers
{
    public class kosaricaController : Controller
    {
        DBartikliEntities dbart = new DBartikliEntities();
        DBuserSignupLoginEntities db = new DBuserSignupLoginEntities();
        // GET: kosarica
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Dodaj()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dodaj(TBLartikli tBLartikli)
        {
            int kosarica = 0; 
            if(tBLartikli.kolicina > 2)
            {
                return RedirectToAction("Registracija", "Home");
            }
             else
            {
                kosarica = 2;
            }
            return View();
        }
    }
}