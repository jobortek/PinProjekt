using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//dodajemo da možemo koristit svoju bazu
using PinProjekt.Models;

namespace PinProjekt.Controllers
{
    public class HomeController : Controller
    {
        // bazi pristupamo sa db
        DBuserSignupLoginEntities db = new DBuserSignupLoginEntities();
        DBartikliEntities dbart = new DBartikliEntities();
        // GET: Home
        public ActionResult Index()
        {
            using(dbart)
            {
                return View(dbart.TBLartiklis.ToList());
            }
         
        }

        // Akcija za klik na Signup
        public ActionResult Registracija()
        {
            return View();
        }

        [HttpPost]
        // prima tip podatka "imetablice" imeVarijable koje korisnik šalje 
        public ActionResult Registracija(TBLUserInfo tBLUserInfo)
        {

            // ispitujemo da li u bazi postoji več user koji se opet želi registrirati, a ako ne postoji dodajemo ga u bazu
            if(db.TBLUserInfoes.Any(x=>x.UsernameUs == tBLUserInfo.UsernameUs))
            {
                // da bi se ovo vidlo moramo ići na View od Signup-a i dodati div class="form-group" na kraju sa lablelom @ViewBag.Notification
                ViewBag.Notification = "Ovaj korisnik je već registriran";
                return View();
            }
            else
            {
                db.TBLUserInfoes.Add(tBLUserInfo);
                db.SaveChanges();

                //Treba nam za provjeru i linkanje na layout-u da ga možemomodificirati 
                Session["IdUsSS"] = tBLUserInfo.IdUs.ToString();
                Session["UsernameSS"] = tBLUserInfo.UsernameUs.ToString();

                return RedirectToAction("Index", "Home");

            }
        }

        // Akcija za Logut Vrati na index i počisti sešn
        public ActionResult Odjava()
        {
            // Čišćenje sešna 
            Session.Clear();
            // umjesto vračanja View-a možemo navesti koji točno da se izvede
            return RedirectToAction("Index", "Home");
        }
        
        //Akcija za Login -> novi View za login
        [HttpGet]
        public ActionResult Prijava()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Prijava(TBLUserInfo tBLUserInfo)
        {
            
            //FristorDefalut vrača prvi element ili default vrijednost ako propadne
            var checkLogin = db.TBLUserInfoes.Where(x => x.UsernameUs.Equals(tBLUserInfo.UsernameUs) && x.PasswordUs.Equals(tBLUserInfo.PasswordUs)).FirstOrDefault();
            
            if(tBLUserInfo.UsernameUs == "Admin")
            {
                // moramo stavit u string da možemo ispitat jel null - npr u Shared -> _Layout 
                Session["UsernameSS"] = tBLUserInfo.UsernameUs.ToString();
                return RedirectToAction("skladiste", "skladiste");
                
            }
            else if(checkLogin != null)
            {
                Session["IdUsSS"] = tBLUserInfo.IdUs.ToString();
                Session["UsernameSS"] = tBLUserInfo.UsernameUs.ToString();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Notification = "Krivo korisničko ime ili lozinka.";
            }
            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Kontakt()
        {
            return View();
        }


    }
}