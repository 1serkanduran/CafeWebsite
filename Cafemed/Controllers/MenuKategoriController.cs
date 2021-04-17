using Cafemed.Models.DataContext;
using Cafemed.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cafemed.Controllers
{
    public class MenuKategoriController : Controller
    {
        private CafemedDBContext db = new CafemedDBContext();
        // GET: MenuKategori
        public ActionResult Index()
        {
            return View(db.MenuKategori.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(MenuKategori menuKategori)
        {
            if (ModelState.IsValid)
            {
                db.MenuKategori.Add(menuKategori);
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
            var menuKategori = db.MenuKategori.Find(id);
            if (menuKategori == null)
            {
                return HttpNotFound();
            }
            return View(menuKategori);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int? id, MenuKategori menuKategori)
        {

            if (ModelState.IsValid)
            {
                var h = db.MenuKategori.Where(x => x.MenuKategoriId == id).SingleOrDefault();
                h.MenuKategoriAd = menuKategori.MenuKategoriAd;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menuKategori);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                HttpNotFound();
            }
            var h = db.MenuKategori.Find(id);
            if (h == null)
            {
                HttpNotFound();
            }
            db.MenuKategori.Remove(h);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}