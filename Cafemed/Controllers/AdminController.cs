using Cafemed.Models.DataContext;
using Cafemed.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cafemed.Controllers
{
    public class AdminController : Controller
    {
        CafemedDBContext db = new CafemedDBContext();
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.BlogSay = db.Blog.Count();
            ViewBag.KategoriSay = db.Kategoriler.Count();
            ViewBag.GaleriSay = db.Galeri.Count();
            ViewBag.YorumSay = db.Yorum.Count();
            ViewBag.YorumOnay = db.Yorum.Where(x => x.Onay == false).Count();
            var sorgu = db.Kategoriler.ToList();
            return View(sorgu);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            var login = db.Admin.Where(x => x.AdminKullaniciAdi == admin.AdminKullaniciAdi && x.Sifre == admin.Sifre).SingleOrDefault();
            if (login != null)
            {
                if (login.AdminKullaniciAdi == admin.AdminKullaniciAdi && login.Sifre == admin.Sifre) // Crypto.Hash(admin.Sifre, "MD5"))
                {
                    Session["adminid"] = login.AdminId;
                    Session["AdminKullaniciAdi"] = login.AdminKullaniciAdi;
                    //Session["yetki"] = login.Yetki;
                    return RedirectToAction("Index", "Admin");
                }
            }

            ViewBag.Uyari = "Kullanıcı adı yada şifre yanlış";
            return View(admin);
        }
        public ActionResult Logout()
        {
            Session["adminid"] = null;
            Session["AdminKullaniciAdi"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }
    }
    
}