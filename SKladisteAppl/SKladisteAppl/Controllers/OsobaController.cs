using SKladisteAppl.Data;
using SKladisteAppl.Extensions;
using SKladisteAppl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Text;
using SKladisteAppl.Controllers;
using System.Data.Entity;
using SKladisteAppl.Mappers;


namespace SKladisteAppl.Controllers
{

    /// <summary>
    /// Namjenjeno za CRUD operacije nad entitetom osoba u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OsobaController : SkladisteController<Osoba, OsobaDTORead, OsobaDTOInsertUpdate>
    {
        public OsobaController(SkladisteContext context) : base(context)
        {
            DbSet = _context.Osobe;
            _mapper = new MappingOsoba();

        }

        protected override void KontrolaBrisanje(Osoba entitet)
        {
            var lista = _context.Izdatnice.Include(x => x.Osoba).Where(x => x.Osoba.Sifra == entitet.Sifra).ToList();

            if (lista != null && lista.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Osoba se ne može obrisati jer je postavljena na Izdatnici: ");
                foreach (var e in lista)
                {
                    sb.Append(e.BrojIzdatnice).Append(", ");
                }

                throw new Exception(sb.ToString().Substring(0, sb.ToString().Length - 2));
            }
        }
    }

}
