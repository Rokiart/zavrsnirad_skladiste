using AutoMapper;
using SKladisteAppl.Models;

namespace SKladisteAppl.Mappers
{
    public class MappingOsoba : Mapping<Osoba, OsobaDTORead, OsobaDTOInsertUpdate>
    {
        public MappingOsoba()
        {
            MapperMapReadToDTO = new Mapper(
            new MapperConfiguration(c =>
            {
                c.CreateMap<Osoba,OsobaDTORead>()
                .ConstructUsing(entitet =>
                 new OsobaDTORead(
                    entitet.Sifra,
                    entitet.Ime,
                    entitet.Prezime,
                    entitet.BrojTelefona,
                    entitet.Email
                   ));
            })
            );

            MapperMapInsertUpdateToDTO = new Mapper(
             new MapperConfiguration(c =>
             {
                 c.CreateMap<Osoba, OsobaDTOInsertUpdate>()
              .ConstructUsing(entitet =>
               new OsobaDTOInsertUpdate(
                  entitet.Ime,
                  entitet.Prezime,
                  entitet.BrojTelefona,
                  entitet.Email

                  ));
             })
             );
        }


      

    }
}
