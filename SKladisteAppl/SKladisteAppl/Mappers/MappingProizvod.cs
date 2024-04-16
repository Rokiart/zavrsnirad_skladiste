using AutoMapper;
using SKladisteAppl.Models;

namespace SKladisteAppl.Mappers
{
    public class MappingProizvod : Mapping<Proizvod, ProizvodDTORead, ProizvodDTOInsertUpdate>
    {
       

        public MappingProizvod()
        {
            MapperMapReadToDTO = new Mapper(
            new MapperConfiguration(c =>
            {
                c.CreateMap<Proizvod, ProizvodDTORead > ()
                .ConstructUsing(entitet =>
                 new ProizvodDTORead(
                    entitet.Sifra,
                    entitet.Naziv,
                    entitet.Sifraproizvoda,
                    entitet.Mjernajedinica,
                    PutanjaDatoteke(entitet)));
            })
            );

            MapperMapInsertUpdateToDTO = new Mapper(
              new MapperConfiguration(c =>
              {
                  c.CreateMap<Proizvod, ProizvodDTOInsertUpdate>()
               .ConstructUsing(entitet =>
                new ProizvodDTOInsertUpdate(
                   entitet.Naziv,
                   entitet.Sifraproizvoda,
                   entitet.Mjernajedinica,
                   PutanjaDatoteke(entitet)));
              })
              );
        }


        private static string PutanjaDatoteke(Proizvod e)
        {
            try
            {
                var ds = Path.DirectorySeparatorChar;
                string slika = Path.Combine(Directory.GetCurrentDirectory()
                    + ds + "wwwroot" + ds + "slike" + ds + "proizvodi" + ds + e.Sifra + ".png");
                return File.Exists(slika) ? "/slike/proizvodi/" + e.Sifra + ".png" : null;
            }
            catch
            {
                return null;
            }

        }



    }
}

