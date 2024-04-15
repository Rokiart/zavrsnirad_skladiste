using SKladisteAppl.Data;
using Microsoft.AspNetCore.Mvc;
using SKladisteAppl.Models;
using Microsoft.Data.SqlClient;
using SKladisteAppl.Extensions;
using System.Text;
using SKladisteAppl.Controllers;
using System.Data.Entity;
using SKladisteAppl.Mappers;

namespace SKladisteAppl.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class ProizvodController : SkladisteController<Proizvod, ProizvodDTORead, ProizvodDTOInsertUpdate>
    {
        public ProizvodController(SkladisteContext context) : base(context)
        {
            DbSet = _context.Proizvodi;
            _mapper = new MappingProizvod();
        }


        [HttpPut]
        [Route("postaviSliku/{sifra:int}")]
        public IActionResult PostaviSliku(int sifra, SlikaDTO slika)
        {
            if (sifra <= 0)
            {
                return BadRequest("Šifra mora biti veća od nula (0)");
            }
            if (slika.Base64 == null || slika.Base64?.Length == 0)
            {
                return BadRequest("Slika nije postavljena");
            }
            var p = _context.Proizvodi.Find(sifra);
            if (p == null)
            {
                return BadRequest("Ne postoji proizvod s šifrom " + sifra + ".");
            }
            try
            {
                var ds = Path.DirectorySeparatorChar;
                string dir = Path.Combine(Directory.GetCurrentDirectory()
                    + ds + "wwwroot" + ds + "slike" + ds + "proizvodi");

                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
                var putanja = Path.Combine(dir + ds + sifra + ".png");
                System.IO.File.WriteAllBytes(putanja, Convert.FromBase64String(slika.Base64));
                return Ok("Uspješno pohranjena slika");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        protected override void KontrolaBrisanje(Proizvod entitet)
        {
            var lista = _context.IzdatniceProizvodi.Include(x => x.Proizvod).Where(x => x.Proizvod.Sifra == entitet.Sifra).ToList();



            if (lista != null && lista.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Proizvod se ne može obrisati jer je postavljen na izdatnici: ");
                foreach (var e in lista)
                {
                    sb.Append(e.Kolicina).Append(", ");
                }

                throw new Exception(sb.ToString().Substring(0, sb.ToString().Length - 2));
            }
        }

    }
}
