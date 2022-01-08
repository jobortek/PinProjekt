using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PinProjekt.Models;

namespace PinProjekt.Controllers
{
    public class skladisteController : Controller
    {
        DBartikliEntities dbart = new DBartikliEntities();
        // GET: skladiste

        // Upis robe na stanje
        public ActionResult Skladiste()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Skladiste(TBLartikli tBLartikli)
        {
            if(dbart.TBLartiklis.Any(x=>x.naziv == tBLartikli.naziv))
            {
                ViewBag.Notification = "Artikl sa tim nazivom već postoji";
                return View();
            }
            else if(tBLartikli.kolicina == 0)
            {
                ViewBag.Notification = "Količina artikla ne smije biti 0.";
            }
            else if(tBLartikli.cijena == 0)
            {
                ViewBag.Notification = "Cijena artikla ne smije biti 0";
            }
            else
            {
                //za sliku
                string filename = Path.GetFileNameWithoutExtension(tBLartikli.ImageFile.FileName);
                string extension = Path.GetExtension(tBLartikli.ImageFile.FileName);
                filename = filename + extension;
                tBLartikli.slika = "../Image/" + filename;
                filename = Path.Combine(Server.MapPath("../Image/"), filename);
                tBLartikli.ImageFile.SaveAs(filename);
                using (dbart)
                {
                    dbart.TBLartiklis.Add(tBLartikli);
                    dbart.SaveChanges();
                    
                }
           
                ModelState.Clear();
          
            }

            ViewBag.Notification = "Artikl je uspiješno unesen";
            return View();
        }

        // Pregled stanja skaladista
        [HttpGet]
        public ActionResult Stanje()
        {
            return View(dbart.TBLartiklis.ToList());
        }

        [HttpPost]
        public ActionResult Kosarica()
        {
            List<int> kosarica = new List<int>();
            kosarica.Add(1);
            return View();
        }


    }
}