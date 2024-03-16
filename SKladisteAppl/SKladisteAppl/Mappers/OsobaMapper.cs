using AutoMapper;
using SKladisteAppl.Models;

namespace SKladisteAppl.Mappers
{
    /// <summary>
    /// Klasa za mapiranje podataka o osobama.
    /// </summary>
    public class OsobaMapper
    {
        /// <summary>
        /// Metoda za inicijalizaciju mapiranja iz entiteta Osoba u DTO za čitanje.
        /// </summary>
        /// <returns>Mapper za mapiranje Osoba u OsobaDTORead</returns>
        public static Mapper InicijalizirajReadToDTO()
        {
            return new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<Osoba, OsobaDTORead>();
                })
            );
        }

        /// <summary>
        /// Metoda za inicijalizaciju mapiranja iz DTO za čitanje u entitet Osoba.
        /// </summary>
        /// <returns>Mapper za mapiranje OsobaDTORead u Osoba</returns>
        public static Mapper InicijalizirajReadFromDTO()
        {
            return new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<OsobaDTORead, Osoba>();
                })
            );
        }

        /// <summary>
        /// Metoda za inicijalizaciju mapiranja iz entiteta Osoba u DTO za unos i ažuriranje.
        /// </summary>
        /// <returns>Mapper za mapiranje Osoba u OsobaDTOInsertUpdate</returns>
        public static Mapper InicijalizirajInsertUpdateToDTO()
        {
            return new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<Osoba, OsobaDTOInsertUpdate>();
                })
            );
        }
    }
}
