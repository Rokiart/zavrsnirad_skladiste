using SKladisteAppl.Mappers;
using SKladisteAppl.Models;

namespace SKladisteAppl.Extensions
{
    /// <summary>
    /// Mapiranje osoba
    /// </summary>
    public static class MappingOsoba
    {
        /// <summary>
        /// mapiranje liste
        /// </summary>
        /// <param name="lista"></param>
        /// <returns></returns>

        public static List<OsobaDTORead> MapOsobaReadList(this List<Osoba> lista)
        {
            var mapper = OsobaMapper.InicijalizirajReadToDTO();
            var vrati = new List<OsobaDTORead>();
            lista.ForEach(e => {
                vrati.Add(mapper.Map<OsobaDTORead>(e));
            });
            return vrati;
        }
        /// <summary>
        /// mapiranje entiteta
        /// </summary>
        /// <param name="entitet"></param>
        /// <returns></returns>

        public static OsobaDTORead MapOsobaReadToDTO(this Osoba entitet)
        {
            var mapper = OsobaMapper.InicijalizirajReadToDTO();
            return mapper.Map<OsobaDTORead>(entitet);
        }

        public static OsobaDTOInsertUpdate MapOsobaInsertUpdatedToDTO(this Osoba entitet)
        {
            var mapper = OsobaMapper.InicijalizirajInsertUpdateToDTO();
            return mapper.Map<OsobaDTOInsertUpdate>(entitet);
        }
        /// <summary>
        /// mapiranje osobe
        /// </summary>
        /// <param name="entitet"></param>
        /// <returns></returns>
        public static Osoba MapOsobaInsertUpdateFromDTO(this OsobaDTOInsertUpdate dto, Osoba entitet)
        {
            entitet.Ime = dto.ime;
            entitet.Prezime = dto.prezime;
            entitet.BrojTelefona = dto.brojtelefona;
            entitet.Email = dto.email;
            return entitet;

        }

    }
}