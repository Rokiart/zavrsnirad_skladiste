
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKladisteAppl.Models
{
    public class Proizvod : Entitet
    {
        /// <summary>
        /// Naziv u bazi
        /// </summary>
        [Required(ErrorMessage = "Naziv obavezno")]
        public string? Naziv { get; set; }
        /// <summary>
        /// Naziv u bazi
        /// </summary>
        [Required(ErrorMessage = "Šifra proizvoda obavezno")]
        public string? Sifraproizvoda { get; set; }
        /// <summary>
        /// Naziv u bazi
        /// </summary>
        [Required(ErrorMessage = "mjerna jedinica obavezno")]
        public string? MjernaJedinica { get; set; }
        
    }
}
