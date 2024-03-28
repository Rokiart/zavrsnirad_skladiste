using SKladisteAppl.Data;
using SKladisteAppl.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using SKladisteAppl.Extensions;
using IzdatnicaDTOInsertUpdate = SKladisteAppl.Models.IzdatnicaDTOInsertUpdate;



namespace SKladisteAppl.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD operacije nad entitetom izdatnice u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class IzdatnicaController : ControllerBase
    {
        /// <summary>
        /// Kontest za rad s bazom koji će biti postavljen s pomoću Dependecy Injection-om
        /// </summary>
        private readonly SkladisteContext _context;
        /// <summary>
        /// Konstruktor klase koja prima Skladiste kontext
        /// pomoću DI principa
        /// </summary>
        /// <param name="context"></param>
        public IzdatnicaController(SkladisteContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Prikazuje sve izdatnice iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita
        /// 
        ///    GET api/v1/Izdatnica
        ///    
        /// </remarks>
        /// <returns>Izdatnice u bazi</returns>
        /// <response code="200">Sve OK, ako nema podataka content-length: 0 </response>
        /// <response code="400">Zahtjev nije valjan</response>
        /// <response code="503">Baza na koju se spajam nije dostupna</response>
        [HttpGet]
        public IActionResult Get()
        {
            // kontrola ukoliko upit nije valjan
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lista = _context.Izdatnice
                  .Include(i => i.Osoba)
                  .Include(i => i.Skladistar)
                  .Include(i => i.Proizvodi)
                  .ToList();
                if (lista == null || lista.Count == 0)
                   
                {
                    return new EmptyResult();
                }
                return new JsonResult(lista.MapIzdatnicaReadList());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }
        /// <summary>
        /// Dohvaća sve sifru iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita
        /// 
        ///    GET api/v1/Sifra
        ///    
        /// </remarks>
        /// <returns>Sifre u bazi</returns>
        /// <response code="200">Sve OK, ako nema podataka content-length: 0 </response>
        /// <response code="400">Zahtjev nije valjan</response>
        /// <response code="503">Baza na koju se spajam nije dostupna</response>

        [HttpGet]
        [Route("{sifra:int}")]
        public IActionResult GetBySifra(int sifra)
        {
            // kontrola ukoliko upit nije valjan
            if (!ModelState.IsValid || sifra <= 0)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var p = _context.Izdatnice.Include(i => i.Osoba).Include(i => i.Skladistar)
                    .Include(i => i.Proizvodi).FirstOrDefault(x => x.Sifra == sifra);
                if (p == null)
                {
                    return new EmptyResult();
                }
                return new JsonResult(p);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }
        /// <summary>
        /// Dodaje novu izdatnicu u bazu
        /// </summary>
        /// <remarks>
        ///     POST api/v1/Izdatnica
        ///     {naziv: "Primjer naziva"}
        /// </remarks>
        /// <param name="dto">Izdatnica za unijeti u JSON formatu</param>
        /// <response code="201">Kreirano</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Baza nedostupna iz razno raznih razloga</response> 
        /// <returns>Izdatnica s šifrom koju je dala baza</returns>
        [HttpPost]
        public IActionResult Post(IzdatnicaDTOInsertUpdate dto)
        {
            if (!ModelState.IsValid || dto == null)
            {
                return BadRequest();
            }

            var osoba = _context.Osobe.Find(dto.osobasifra);

            if (osoba == null)
            {
                return BadRequest();
            }

            var skladistar = _context.Skladistari.Find(dto.skladistarSifra);

            if (skladistar == null)
            {
                return BadRequest();
            }


            var entitet = dto.MapIzdatnicaInsertUpdateFromDTO(new Izdatnica());

            entitet.Osoba = osoba;
            entitet.Skladistar = skladistar;
            entitet.Proizvodi = new List<Proizvod>();

            try
            {
                _context.Izdatnice.Add(entitet);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, entitet.MapIzdatnicaReadToDTO());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }

        }
        ///// <summary>
        ///// Mijenja podatke postojeće izdartnice u bazi
        ///// </summary>
        ///// <remarks>
        ///// Primjer upita:
        /////
        /////    PUT api/v1/izdatnica/1
        /////
        ///// {
        /////  "sifra": 0,
        /////  "Broj izdatnice": "Novi Broj izdatnice",
        /////  "Datum": "Novi Datum"
        /////  "napomena": "nova napomena
        ///// }
        /////
        ///// </remarks>
        ///// <param name="sifra">Šifra izdatnice koja se mijenja</param>  
        ///// <param name="izdatnica">Izdatnica za unijeti u JSON formatu</param>  
        ///// <returns>Svi poslani podaci od izdatnice koji su spremljeni u bazi</returns>
        ///// <response code="200">Sve je u redu</response>
        ///// <response code="204">Nema u bazi izdatnice koju želimo promijeniti</response>
        ///// <response code="415">Nismo poslali JSON</response> 
        ///// <response code="503">Baza nedostupna</response> 


        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, Models.IzdatnicaDTOInsertUpdate dto)
        {
            if (sifra <= 0 || !ModelState.IsValid || dto == null)
            {
                return BadRequest();
            }


            try
            {

                var entitet = _context.Izdatnice.Include(i => i.Osoba).Include(i => i.Skladistar)
                    .Include(i => i.Proizvodi).FirstOrDefault(x => x.Sifra == sifra);

                if (entitet == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, sifra);
                }

                var osoba = _context.Osobe.Find(dto.osobasifra);

                if (osoba == null)
                {
                    return BadRequest();
                }

                var skladistar = _context.Skladistari.Find(dto.skladistarSifra);

                if (skladistar == null)
                {
                    return BadRequest();
                }


                entitet = dto.MapIzdatnicaInsertUpdateFromDTO(entitet);

                entitet.Osoba = osoba;
                entitet.Skladistar = skladistar;


                _context.Izdatnice.Update(entitet);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, entitet.MapIzdatnicaReadToDTO());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }

        }
        /// <summary>
        /// Briše izdatnicu iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/izdatnica/1
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra izdatnice koja se briše</param>  
        /// <returns>Odgovor da li je obrisano ili ne</returns>
        /// <response code="200">Sve je u redu, obrisano je u bazi</response>
        /// <response code="204">Nema u bazi izdatnice kojeu želimo obrisati</response>
        /// <response code="503">Problem s bazom</response> 
        [HttpDelete]
        [Route("{sifra:int}")]
        [Produces("application/json")]
        public IActionResult Delete(int sifra)
        {
            if (!ModelState.IsValid || sifra <= 0)
            {
                return BadRequest();
            }

            try
            {
                var entitetIzBaze = _context.Izdatnice.Find(sifra);

                if (entitetIzBaze == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, sifra);
                }

                _context.Izdatnice.Remove(entitetIzBaze);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\": \"Obrisano\"}"); // ovo nije baš najbolja praksa ali da znake kako i to može

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }

        }

        [HttpGet]
        [Route("Proizvodi/{sifraGrupe:int}")]
        public IActionResult GetProizvodi(int sifraIzdatnice)
        {
            // kontrola ukoliko upit nije valjan
            if (!ModelState.IsValid || sifraIzdatnice <= 0)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var p = _context.Izdatnice
                    .Include(i => i.Proizvodi).FirstOrDefault(x => x.Sifra == sifraIzdatnice);
                if (p == null)
                {
                    return new EmptyResult();
                }
                return new JsonResult(p.Proizvodi!.MapProizvodReadList());
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
                return BadRequest();
            }

            if (sifra <= 0 || proizvodSifra <= 0)
            {
                return BadRequest();
            }

            try
            {

                var izdatnica = _context.Izdatnice
                    .Include(g => g.Proizvodi)
                    .FirstOrDefault(g => g.Sifra == sifra);

                if (izdatnica == null)
                {
                    return BadRequest();
                }

                var proizvod = _context.Proizvodi.Find(proizvodSifra);

                if (proizvod == null)
                {
                    return BadRequest();
                }

                izdatnica.Proizvodi.Add(proizvod);

                _context.Izdatnice.Update(izdatnica);
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
        [Route("{sifra:int}/obrisi/{proizvodSifra:int}")]
        public IActionResult ObrisiIzdatnicu(int sifra, int proizvodSifra)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (sifra <= 0 || proizvodSifra <= 0)
            {
                return BadRequest();
            }

            try
            {

                var izdatnica = _context.Izdatnice
                    .Include(g => g.Proizvodi)
                    .FirstOrDefault(g => g.Sifra == sifra);

                if (izdatnica == null)
                {
                    return BadRequest();
                }

                var proizvod = _context.Proizvodi.Find(proizvodSifra);

                if (proizvod == null)
                {
                    return BadRequest();
                }


                izdatnica.Proizvodi.Remove(proizvod);

                _context.Izdatnice.Update(izdatnica);
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
    }
}
