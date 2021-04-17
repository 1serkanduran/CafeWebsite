using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cafemed.Models.Model
{
    public class MenuKategori
    {
        [Key]
        public int MenuKategoriId { get; set; }
        [Required, StringLength(200, ErrorMessage = "200 karakter olmalıdır")]
        public string MenuKategoriAd { get; set; }
        public ICollection<Urunler> Uruns { get; set; }
    }
}