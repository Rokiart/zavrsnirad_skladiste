using AutoMapper;
using SKladisteAppl.Models;

namespace SKladisteAppl.Mappers
{
    public class MappingSkladistar : Mapping<Skladistar, SkladistarDTORead, SkladistarDTOInsertUpdate>
    {
        public MappingSkladistar()
        {
            MapperMapReadToDTO = new Mapper(
            new MapperConfiguration(c =>
            {
                c.CreateMap<Skladistar, SkladistarDTORead>()
                .ConstructUsing(entitet =>
                 new SkladistarDTORead(
                    entitet.Sifra,
                    entitet.Ime,
                    entitet.Prezime,
                    entitet.BrojTelefona,
                    entitet.Email,
                    PutanjaDatoteke(entitet)));



            })
            );
        }
        private static string PutanjaDatoteke(Skladistar e)
        {
            try
            {
                var ds = Path.DirectorySeparatorChar;
                string dir = Path.Combine(Directory.GetCurrentDirectory()
                    + ds + "wwwroot" + ds + "datoteke" + ds + "skladistari" + ds);
                DirectoryInfo d = new DirectoryInfo(dir);
                FileInfo[] Files = d.GetFiles(e.Sifra + "_*"); // dohvati sve koji počinju s šifra_ 
                return Files != null && Files.Length > 0 ? "/datoteke/skladistari/" + Files[0].Name : null;
            }
            catch
            {
                return null;
            }

        }
    }
}
