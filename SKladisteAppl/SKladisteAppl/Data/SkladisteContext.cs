using SKladisteAppl.Models;
using Microsoft.EntityFrameworkCore;


namespace SKladisteAppl.Data
{
    /// <summary>
    /// Ovo mi je datoteka gdje ću navoditi datasetove i načine spajanja u bazi
    /// </summary>

    public class SkladisteContext : DbContext
    {
            /// <summary>
            /// Kostruktor
            /// </summary>
            /// <param name="options"></param>
            public SkladisteContext(DbContextOptions<SkladisteContext> options)
                : base(options)
            {

            }

            /// <summary>
            /// Osobe u bazi
            /// </summary>
            public DbSet<Osoba> Osobe{ get; set; }
           
            /// <summary>
            /// Proizvodi u bazi
            /// </summary>

            public DbSet<Proizvod> Proizvodi{ get; set; }

            /// <summary>
            /// Skladistari u bazi
            /// </summary>

            public DbSet<Skladistar> Skladistari{ get; set; }

            /// <summary>
            /// Izdatnice u bazi
            /// </summary>
            public DbSet<Izdatnica> Izdatnice{ get; set; }

            /// <summary>
            /// IzdatniceProizvodi u bazi
            /// </summary>
            //public DbSet<IzdatnicaProizvodi> IzdatniceProizvodi{ get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}


    }

    
}
