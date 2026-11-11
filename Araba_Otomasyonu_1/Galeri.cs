using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Araba_Otomasyonu_1
{
    [Table("Table_Galeri")]
    internal class Galeri
    {
        [Key]
        public int Galeri_Id { get; set; }

        [Required, MaxLength(50)]
        public string Galeri_Isim { get; set; }

        [ForeignKey("Satis")]
        public int Satis_Id { get; set; }

        public virtual Satis Satis { get; set; }
        public virtual ICollection<ArabaGaleri> ArabaGaleris { get; set; }
    }
}
