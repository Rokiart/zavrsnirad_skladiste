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
            var lista = _context.IzdatniceProizvodi
                .Include(x => x.Proizvod)
                .Include(x => x.Izdatnica)
                .Where(x => x.Proizvod.Sifra == entitet.Sifra).ToList();
           
            

            if (lista != null && lista.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Proizvod se ne može obrisati jer je postavljen na izdatnici: ");
                foreach (var e in lista)
                {
                    sb.Append(e.Izdatnica ).Append(", ");
                }

                throw new Exception(sb.ToString().Substring(0, sb.ToString().Length - 2));
            }
        }

    }
}
