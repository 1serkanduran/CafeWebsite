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
    public class SliderController : Controller

    {
        private CafemedDBContext db = new CafemedDBContext();
        // GET: Kimlik
        public ActionResult Index()
        {
            return View(db.Slider.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Slider slider, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimURL.FileName);

                    string logoname = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save(@"~/Uploads/Slider/" + logoname);

                    slider.ResimURL = @"/Uploads/Slider/" + logoname;
                }
                db.Slider.Add(slider);
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
            var slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int? id, Slider slider, HttpPostedFileBase ResimURL)
        {

            if (ModelState.IsValid)
            {
                var h = db.Slider.Where(x => x.SliderId == id).SingleOrDefault();
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(h.ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(h.ResimURL));
                    }
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimURL.FileName);

                    string kimlikname = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save(@"~/Uploads/Slider/" + kimlikname);

                    h.ResimURL = @"/Uploads/Slider/" + kimlikname;
                }
                h.Baslik = slider.Baslik;
                h.Aciklama = slider.Aciklama;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                HttpNotFound();
            }
            var h = db.Slider.Find(id);
            if (h == null)
            {
                HttpNotFound();
            }
            db.Slider.Remove(h);
            db.SaveChanges();
            System.IO.File.Delete(Server.MapPath(h.ResimURL));
            return RedirectToAction("Index");
        }
    }
}