using Cafemed.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Cafemed.Models.Model;

namespace Cafemed.Controllers
{
    public class HomeController : Controller
    {
        private CafemedDBContext db = new CafemedDBContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Hakkimizda()
        {
            return View(db.Hakkimizda.SingleOrDefault());
        }
        public ActionResult SliderPartial()
        {
            return View(db.Slider.ToList());
        }
        public ActionResult Menu()
        {
            return View(db.Urunler.Include("MenuKategori").OrderByDescending(x => x.UrunId));
        }
        public ActionResult Galeri()
        {
            return View(db.Galeri.ToList().OrderByDescending(x => x.GaleriId));
        }
        [Route("iletisim")]
        public ActionResult Iletisim()
        {
            return View(db.Iletisim.ToList());
        }
        [HttpPost]
        public ActionResult Iletisim(string adsoyad = null, string email = null, string telefon = null, string mesaj = null)
        {
            if (adsoyad != null && email != null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "deneme34785@gmail.com";
                WebMail.Password = "sakaryaspor";
                WebMail.SmtpPort = 587;
                WebMail.Send("deneme34785@gmail.com", telefon, email + "-" + mesaj);
                ViewBag.Uyari = "Mesajınız başarı ile gönderilmiştir.";

            }
            else
            {
                ViewBag.Uyari = "Hata Oluştu.Tekrar deneyiniz";
            }
            return View();
        }
        public ActionResult Blog(int Sayfa= 1)
        {
            return View(db.Blog.Include("Kategoriler").OrderByDescending(x => x.BlogId).ToPagedList(Sayfa, 2));
        }
        public ActionResult KategoriBlog(int id, int Sayfa = 1)
        {
            var b = db.Blog.Include("Kategoriler").OrderByDescending(x => x.BlogId).Where(x => x.Kategoriler.KategoriId == id).ToPagedList(Sayfa, 5);
            return View(b);
        }
        public ActionResult BlogKategoriPartial()
        {
            return PartialView(db.Kategoriler.Include("Blogs").ToList().OrderBy(x => x.KategoriAd));
        }
        public ActionResult BlogKayitPartial()
        {
            return PartialView(db.Blog.ToList().OrderByDescending(x=>x.BlogId));
        }
        public ActionResult BlogDetay(int id)
        {
            var b = db.Blog.Include("Kategoriler").Include("Yorums").Where(x => x.BlogId == id).SingleOrDefault();
            return View(b);
        }
                public JsonResult YorumYap(string icerik, string adsoyad, string eposta, int blogid)
        {
            if (icerik == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            db.Yorum.Add(new Yorum { Icerik = icerik, AdSoyad = adsoyad, Eposta = eposta, BlogId = blogid, Onay = false });
            db.SaveChanges();

            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FooterPartial()
        {
            ViewBag.Galeri = db.Galeri.ToList().OrderByDescending(x => x.GaleriId);

            ViewBag.Iletisim = db.Iletisim.FirstOrDefault();

            ViewBag.Blog = db.Blog.OrderByDescending(x => x.BlogId);
            return PartialView();
        }
        public ActionResult IndexBlog(int Sayfa = 1)
        {
            ViewBag.Blog = db.Blog.OrderByDescending(x => x.BlogId).ToPagedList(Sayfa, 3);
            return PartialView();
        }
    }
}