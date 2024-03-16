using SKladisteAppl.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;


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
            /// Skladistari u bazi
            /// </summary>

            public DbSet<Skladistar> Skladistari{ get; set; }

       
        /// <summary>
        /// Proizvodi u bazi
        /// </summary>

        public DbSet<Proizvod> Proizvodi { get; set; }
        /// <summary>
        /// Izdatnice u bazi
        /// </summary>
        public DbSet<Izdatnica> Izdatnice{ get; set; }
        /// <summary>
        /// Implementacije veza
        /// </summary>
        /// <param name="modelBuilder"></param>


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // implementacija veze 1:n
            modelBuilder.Entity<Izdatnica>().HasOne(i => i.Osoba);
            modelBuilder.Entity<Izdatnica>().HasOne(i => i.Skladistar);

            // implementacija veze n:n
            modelBuilder.Entity<Izdatnica>()
                 .HasMany(i => i.Proizvodi)
                .WithMany(i => i.Izdatnice)
                .UsingEntity<Dictionary<string, object>>("clanovi",
                c => c.HasOne<Proizvod>().WithMany().HasForeignKey("proizvod"),
                c => c.HasOne<Izdatnica>().WithMany().HasForeignKey("izdatnica"),
                c => c.ToTable("clanovi")
                );


        }

         


    }

    
}
