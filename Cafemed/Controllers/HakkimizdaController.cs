using Cafemed.Models.DataContext;
using Cafemed.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Cafemed.Controllers
{
    public class HakkimizdaController : Controller
    {
        CafemedDBContext db = new CafemedDBContext();
        // GET: Hakkimizda
        public ActionResult Index()
        {
            return View(db.Hakkimizda.ToList());
        }
        public ActionResult Edit(int id)
        {
            var h = db.Hakkimizda.Where(x => x.HakkimizdaId == id).SingleOrDefault();
            return View(h);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Hakkimizda h)
        {
            if (ModelState.IsValid)
            {
                var hakkimizda = db.Hakkimizda.Where(x => x.HakkimizdaId == id).SingleOrDefault();
                hakkimizda.Baslik=h.Baslik;
                hakkimizda.Aciklama = h.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}