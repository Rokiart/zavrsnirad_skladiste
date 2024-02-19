
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKladisteAppl.Models

{
    /// <summary>
    /// Ovo mi je POCO koji je mapiran na bazu
    /// </summary>
    public class Proizvod : Entitet
    {
        /// <summary>
        /// Naziv u bazi
        /// </summary>
        [Required(ErrorMessage = "Naziv obavezno")]
        public string? Naziv { get; set; }
        /// <summary>
        /// Sifra proizvoda u bazi
        /// </summary>
        [Required(ErrorMessage = "Šifra proizvoda obavezno")]
        public string? Sifraproizvoda { get; set; }
        /// <summary>
        /// Mjerna jedinica u bazi
        /// </summary>
        [Required(ErrorMessage = "mjerna jedinica obavezno")]
        public string? Mjernajedinica { get; set; }
        
    }
}
