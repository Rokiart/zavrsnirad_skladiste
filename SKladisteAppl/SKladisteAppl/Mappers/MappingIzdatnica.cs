using AutoMapper;
using SKladisteAppl.Models;

namespace SKladisteAppl.Mappers
{
    public class MappingIzdatnica : Mapping<Izdatnica, IzdatnicaDTORead, IzdatnicaDTOInsertUpdate>
    {
        public MappingIzdatnica()
        {
            MapperMapReadToDTO = new Mapper(
            new MapperConfiguration(c =>
            {
                c.CreateMap<Izdatnica, IzdatnicaDTORead>()
                .ConstructUsing(entitet =>
                 new IzdatnicaDTORead(
                    entitet.Sifra,
                    entitet.BrojIzdatnice,
                    entitet.Datum,
                    entitet.Osoba == null ? "" : ( entitet.Osoba.Ime
                        + " " + entitet.Osoba.Prezime).Trim(),
                    entitet.Skladistar == null ? "" : (entitet.Skladistar.Ime
                        + " " + entitet.Skladistar.Prezime).Trim(),
                    
                    null,
                  

                    entitet.Napomena));
            })
            );
            MapperMapInsertUpdatedFromDTO = new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<IzdatnicaDTOInsertUpdate, Izdatnica>();
                })
                );

            MapperMapInsertUpdateToDTO = new Mapper(
             new MapperConfiguration(c =>
             {
                 c.CreateMap<Izdatnica, IzdatnicaDTOInsertUpdate>()
                 .ConstructUsing(entitet =>
                  new IzdatnicaDTOInsertUpdate(
                     entitet.BrojIzdatnice,
                     entitet.Datum,
                     entitet.Osoba == null ? null : entitet.Osoba.Sifra,
                     entitet.Skladistar == null ? null : entitet.Skladistar.Sifra,
                    
                     entitet.Napomena))
                 ;
             })
             );
        }

       
    }
}
