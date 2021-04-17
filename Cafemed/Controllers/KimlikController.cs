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
    public class KimlikController : Controller
    {
        private  CafemedDBContext db = new CafemedDBContext();
        // GET: Kimlik
        public ActionResult Index()
        {
            return View(db.Kimlik.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Kimlik kimlik, HttpPostedFileBase LogoURL)
        {
            if (ModelState.IsValid)
            {
                if (LogoURL != null)
                {
                    WebImage img = new WebImage(LogoURL.InputStream);
                    FileInfo imgInfo = new FileInfo(LogoURL.FileName);

                    string logoname = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Resize(500, 500);
                    img.Save(@"~/Uploads/Kimlik/" + logoname);

                    kimlik.LogoURL = @"/Uploads/Kimlik/" + logoname;
                }
                db.Kimlik.Add(kimlik);
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
            var kimlik = db.Kimlik.Find(id);
            if (kimlik==null)
            {
                return HttpNotFound();
            }
            return View(kimlik);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int? id, Kimlik kimlik, HttpPostedFileBase LogoURL)
        {

            if (ModelState.IsValid)
            {
                var h = db.Kimlik.Where(x => x.KimlikId == id).SingleOrDefault();
                if (LogoURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(h.LogoURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(h.LogoURL));
                    }
                    WebImage img = new WebImage(LogoURL.InputStream);
                    FileInfo imgInfo = new FileInfo(LogoURL.FileName);

                    string kimlikname = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Resize(500, 500);
                    img.Save(@"~/Uploads/Kimlik/" + kimlikname);

                    h.LogoURL = @"/Uploads/Kimlik/" + kimlikname;
                }
                h.Title = kimlik.Title;
                h.Keywords = kimlik.Keywords;
                h.Description = kimlik.Description;
                h.Unvan = kimlik.Unvan;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kimlik);
        }
        public ActionResult Delete(int? id)
        {
            if (id==null)
            {
                HttpNotFound();
            }
            var h = db.Kimlik.Find(id);
            if (h==null)
            {
                HttpNotFound();
            }
            db.Kimlik.Remove(h);
            db.SaveChanges();
            System.IO.File.Delete(Server.MapPath(h.LogoURL));
            return RedirectToAction("Index");
        }

    }
}