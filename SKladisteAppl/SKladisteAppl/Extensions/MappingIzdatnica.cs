using SKladisteAppl.Mappers;
using SKladisteAppl.Models;
using AutoMapper;

namespace SKladisteAppl.Extensions
{
    public static class MappingIzdatnica
    {


        public static List<IzdatnicaDTORead> MapIzdatnicaReadList(this List<Izdatnica> lista)
        {
            var mapper = IzdatnicaMapper.InicijalizirajReadToDTO();
            var vrati = new List<IzdatnicaDTORead>();
            lista.ForEach(e => {
                vrati.Add(mapper.Map<IzdatnicaDTORead>(e));
            });
            return vrati;
        }

        public static IzdatnicaDTORead MapIzdatnicaReadToDTO(this Izdatnica e)
        {
            var mapper = IzdatnicaMapper.InicijalizirajReadToDTO();
            return mapper.Map<IzdatnicaDTORead>(e);
        }

        public static IzdatnicaDTOInsertUpdate MapIzdatnicaInsertUpdatedToDTO(this Izdatnica e)
        {

            var mapper = IzdatnicaMapper.InicijalizirajInsertUpdateToDTO();
            return mapper.Map<IzdatnicaDTOInsertUpdate>(e);
        }


        public static Izdatnica MapIzdatnicaInsertUpdateFromDTO(this IzdatnicaDTOInsertUpdate dto, Izdatnica entitet)
        {
            entitet.BrojIzdatnice = dto.brojizdatnice;
            entitet.Datum = dto.datum;
            entitet.Napomena = dto.napomena;

            return entitet;
        }
    }


}