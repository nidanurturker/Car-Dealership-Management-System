using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Araba_Otomasyonu_1
{
    [Table("Table_Araba")]
   internal class Araba
    {
        [Key]
        public int Araba_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Araba_Marka { get; set; }

        [Required]
        [MaxLength(50)]
        public string Araba_Model { get; set; }

        [Required]
        [MaxLength(50)]
        public string Araba_Model_Yili { get; set; }

        [Required]
        public decimal Araba_Fiyat { get; set; }

        public virtual ICollection<ArabaGaleri> ArabaGaleris { get; set; }
    }
}
