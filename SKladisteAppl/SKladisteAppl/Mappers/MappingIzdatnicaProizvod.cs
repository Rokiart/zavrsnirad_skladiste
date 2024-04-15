using AutoMapper;
using SKladisteAppl.Models;

namespace SKladisteAppl.Mappers
{
    public class MappingIzdatnicaProizvod : Mapping<IzdatnicaProizvod, IzdatniceProizvodiDTORead, IzdatniceProizvodiDTOInsertUpdate>
    {
        public MappingIzdatnicaProizvod()
        {
            MapperMapReadToDTO = new Mapper(
            new MapperConfiguration(c =>
            {
                c.CreateMap<IzdatnicaProizvod, IzdatniceProizvodiDTORead>()
                .ConstructUsing(entitet =>
                 new IzdatniceProizvodiDTORead(
                    entitet.Sifra,
                    entitet.Kolicina
                                     ));
            })
            );

           
        
       
        }


    }
}
