using SKladisteAppl.Data;
using SKladisteAppl.Models;
using Microsoft.AspNetCore.Mvc;
using SKladisteAppl.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Text;






namespace SKladisteAppl.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
public class IzdatnicaController : SkladisteController<Izdatnica, IzdatnicaDTORead, IzdatnicaDTOInsertUpdate>
{
   

    public IzdatnicaController(SkladisteContext context) : base(context)
    {
        DbSet = _context.Izdatnice;
        _mapper = new MappingIzdatnica();
    }




    [HttpGet]
    [Route("Proizvodi/{sifraIzdatnice:int}")]
    public IActionResult GetProizvodi(int sifraIzdatnice)
    {
        // kontrola ukoliko upit nije valjan
        if (!ModelState.IsValid || sifraIzdatnice <= 0)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var proizvodi = _context.IzdatniceProizvodi 
                .Include(i => i.Proizvod)
                .Include(i => i.Izdatnica)
                .Where(x => x.Izdatnica.Sifra == sifraIzdatnice).ToList();
            if (proizvodi == null)
            {
                return BadRequest("Ne postoji proizvod s šifrom " + sifraIzdatnice + " u bazi");
            }
            List<IzdatniceProizvodiDTORead> lista = new List<IzdatniceProizvodiDTORead>();
            proizvodi.ForEach(x => lista.Add(new IzdatniceProizvodiDTORead(x.Proizvod.Sifra, x.Proizvod.Naziv, x.Kolicina)));

            return new JsonResult(lista);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable,
                     ex.Message);
        }
    }





    [HttpPost]
    [Route("{sifra:int}/dodaj/{proizvodSifra:int}")]
    public IActionResult DodajProizvod(int sifra, int proizvodSifra)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (sifra <= 0 || proizvodSifra <= 0)
        {
            return BadRequest("Šifra izdatnice iliproizvoda nije dobra");
        }

        try
        {

            var izdatnica = _context.Izdatnice
                .FirstOrDefault(g => g.Sifra == sifra);

            if (izdatnica == null)
            {
                return BadRequest("Ne postoji izdatnica s šifrom " + sifra + " u bazi");
            }

            var proizvod = _context.Proizvodi.Find(proizvodSifra);

            if (proizvod == null)
            {
                return BadRequest("Ne postoji proizvod s šifrom " + proizvodSifra + " u bazi");
            }
            // Ovu količinu jedan kasnije omogući unos preko modala, za sada dodaje jedan proizvod
            var ip = new IzdatnicaProizvod() { Izdatnica = izdatnica, Proizvod = proizvod, Kolicina = 1 };


            _context.IzdatniceProizvodi.Add(ip);
            _context.SaveChanges();

            return Ok();

        }
        catch (Exception ex)
        {
            return StatusCode(
                   StatusCodes.Status503ServiceUnavailable,
                   ex.Message);

        }

    }



    [HttpDelete]
    [Route("ObrisiProizvod/{sifraIzdatnice:int}/{proizvodSifra:int}")]
    public IActionResult ObrisiProizvod(int sifraIzdatnice, int proizvodSifra)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (sifraIzdatnice <= 0  || proizvodSifra <= 0)
        {
            return BadRequest("Šifra izdatnice ili proizvoda nije dobra");
        }

        try
        {


            var izdatnica = _context.IzdatniceProizvodi
                    .Include(g => g.Proizvod)
                    .Include(g => g.Izdatnica)
                    .FirstOrDefault(g => g.Izdatnica.Sifra == sifraIzdatnice && g.Proizvod.Sifra==proizvodSifra);

            if (izdatnica == null)
            {
                return BadRequest("Ne postoji proizvod s šifrom " + proizvodSifra + " izdatnica  s šifrom " + sifraIzdatnice + " u bazi");
            }

            _context.IzdatniceProizvodi.Remove(izdatnica);
            _context.SaveChanges();

            return Ok();

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);

        }

    }


    [HttpPatch]
    [Route("PromjeniProizvod/{sifra:int}")]
    public IActionResult PromjeniProizvod(int sifra, int Kolicina)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {

            var entitet = _context.IzdatniceProizvodi.Include(x => x.Proizvod).FirstOrDefault(x => x.Sifra == sifra);

            if (entitet == null)
            {
                return BadRequest("Ne postoji proizvod na na izdatnici s šifrom " + sifra + " u bazi");
            }

            entitet.Kolicina = Kolicina;

            _context.IzdatniceProizvodi.Update(entitet);
            _context.SaveChanges();

            return new JsonResult(new IzdatniceProizvodiDTORead(entitet.Sifra, entitet.Proizvod.Naziv, entitet.Kolicina));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }


    protected override void KontrolaBrisanje(Izdatnica entitet)
    {
        var lista = _context.Izdatnice;
        if (lista != null && lista.Count() > 0)
        { 
            StringBuilder sb = new StringBuilder();
            sb.Append("Izdatnica se ne može obrisati jer su na njoj proizvodi: ");
            foreach (var e in lista)
            {
                sb.Append(e.BrojIzdatnice).Append(", ");
            }

            throw new Exception(sb.ToString().Substring(0, sb.ToString().Length - 2));
        }
    }

    protected override Izdatnica KreirajEntitet(IzdatnicaDTOInsertUpdate dto)
    {
        var osoba = _context.Osobe.Find(dto.osobaSifra) ?? throw new Exception("Ne postoji osoba s šifrom " + dto.osobaSifra + " u bazi");
        var skladistar = _context.Skladistari.Find(dto.skladistarSifra) ?? throw new Exception("Ne postoji skladistar s šifrom " + dto.skladistarSifra + " u bazi");
        var entitet = _mapper.MapInsertUpdatedFromDTO(dto);
        entitet.IzdatniceProizvodi = new List<IzdatnicaProizvod>();
        entitet.Osoba = osoba;
        entitet.Skladistar = skladistar;
        return entitet;
    }

    protected override List<IzdatnicaDTORead> UcitajSve()
    {
        var lista = _context.Izdatnice  
                .Include(g => g.Osoba)
                .Include(g => g.Skladistar)
                .Include(g => g.IzdatniceProizvodi)
                .ThenInclude(g=>g.Proizvod)
                .ToList();
        if (lista == null || lista.Count == 0)
        {
            throw new Exception("Ne postoje podaci u bazi");
        }

        var dtoLista = new List<IzdatnicaDTORead>();
        foreach ( var item in lista)
        {
            var LPNI = new List<ProizvodNaIzdatniciDTORead>();
            foreach(var p in item.IzdatniceProizvodi)
            {
                LPNI.Add(new ProizvodNaIzdatniciDTORead(p.Proizvod.Naziv, p.Kolicina));
                dtoLista.Add(new IzdatnicaDTORead(
                item.Sifra,
                    item.BrojIzdatnice,
                item.Datum,
                item.Osoba == null ? "" : (item.Osoba.Ime
                        + " " + item.Osoba.Prezime).Trim(),
                item.Skladistar == null ? "" : (item.Skladistar.Ime
                        + " " + item.Skladistar.Prezime).Trim(),
               
                LPNI,

                    item.Napomena
                    ));
            }
        }

        return dtoLista;
    }

    protected override Izdatnica NadiEntitet(int sifra)
    {
        return _context.Izdatnice.Include(i => i.Osoba).Include(i => i.Skladistar)
                .Include(i => i.IzdatniceProizvodi).FirstOrDefault(x => x.Sifra == sifra) ?? throw new Exception("Ne postoji izdatnica s šifrom " + sifra + " u bazi");
    }



    protected override Izdatnica PromjeniEntitet(IzdatnicaDTOInsertUpdate dto, Izdatnica entitet)
    {
        var osoba = _context.Osobe.Find(dto.osobaSifra) ?? throw new Exception("Ne postoji osoba s šifrom " + dto.osobaSifra + " u bazi");
        var skladistar = _context.Skladistari.Find(dto.skladistarSifra) ?? throw new Exception("Ne postoji skladištar s šifrom " + dto.skladistarSifra + " u bazi");


        /*
        List<Proizvod> proizvodi = entitet.Proizvodi;
        entitet = _mapper.MapInsertUpdatedFromDTO(dto);
        entitet.Proizvodi = proizvodi;
        */

        // ovdje je možda pametnije ići s ručnim mapiranje
        entitet.BrojIzdatnice = dto.brojizdatnice;
        entitet.Datum = dto.datum;
        entitet.Napomena = dto.napomena;
        entitet.Osoba = osoba;
        entitet.Skladistar = skladistar;

        return entitet;
    }

//    [HttpGet]
//    [Route("Proizvodi/{sifraIzdatnica:int}")]
//    public IActionResult GetProizvodi(int sifraIzdatnica)
//    {
//        // kontrola ukoliko upit nije valjan
//        if (!ModelState.IsValid || sifraIzdatnica <= 0)
//        {
//            return BadRequest(ModelState);
//        }
//        try
//        {
//            var p = _context.Izdatnice
//                     .Include(i => i.Proizvodi).FirstOrDefault(x => x.Sifra == sifraIzdatnica);
//            if (p == null)
//            {
//                return BadRequest("Ne postoje Izdatnica s šifrom " + sifraIzdatnica + " u bazi");
//            }

//            //    // nisam radio posebno mapper
//            //    List<IzdatniceProizvodiDTORead> lista = new List<IzdatniceProizvodiDTORead>();
//            //    kolicine.ForEach(x => lista.Add(new IzdatniceProizvodiDTORead(x.Sifra, x.Proizvod.Naziv, x.Kolicina)));

//            //    return new JsonResult(lista);
//            //}
//            //catch (Exception ex)
//            //{
//            //    return StatusCode(StatusCodes.Status503ServiceUnavailable,
//            //        ex.Message);
//        }
//    }


//    [HttpPost]
//    [Route("DodajKolicinu")]
//    public IActionResult DodajKolicinu(IzdatniceProizvodiDTOInsertUpdate dto)
//    {
//        if (!ModelState.IsValid)
//        {
//            return BadRequest(ModelState);
//        }
//        try
//        {
//            var izdatnica = _context.Izdatnice.Find(dto.izdatnicaSifra);

//            if (izdatnica == null)
//            {
//                throw new Exception("Ne postoji izdatnica s šifrom " + dto.izdatnicaSifra + " u bazi");
//            }

//            var proizvod = _context.Proizvodi.Find(dto.proizvodSifra);

//            if (proizvod == null)
//            {
//                throw new Exception("Ne postoji proizvod s šifrom " + dto.proizvodSifra + " u bazi");
//            }

//            var entitet = new IzdatniceProizvodi() { Izdatnica = izdatnica, Proizvod = proizvod, kolicina = dto.kolicina };

//            _context.IzdatniceProizvodi.Add(entitet);
//            _context.SaveChanges();

//            return new JsonResult(new IzdatniceProizvodiDTORead(entitet.Sifra, entitet.Proizvod.Naziv, entitet.kolicina));
//        }
//        catch (Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }

//    }



//    [HttpDelete]
//    [Route("ObrisiProizvod/{sifra:int}")]
//    public IActionResult ObrisiProizvod(int sifra)
//    {

//        if (!ModelState.IsValid)
//        {
//            return BadRequest(ModelState);
//        }

//        if (sifra <= 0)
//        {
//            return BadRequest("Šifra proizvoda nije dobra");
//        }

//        try
//        {

//            var entitet = _context.IzdatniceProizvodi.Find(sifra);

//            if (entitet == null)
//            {
//                return BadRequest("Ne postoji proizvod na izdatnici s šifrom " + sifra + " u bazi");
//            }

//            _context.IzdatniceProizvodi.Remove(entitet);
//            _context.SaveChanges();

//            return Ok("Obrisano");

//        }
//        catch (Exception ex)
//        {
//            return BadRequest(ex.Message);

//        }

//    }

//    [HttpPatch]
//    [Route("PromjeniProizvod/{sifra:int}")]
//    public IActionResult PromjeniProizvod(int sifra, int kolicina)
//    {
//        if (!ModelState.IsValid)
//        {
//            return BadRequest(ModelState);
//        }
//        try
//        {

//            var entitet = _context.IzdatniceProizvodi.Include(x => x.Proizvod).FirstOrDefault(x => x.Sifra == sifra);

//            if (entitet == null)
//            {
//                return BadRequest("Ne postoji proizvod na izdatnici s šifrom " + sifra + " u bazi");
//            }

//            entitet.kolicina = kolicina;

//            _context.IzdatniceProizvodi.Update(entitet);
//            _context.SaveChanges();

//            return new JsonResult(new IzdatniceProizvodDTORead(entitet.Sifra, entitet.Proizvod.Naziv, entitet.kolicina));
//        }
//        catch (Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }

//    }
///// <inheritdoc/>


//    //protected override void KontrolaBrisanje(Izdatnica entitet)
//    //{
//    //    var lista = _context.Izdatnica.Include(x => x.Proizvod).Where(x => x.Proizvod.Sifra == entitet.Sifra).ToList();

//    //    if (lista != null && lista.Count() > 0)
//    //    {
//    //        StringBuilder sb = new StringBuilder();
//    //        sb.Append("Izdatnica se ne može obrisati jer je postavljen sa proizvodima: ");
//    //        foreach (var e in lista)
//    //        {
//    //            sb.Append(e.Naziv).Append(", ");
//    //        }

//    //        throw new Exception(sb.ToString().Substring(0, sb.ToString().Length - 2));
//    //    }
//    //}
}

