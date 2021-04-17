using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cafemed.Models.Model
{
    [Table("Galeri")]
    public class Galeri
    {
        [Key]
        public int GaleriId { get; set; }
        [DisplayName("Galeri Başlık")]
        public string Baslik { get; set; }
        [DisplayName("Galeri Açıklama")]
        public string Aciklama { get; set; }
        [DisplayName("Galeri Fotoğraf"), StringLength(250)]
        public string ResimURL { get; set; }
    }
}