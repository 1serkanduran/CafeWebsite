using Cafemed.Models.DataContext;
using Cafemed.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cafemed.Controllers
{
    public class UrunlerController : Controller
    {
        private CafemedDBContext db = new CafemedDBContext();
        // GET: Urunler
        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return View(db.Urunler.Include("MenuKategori").ToList().OrderByDescending(x => x.UrunId));
        }
        public ActionResult Create()
        {
            ViewBag.MenuKategoriId = new SelectList(db.MenuKategori, "MenuKategoriId", "MenuKategoriAd");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Urunler urun)
        {
            if (ModelState.IsValid)
            {
                db.Urunler.Add(urun);
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
            var urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            ViewBag.menuKategoriId = new SelectList(db.MenuKategori, "MenuKategoriId", "MenuKategoriAd", urunler.MenuKategoriId);
            return View(urunler);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, Urunler urunler)
        {

            if (ModelState.IsValid)
            {
                var h = db.Urunler.Where(x => x.UrunId == id).SingleOrDefault();
                h.UrunAd = urunler.UrunAd;
                h.UrunAciklama = urunler.UrunAciklama;
                h.Fiyat = urunler.Fiyat;
                h.MenuKategoriId = urunler.MenuKategoriId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(urunler);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                HttpNotFound();
            }
            var h = db.Urunler.Find(id);
            if (h == null)
            {
                HttpNotFound();
            }
            db.Urunler.Remove(h);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}