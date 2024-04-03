using SKladisteAppl.Data;
using Microsoft.AspNetCore.Mvc;
using SKladisteAppl.Models;
using Microsoft.Data.SqlClient;
using SKladisteAppl.Extensions;
using System.Text;
using EdunovaAPP.Controllers;
using System.Data.Entity;

namespace SKladisteAppl.Controllers
{
    public class ProizvodController : SkladisteController<Proizvod, ProizvodDTORead, ProizvodDTOInsertUpdate>
    {
        public ProizvodController(SkladisteContext context) : base(context)
        {
            DbSet = _context.Proizvodi;
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
                    query = query.Where(p => p.Naziv.ToLower().Contains(s) );
                }


                var polaznici = query.ToList();

                return new JsonResult(_mapper.MapReadList(polaznici)); //200

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
