using SKladisteAppl.Data;
using Microsoft.AspNetCore.Mvc;
using SKladisteAppl.Models;
using Microsoft.Data.SqlClient;
using SKladisteAppl.Extensions;

namespace SKladisteAppl.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD operacije nad entitetom proizvod u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProizvodController : ControllerBase
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
        public ProizvodController(SkladisteContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Dohvaća sve proizvode iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita
        /// 
        ///    GET api/v1/Proizvod
        ///    
        /// </remarks>
        /// <returns>Proizvodi u bazi</returns>
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
                var lista = _context.Proizvodi.ToList();
                if (lista == null || lista.Count == 0)
                {
                    return new EmptyResult();
                }
                return new JsonResult(lista.MapProizvodReadList());
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
                var p = _context.Proizvodi.Find(sifra);
                if (p == null)
                {
                    return new EmptyResult();
                }
                return new JsonResult(p.MapProizvodInsertUpdatedToDTO());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Dodaje novi proizvod u bazu
        /// </summary>
        /// <remarks>
        ///     POST api/v1/Proizvod
        ///     {naziv: "Primjer proizvoda"}
        /// </remarks>
        /// <param name="dto">Proizvod za unijeti u JSON formatu</param>
        /// <response code="201">Kreirano</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Baza nedostupna iz razno raznih razloga</response> 
        /// <returns>Proizvod s šifrom koju je dala baza</returns>
        [HttpPost]
        public IActionResult Post(ProizvodDTOInsertUpdate dto)
        {
            if (!ModelState.IsValid || dto == null)
            {
                return BadRequest();
            }
            try
            {
                var entitet = dto.MapProizvodInsertUpdateFromDTO(new Proizvod());
                _context.Proizvodi.Add(entitet);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, entitet.MapProizvodReadToDTO());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Mijenja podatke postojećeg proizvoda u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/proizvod/1
        ///
        /// {
        ///  "sifra": 0,
        ///  "naziv": "Novi naziv",
        ///  "sifra proizvoda": "Nova Šifra proizvpoda",
        ///  "mjerna jedinica": "Nova mjerna jedinica"
        /// }
        ///
        /// </remarks>
        /// <param name="sifra">Šifra proizvoda koji se mijenja</param>  
        /// <param name="dto">Proizvod za unijeti u JSON formatu</param>  
        /// <returns>Svi poslani podaci od proizvoda koji su spremljeni u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi proizvoda kojeg želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Baza nedostupna</response> 


        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, ProizvodDTOInsertUpdate dto)
        {
            if (sifra <= 0 || !ModelState.IsValid || dto == null)
            {
                return BadRequest();
            }


            try
            {


                var entitetIzBaze = _context.Proizvodi.Find(sifra);

                if (entitetIzBaze == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, sifra);
                }

                entitetIzBaze = dto.MapProizvodInsertUpdateFromDTO(entitetIzBaze);


                _context.Proizvodi.Update(entitetIzBaze);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status200OK,
                    entitetIzBaze.MapProizvodInsertUpdatedToDTO());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }

        }

        /// <summary>
        /// Briše proizvod iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/Proizvod/1
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra proizvoda koji se briše</param>  
        /// <returns>Odgovor da li je obrisano ili ne</returns>
        /// <response code="200">Sve je u redu, obrisano je u bazi</response>
        /// <response code="204">Nema u bazi proizvoda kojeg želimo obrisati</response>
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
                var proizvodIzBaze = _context.Proizvodi.Find(sifra);

                if (proizvodIzBaze == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, sifra);
                }

                _context.Proizvodi.Remove(proizvodIzBaze);
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


                var proizvodi = query.ToList();

                return new JsonResult(proizvodi.MapProizvodReadList()); //200

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, e.Message); //204
            }
        }

    }
}
