using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace Araba_Otomasyonu_1
{
    internal class ArabaDbContext:DbContext
    {
        public ArabaDbContext():base("name=ArabaDbContext")
        {

        }
        public DbSet<Araba> Arabas { get; set; }
        public DbSet<Galeri> Galeris { get; set; }
        public DbSet<Satis> Satiss { get; set; }
        public DbSet<ArabaGaleri> ArabaGaleris { get; set; }




    }
}
