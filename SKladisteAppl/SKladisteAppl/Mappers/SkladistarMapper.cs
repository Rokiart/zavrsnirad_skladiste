using AutoMapper;
using SKladisteAppl.Models;

namespace SKladisteAppl.Mappers
{
    /// <summary>
    /// Klasa za mapiranje podataka o skladištarima.
    /// </summary>
    public class SkladistarMapper
    {
        /// <summary>
        /// Metoda za inicijalizaciju mapiranja iz entiteta Skladistar u DTO za čitanje.
        /// </summary>
        /// <returns>Mapper za mapiranje Skladistar u SkladistarDTORead</returns>
        public static Mapper InicijalizirajReadToDTO()
        {
            return new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<Skladistar, SkladistarDTORead>();
                })
            );
        }

        /// <summary>
        /// Metoda za inicijalizaciju mapiranja iz DTO za čitanje u entitet Skladistar.
        /// </summary>
        /// <returns>Mapper za mapiranje SkladistarDTORead u Skladistar</returns>
        public static Mapper InicijalizirajReadFromDTO()
        {
            return new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<SkladistarDTORead, Skladistar>();
                })
            );
        }

        /// <summary>
        /// Metoda za inicijalizaciju mapiranja iz entiteta Skladistar u DTO za unos i ažuriranje.
        /// </summary>
        /// <returns>Mapper za mapiranje Skladistar u SkladistarDTOInsertUpdate</returns>
        public static Mapper InicijalizirajInsertUpdateToDTO()
        {
            return new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<Skladistar, SkladistarDTOInsertUpdate>();
                })
            );
        }
    }
}
