using System.ComponentModel.DataAnnotations;

namespace SKladisteAppl.Models
{
    /// <summary>
    /// Ovo mi je POCO koji je mapiran na bazu
    /// </summary>
    public class Skladistar : Entitet

    {
        /// <summary>
        /// Ime u bazi
        /// </summary>
        [Required(ErrorMessage = "Ime obavezno")]
        public string? Ime { get; set; }
        /// <summary>
        /// Prezime u bazi
        /// </summary>
        [Required(ErrorMessage = "Prezime obavezno")]
        public string? Prezime { get; set; }
        /// <summary>
        /// Email u bazi
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Broj Telefona u bazi
        /// </summary>
        public string? BrojTelefona { get; set; }

       


    }
}
