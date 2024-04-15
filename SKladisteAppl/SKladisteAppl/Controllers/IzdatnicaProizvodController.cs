//using SKladisteAppl.Data;
//using SKladisteAppl.Models;
//using System.Data.Entity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Text;
//using SKladisteAppl.Mappers;



//namespace SKladisteAppl.Controllers
//{
//    [ApiController]
//    [Route("api/v1/[controller]")]

//    public class IzdatnicaProizvodController : SkladisteController<IzdatnicaProizvod, IzdatniceProizvodiDTORead, IzdatniceProizvodiDTOInsertUpdate>
//    {
      

//        public IzdatnicaProizvodController(SkladisteContext context) : base(context)
//        {
//            DbSet = _context.IzdatniceProizvodi;
//            _mapper = new MappingIzdatnicaProizvod();
//        }

       

//        //protected override void KontrolaBrisanje(IzdatnicaProizvod entitet)
//        //{
//        //    var lista = _context.IzdatniceProizvodi.Include(x => x.Proizvodi).Where(x => x.Proizvodi.Sifra == entitet.Sifra).ToList();



//        //    if (lista != null && lista.Count() > 0)
//        //    {
//        //        StringBuilder sb = new StringBuilder();
//        //        sb.Append("Proizvod se ne može obrisati jer je postavljen na izdatnici: ");
//        //        foreach (var e in lista)
//        //        {
//        //            sb.Append(e.Kolicina).Append(", ");
//        //        }

//        //        throw new Exception(sb.ToString().Substring(0, sb.ToString().Length - 2));
//        //    }
//        //}
//    }
//}
