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
