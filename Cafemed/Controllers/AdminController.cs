using Cafemed.Models.DataContext;
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
            var sorgu = db.Kategoriler.ToList();
            return View(sorgu);
        }
    }
}