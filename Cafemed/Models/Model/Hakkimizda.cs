using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cafemed.Models.Model
{
    [Table("Hakkimizda")]
    public class Hakkimizda
    {
        [Key]
        public int HakkimizdaId { get; set; }
        [Required]
        [DisplayName("Hakkimizda Baslik")]
        public string Baslik { get; set; }
        [Required]
        [DisplayName("Hakkimizda Açıklama")]
        public string Aciklama { get; set; }
    }
}