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

            //MapperMapInsertUpdateToDTO = new Mapper(
            // new MapperConfiguration(c =>
            // {
            //     c.CreateMap<Osoba, OsobaDTOInsertUpdate>()
            //  .ConstructUsing(entitet =>
            //   new OsobaDTOInsertUpdate(
            //      entitet.Ime,
            //      entitet.Prezime,
            //      entitet.BrojTelefona,
            //      entitet.Email

            //      ));
            // })
            // );
        
       
        }


    }
}
