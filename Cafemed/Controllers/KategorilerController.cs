using Cafemed.Models.DataContext;
using Cafemed.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cafemed.Controllers
{
    public class KategorilerController : Controller
    {
        private CafemedDBContext db = new CafemedDBContext();
        // GET: Kategoriler
        public ActionResult Index()
        {
            return View(db.Kategoriler.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Kategoriler kategori)
        {
            if (ModelState.IsValid)
            {
                db.Kategoriler.Add(kategori);
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
            var kategori = db.Kategoriler.Find(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int? id, Kategoriler kategori)
        {

            if (ModelState.IsValid)
            {
                var h = db.Kategoriler.Where(x => x.KategoriId == id).SingleOrDefault();
                h.KategoriAd = kategori.KategoriAd;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kategori);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                HttpNotFound();
            }
            var h = db.Kategoriler.Find(id);
            if (h == null)
            {
                HttpNotFound();
            }
            db.Kategoriler.Remove(h);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}