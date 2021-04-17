using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cafemed.Models.Model
{
    [Table("Urunler")]
    public class Urunler
    {
        [Key]
        public int UrunId { get; set; }
        public string UrunAd { get; set; }
        public string UrunAciklama { get; set; }
        public string Fiyat { get; set; }
        public int MenuKategoriId { get; set; }
        public MenuKategori MenuKategori { get; set; }


    }
}