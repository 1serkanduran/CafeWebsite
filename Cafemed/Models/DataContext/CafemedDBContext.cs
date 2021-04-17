using Cafemed.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cafemed.Models.DataContext
{
    public class CafemedDBContext: DbContext
    {
        public CafemedDBContext(): base("CafemedDB")
        {

        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Hakkimizda> Hakkimizda { get; set; }
        public DbSet<Galeri> Galeri { get; set; }
        public DbSet<MenuKategori> MenuKategori { get; set; }
        public DbSet<Urunler> Urunler { get; set; }
        public DbSet<Iletisim> Iletisim { get; set; }
        public DbSet<Kategoriler> Kategoriler { get; set; }
        public DbSet<Kimlik> Kimlik { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<Yorum> Yorum { get; set; }
    }
}