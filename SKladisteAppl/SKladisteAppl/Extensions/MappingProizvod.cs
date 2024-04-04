using SKladisteAppl.Models;
using SKladisteAppl.Mappers;
namespace SKladisteAppl.Extensions
{

    /// <summary>
    /// 
    /// </summary>
    public static class MappingProizvod
    {

        public static List<ProizvodDTORead> MapProizvodReadList(this List<Proizvod> lista)
        {
            var mapper = ProizvodMapper.InicijalizirajReadToDTO();
            var vrati = new List<ProizvodDTORead>();
            lista.ForEach(e => {
                vrati.Add(mapper.Map<ProizvodDTORead>(e));
            });
            return vrati;
        }

        public static ProizvodDTORead MapProizvodReadToDTO(this Proizvod entitet)
        {
            var mapper = ProizvodMapper.InicijalizirajReadToDTO();
            return mapper.Map<ProizvodDTORead>(entitet);
        }

        public static ProizvodDTOInsertUpdate MapProizvodInsertUpdatedToDTO(this Proizvod entitet)
        {
            var mapper = ProizvodMapper.InicijalizirajInsertUpdateToDTO();
            return mapper.Map<ProizvodDTOInsertUpdate>(entitet);
        }

        public static Proizvod MapProizvodInsertUpdateFromDTO(this ProizvodDTOInsertUpdate dto, Proizvod entitet)
        {
            entitet.Naziv = dto.naziv;
            entitet.Sifraproizvoda = dto.sifraproizvoda;
            entitet.Mjernajedinica = dto.mjernajedinica;

            return entitet;
        }
    }
}