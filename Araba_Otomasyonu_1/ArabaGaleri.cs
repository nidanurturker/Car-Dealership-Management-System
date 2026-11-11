using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Araba_Otomasyonu_1
{
    [Table("Table_ArabaGaleri")]
    internal class ArabaGaleri
    {
        [Key]
        public int AraGal_Id { get; set; }

        [ForeignKey("Araba")]
        public int Araba_Id { get; set; }

        [ForeignKey("Galeri")]
        public int Galeri_Id { get; set; }

        public virtual Araba Araba { get; set; }
        public virtual Galeri Galeri { get; set; }
    }
}
