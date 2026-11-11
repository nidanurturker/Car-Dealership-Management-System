using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Araba_Otomasyonu_1
{
    [Table("Table_Satis")]
    internal class Satis
    {
        [Key]
        public int Satis_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Satis_Isim { get; set; }

        public virtual ICollection<Galeri> Galeris { get; set; }

    }
}
