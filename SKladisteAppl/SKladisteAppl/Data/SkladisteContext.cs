using SKladisteAppl.Models;
using Microsoft.EntityFrameworkCore;


namespace SKladisteAppl.Data
{
    
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
            /// Skladištari u bazi
            /// </summary>
            public DbSet<Skladistar> Skladistari{ get; set; }


        }

    
}
