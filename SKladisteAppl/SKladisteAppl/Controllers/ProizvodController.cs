using SKladisteAppl.Data;
using Microsoft.AspNetCore.Mvc;
using SKladisteAppl.Models;
using SKladisteAppl.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Text;




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

     
      
        [HttpGet]
        [Route("trazi/{uvjet}")]
        public IActionResult TraziProizvod(string uvjet)
        {
            // ovdje će ići dohvaćanje u bazi

            if (uvjet == null || uvjet.Length < 3)
            {
                return BadRequest(ModelState);
            }

            // ivan se PROBLEM riješiti višestruke uvjete
            uvjet = uvjet.ToLower();
            try
            {
                IEnumerable<Proizvod> query = _context.Proizvodi;
                var niz = uvjet.Split(" ");

                foreach (var s in uvjet.Split(" "))
                {
                    query = query.Where(p => p.Naziv.ToLower().Contains(s)) ;
                }


                var proizvodi = query.ToList();

                return new JsonResult(_mapper.MapReadList(proizvodi));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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

        [HttpGet]
        [Route("traziStranicenje/{stranica}")]
        public IActionResult TraziProizvodStranicenje(int stranica, string uvjet = "")
        {
            var poStranici = 8;
            uvjet = uvjet.ToLower();
            try
            {
                var proizvodi = _context.Proizvodi
                    .Where(p => EF.Functions.Like(p.Naziv.ToLower(), "%" + uvjet + "%"))
                               
                    .Skip((poStranici * stranica) - poStranici)
                    .Take(poStranici)
                    
                    .ToList();


                return new JsonResult(_mapper.MapReadList(proizvodi));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        protected override void KontrolaBrisanje(Proizvod entitet)
        {
            var entitetIzbaze = _context.Proizvodi.Include(x => x.Izdatnice).FirstOrDefault(x => x.Sifra == entitet.Sifra);

            if (entitetIzbaze == null)
            {
                throw new Exception("Ne postoji proizvod s šifrom " + entitet.Sifra + " u bazi");
            }

            if (entitetIzbaze.Izdatnice != null && entitetIzbaze.Izdatnice.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Proizvod se ne može obrisati jer je postavljen na izdatnici: ");
                foreach (var e in entitetIzbaze.Izdatnice)
                {
                    sb.Append(e.BrojIzdatnice).Append(", ");
                }

                throw new Exception(sb.ToString().Substring(0, sb.ToString().Length - 2));
            }
        }

    }
}
