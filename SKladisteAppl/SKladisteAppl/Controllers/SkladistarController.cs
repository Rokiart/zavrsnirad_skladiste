using Microsoft.AspNetCore.Mvc;
using SKladisteAppl.Data;
using SKladisteAppl.Models;

using System.Text;
using SKladisteAppl.Controllers;
using SKladisteAppl.Mappers;
using System.Data.Entity;

namespace SKladisteAppl.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD operacije nad entitetom skladistar u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SkladistarController : SkladisteController<Skladistar, SkladistarDTORead, SkladistarDTOInsertUpdate>
    {
        public SkladistarController(SkladisteContext context) : base(context)
        {
            DbSet = _context.Skladistari;
            _mapper = new MappingSkladistar();
        }

        protected override void KontrolaBrisanje(Skladistar entitet)
        {
            var lista = _context.Izdatnice.Include(x => x.Skladistar).Where(x => x.Skladistar.Sifra == entitet.Sifra).ToList();

            if (lista != null && lista.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Skladistar se ne može obrisati jer je postavljen na izdatnici: ");
                foreach (var e in lista)
                {
                    sb.Append(e.BrojIzdatnice).Append(", ");
                }

                throw new Exception(sb.ToString().Substring(0, sb.ToString().Length - 2));
            }
        }

        [HttpPatch]
        [Route("{sifraSkladistar:int}")]
        public async Task<ActionResult> Patch(int sifraSkladistar, IFormFile datoteka)
        {
            if (datoteka == null)
            {
                return BadRequest("Datoteka nije postavljena");
            }

            var entitetIzbaze = _context.Skladistari.Find(sifraSkladistar);

            if (entitetIzbaze == null)
            {
                return BadRequest("Ne postoji skladistar s šifrom " + sifraSkladistar + " u bazi");
            }
            try
            {
                var ds = Path.DirectorySeparatorChar;
                string dir = Path.Combine(Directory.GetCurrentDirectory()
                    + ds + "wwwroot" + ds + "datoteke" + ds + "skladistari");
                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
                var putanja = Path.Combine(dir + ds + sifraSkladistar + "_" + System.IO.Path.GetExtension(datoteka.FileName));
                Stream fileStream = new FileStream(putanja, FileMode.Create);
                await datoteka.CopyToAsync(fileStream);
                return Ok("Datoteka pohranjena");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

