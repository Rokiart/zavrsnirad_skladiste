using Microsoft.AspNetCore.Mvc;
using SKladisteAppl.Data;
using SKladisteAppl.Models;

using System.Text;
using EdunovaAPP.Controllers;
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
    }
}

