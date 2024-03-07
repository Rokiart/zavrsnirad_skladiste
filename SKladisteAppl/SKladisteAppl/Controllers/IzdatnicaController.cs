using SKladisteAppl.Data;
using SKladisteAppl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;


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
                var izdatnice = _context.Izdatnice.ToList();
                if (izdatnice == null || izdatnice.Count == 0)
                {
                    return new EmptyResult();
                }
                return new JsonResult(izdatnice);
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
                var izdatnica = _context.Izdatnice.Find(sifra);
                if (izdatnica == null)
                {
                    return new EmptyResult();
                }
                return new JsonResult(izdatnica);
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
        /// <param name="izdatnica">Izdatnica za unijeti u JSON formatu</param>
        /// <response code="201">Kreirano</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Baza nedostupna iz razno raznih razloga</response> 
        /// <returns>Izdatnica s šifrom koju je dala baza</returns>
        [HttpPost]
        public IActionResult Post(Izdatnica izdatnica)
        {
            if (!ModelState.IsValid || izdatnica == null)
            {
                return BadRequest();
            }
            try
            {
                _context.Izdatnice.Add(izdatnica);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, izdatnica);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }

        }
        /// <summary>
        /// Mijenja podatke postojeće izdartnice u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/izdatnica/1
        ///
        /// {
        ///  "sifra": 0,
        ///  "Broj izdatnice": "Novi Broj izdatnice",
        ///  "Datum": "Novi Datum"
        ///  "napomena": "nova napomena
        /// }
        ///
        /// </remarks>
        /// <param name="sifra">Šifra izdatnice koja se mijenja</param>  
        /// <param name="izdatnica">Izdatnica za unijeti u JSON formatu</param>  
        /// <returns>Svi poslani podaci od izdatnice koji su spremljeni u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi izdatnice koju želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Baza nedostupna</response> 


        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, Izdatnica izdatnica)
        {
            if (sifra <= 0 || !ModelState.IsValid || izdatnica == null)
            {
                return BadRequest();
            }


            try
            {


                var izdatnicaIzBaze = _context.Izdatnice.Find(sifra);

                if (izdatnicaIzBaze == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, sifra);
                }


                // inače ovo rade mapperi
                // za sada ručno
                izdatnicaIzBaze.BrojIzdatnice = izdatnica.BrojIzdatnice;
                izdatnicaIzBaze.Datum = izdatnica.Datum;
                //izdatnicaIzBaze.Kolicina = izdatnica.kolicina;
                izdatnicaIzBaze.Napomena = izdatnica.Napomena;


                _context.Izdatnice.Update(izdatnicaIzBaze);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, izdatnicaIzBaze);
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
                var izdatnicaIzBaze = _context.Izdatnice.Find(sifra);

                if (izdatnicaIzBaze == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, sifra);
                }

                _context.Izdatnice.Remove(izdatnicaIzBaze);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\": \"Obrisano\"}"); // ovo nije baš najbolja praksa ali da znake kako i to može

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }

        }
    }
}
