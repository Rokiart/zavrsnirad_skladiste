using SKladisteAppl.Data;
using Microsoft.AspNetCore.Mvc;

namespace SKladisteAppl.Controllers
{

    /// <summary>
    /// Namjenjeno za CRUD operacije nad entitetom smjer u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OsobaController : ControllerBase
    {
        /// <summary>
        /// Kontest za rad s bazom koji će biti postavljen s pomoću Dependecy Injection-om
        /// </summary>
        private readonly SkladisteContext _context;
        /// <summary>
        /// Konstruktor klase koja prima Edunova kontext
        /// pomoću DI principa
        /// </summary>
        /// <param name="context"></param>
        public OsobaController(SkladisteContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Dohvaća sve smjerove iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita
        /// 
        ///    GET api/v1/Smjer
        ///    
        /// </remarks>
        /// <returns>Smjerovi u bazi</returns>
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
                var Osobe = _context.Osobe.ToList();
                if (Osobe == null || Osobe.Count == 0)
                {
                    return new EmptyResult();
                }
                return new JsonResult(Osobe);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }


    }

}
