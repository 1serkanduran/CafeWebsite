using Cafemed.Models.DataContext;
using Cafemed.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cafemed.Controllers
{
    public class IletisimController : Controller
    {
        private CafemedDBContext db = new CafemedDBContext();
        // GET: Iletisim
        public ActionResult Index()
        {
            return View(db.Iletisim.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Iletisim iletisim)
        {
            if (ModelState.IsValid)
            {
                db.Iletisim.Add(iletisim);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.Uyari = "Güncellenecek hizmet bulunamadı";
            }
            var iletisim = db.Iletisim.Find(id);
            if ( iletisim== null)
            {
                return HttpNotFound();
            }
            return View(iletisim);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int? id, Iletisim iletisim)
        {

            if (ModelState.IsValid)
            {
                var h = db.Iletisim.Where(x => x.IletisimId == id).SingleOrDefault();
                h.Adres = iletisim.Adres;
                h.Facebook = iletisim.Facebook;
                h.İnstagram = iletisim.İnstagram;
                h.Whatsapp = iletisim.Whatsapp;
                h.AcilisKapanis = iletisim.AcilisKapanis;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iletisim);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                HttpNotFound();
            }
            var h = db.Iletisim.Find(id);
            if (h == null)
            {
                HttpNotFound();
            }
            db.Iletisim.Remove(h);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}