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
        [Route("")]
        [Route("Anasayfa")]
        public ActionResult Index()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.Galeri = db.Galeri.ToList().OrderByDescending(x => x.GaleriId);
            return View();
        }
        [Route("Hakkimizda")]
        public ActionResult Hakkimizda()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Hakkimizda.SingleOrDefault());
        }
        public ActionResult SliderPartial()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Slider.ToList());
        }
        [Route("Menu")]
        public ActionResult Menu()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Urunler.Include("MenuKategori").OrderByDescending(x => x.UrunId));
        }
        [Route("Galeri")]
        public ActionResult Galeri()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Galeri.ToList().OrderByDescending(x => x.GaleriId));
        }
        [Route("Iletisim")]
        public ActionResult Iletisim()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
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
        [Route("BlogPost")]
        public ActionResult Blog(int Sayfa = 1)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Blog.Include("Kategoriler").OrderByDescending(x => x.BlogId).ToPagedList(Sayfa, 2));
        }
        [Route("BlogPost/{kategoriad}/{id:int}")]
        public ActionResult KategoriBlog(int id, int Sayfa = 1)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            var b = db.Blog.Include("Kategoriler").OrderByDescending(x => x.BlogId).Where(x => x.Kategoriler.KategoriId == id).ToPagedList(Sayfa, 5);
            return View(b);
        }
        public ActionResult BlogKategoriPartial()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return PartialView(db.Kategoriler.Include("Blogs").ToList().OrderBy(x => x.KategoriAd));
        }
        public ActionResult BlogKayitPartial()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return PartialView(db.Blog.ToList().OrderByDescending(x => x.BlogId));
        }
        [Route("BlogPost/{baslik}-{id:int}")]
        public ActionResult BlogDetay(int id)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
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
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.Galeri = db.Galeri.ToList().OrderByDescending(x => x.GaleriId);

            ViewBag.Iletisim = db.Iletisim.FirstOrDefault();

            ViewBag.Blog = db.Blog.OrderByDescending(x => x.BlogId);
            return PartialView();
        }
        public ActionResult RightMenuPartial()
        {
            ViewBag.Galeri = db.Galeri.ToList().OrderByDescending(x => x.GaleriId);
            return PartialView();
        }
        public ActionResult IndexBlog(int Sayfa = 1)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.Blog = db.Blog.OrderByDescending(x => x.BlogId).ToPagedList(Sayfa, 3);
            return PartialView();
        }
    }
}