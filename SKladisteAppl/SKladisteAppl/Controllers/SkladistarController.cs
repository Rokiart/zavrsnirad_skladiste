using Microsoft.AspNetCore.Mvc;
using SKladisteAppl.Data;
using SKladisteAppl.Models;
using Microsoft.Data.SqlClient;

namespace SKladisteAppl.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD operacije nad entitetom skladistar u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SkladistarController : ControllerBase
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


        public SkladistarController(SkladisteContext context)
        {
            _context = context;

        }




        /// <summary>
        /// Dohvaća sve skladistare iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita
        /// 
        ///    GET api/v1/Skladistar
        ///    
        /// </remarks>
        /// <returns>Skladistari u bazi</returns>
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
                var skladistari = _context.Skladistari.ToList();
                if (skladistari == null || skladistari.Count == 0)
                {
                    return new EmptyResult();
                }
                return new JsonResult(skladistari);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }


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
                var skladistar = _context.Skladistari.Find(sifra);
                if (skladistar == null)
                {
                    return new EmptyResult();
                }
                return new JsonResult(skladistar);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }
        /// <summary>
        /// Dodaje novog skladištara u bazu
        /// </summary>
        /// <remarks>
        ///     POST api/v1/Skladištar
        ///     {naziv: "Primjer naziva"}
        /// </remarks>
        /// <param name="skladistar">Skladistar za unijeti u JSON formatu</param>
        /// <response code="201">Kreirano</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Baza nedostupna iz razno raznih razloga</response> 
        /// <returns>Skladistar s šifrom koju je dala baza</returns>
        [HttpPost]
        public IActionResult Post(Skladistar skladistar)
        {
            if (!ModelState.IsValid || skladistar == null)
            {
                return BadRequest();
            }
            try
            {
                _context.Skladistari.Add(skladistar);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, skladistar);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Mijenja podatke postojećeg skladistara u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/skladistar/1
        ///
        /// {
        ///  "sifra": 0,
        ///  "ime": "Novo ime",
        ///  "prezime": "Novo prezime",
        ///  "Email": "Novi Email",
        ///  "Broj telefona":"Novi broj telefona"
        /// }
        ///
        /// </remarks>
        /// <param name="sifra">Šifra skladistara koji se mijenja</param>  
        /// <param name="skladistar">Skladistar za unijeti u JSON formatu</param>  
        /// <returns>Svi poslani podaci od skladistara koji su spremljeni u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi osobe kojeu želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Baza nedostupna</response>


        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, Skladistar skladistar)
        {
            if (sifra <= 0 || !ModelState.IsValid || skladistar == null)
            {
                return BadRequest();
            }


            try
            {


                var skladistarIzBaze = _context.Skladistari.Find(sifra);

                if (skladistarIzBaze == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, sifra);
                }


                // inače ovo rade mapperi
                // za sada ručno
                skladistarIzBaze.Ime = skladistar.Ime;
                skladistarIzBaze.Prezime = skladistar.Prezime;
                skladistarIzBaze.Email = skladistar.Email;
                skladistarIzBaze.BrojTelefona = skladistar.BrojTelefona;


                _context.Skladistari.Update(skladistarIzBaze);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, skladistarIzBaze);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }

        }

        /// <summary>
        /// Briše skladistara iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/skladistar/1
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra skladistara koji se briše</param>  
        /// <returns>Odgovor da li je obrisano ili ne</returns>
        /// <response code="200">Sve je u redu, obrisano je u bazi</response>
        /// <response code="204">Nema u bazi osobe kojeu želimo obrisati</response>
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
                var skladistarIzBaze = _context.Skladistari.Find(sifra);

                if (skladistarIzBaze == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, sifra);
                }

                _context.Skladistari.Remove(skladistarIzBaze);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\": \"Obrisano\"}");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }

        }

    }
}
