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
    public class GaleriController : Controller
    {
        private CafemedDBContext db = new CafemedDBContext();
        // GET: Galeri
        public ActionResult Index()
        {
            return View(db.Galeri.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Galeri galeri, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimURL.FileName);

                    string logoname = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Resize(500, 500);
                    img.Save(@"~/Uploads/Galeri/" + logoname);

                    galeri.ResimURL = @"/Uploads/Galeri/" + logoname;
                }
                db.Galeri.Add(galeri);
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
            var galeri = db.Galeri.Find(id);
            if (galeri == null)
            {
                return HttpNotFound();
            }
            return View(galeri);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int? id, Galeri galeri, HttpPostedFileBase ResimURL)
        {

            if (ModelState.IsValid)
            {
                var h = db.Galeri.Where(x => x.GaleriId == id).SingleOrDefault();
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(h.ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(h.ResimURL));
                    }
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimURL.FileName);

                    string kimlikname = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Resize(500, 500);
                    img.Save(@"~/Uploads/Galeri/" + kimlikname);

                    h.ResimURL = @"/Uploads/Galeri/" + kimlikname;
                }
                h.Baslik = galeri.Baslik;
                h.Aciklama = galeri.Aciklama;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(galeri);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                HttpNotFound();
            }
            var h = db.Galeri.Find(id);
            if (h == null)
            {
                HttpNotFound();
            }
            db.Galeri.Remove(h);
            db.SaveChanges();
            System.IO.File.Delete(Server.MapPath(h.ResimURL));
            return RedirectToAction("Index");
        }
    }
}