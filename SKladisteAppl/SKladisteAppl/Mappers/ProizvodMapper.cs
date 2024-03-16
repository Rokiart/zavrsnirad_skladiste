using AutoMapper;
using SKladisteAppl.Models;

namespace SKladisteAppl.Mappers
{
    /// <summary>
    /// Klasa za mapiranje podataka o proizvodima.
    /// </summary>
    public class ProizvodMapper
    {
        /// <summary>
        /// Metoda za inicijalizaciju mapiranja iz entiteta Proizvod u DTO za čitanje.
        /// </summary>
        /// <returns>Mapper za mapiranje Proizvod u ProizvodDTORead</returns>
        public static Mapper InicijalizirajReadToDTO()
        {
            return new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<Proizvod, ProizvodDTORead>();
                })
            );
        }

        /// <summary>
        /// Metoda za inicijalizaciju mapiranja iz DTO za čitanje u entitet Proizvod.
        /// </summary>
        /// <returns>Mapper za mapiranje ProizvodDTORead u Proizvod</returns>
        public static Mapper InicijalizirajReadFromDTO()
        {
            return new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<ProizvodDTORead, Proizvod>();
                })
            );
        }

        /// <summary>
        /// Metoda za inicijalizaciju mapiranja iz entiteta Proizvod u DTO za unos i ažuriranje.
        /// </summary>
        /// <returns>Mapper za mapiranje Proizvod u ProizvodDTOInsertUpdate</returns>
        public static Mapper InicijalizirajInsertUpdateToDTO()
        {
            return new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<Proizvod, ProizvodDTOInsertUpdate>();
                })
            );
        }
    }
}
