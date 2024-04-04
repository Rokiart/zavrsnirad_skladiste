using AutoMapper;
using SKladisteAppl.Models;

namespace SKladisteAppl.Mappers
{


    public class MappingIzdatniceProizvodi : Mapping<IzdatniceProizvodi, IzdatniceProizvodiDTORead, IzdatniceProizvodiDTOInsertUpdate>
    {
        public MappingIzdatniceProizvodi()
        {
            MapperMapReadToDTO = new Mapper(
            new MapperConfiguration(c =>
            {
                c.CreateMap<IzdatniceProizvodi, IzdatniceProizvodiDTORead>()
                .ConstructUsing(entitet =>
                 new IzdatniceProizvodiDTORead(
                    entitet.Sifra,
                    entitet.Proizvod == null ? "" : entitet.Proizvod.Naziv,
                    entitet.Proizvod.Sifraproizvoda == null ? "" : entitet.Proizvod.Sifraproizvoda,
                    entitet.Kolicina));

            })
            );
            MapperMapInsertUpdatedFromDTO = new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<IzdatniceProizvodiDTOInsertUpdate, IzdatniceProizvodi>();
                })
                );

            MapperMapMapInsertUpdateToDTO = new Mapper(
             new MapperConfiguration(c =>
             {
                 c.CreateMap<IzdatniceProizvodi, IzdatniceProizvodiDTOInsertUpdate>()
                 .ConstructUsing(entitet =>
                  new IzdatniceProizvodiDTOInsertUpdate(
                      entitet.Izdatnica == null ? null : entitet.Izdatnica.Sifra,
                    entitet.Proizvod == null ? null : entitet.Proizvod.Sifra,
                    entitet.Kolicina));

             })
             );
        }

        public Mapper MapperMapReadToDTO { get; }
        public Mapper MapperMapInsertUpdatedFromDTO { get; }
        public Mapper MapperMapMapInsertUpdateToDTO { get; }
    }

}
