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
            /// Smjerovi u bazi
            /// </summary>
            public DbSet<Osoba> Osobe{ get; set; }
        }

    
}
