
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKladisteAppl.Models
{
    /// <summary>
    /// Ovo mi je POCO koji je mapiran na bazu
    /// </summary>
    public class Izdatnica : Entitet

    {

        /// <summary>
        /// Broj izdatnice u bazi
        /// </summary>
        [Required(ErrorMessage = "Broj izdatnice obavezno")]
        public string? BrojIzdatnice { get; set; }
        /// <summary>
        /// datum izdatnice u bazi
        /// </summary>
        public DateTime? Datum { get; set; }
        /// <summary>
        /// Vanjski kljuc za osobu
        /// </summary>
        //public ICollection<Osoba> Osobe { get; set; }
        /// <summary>
        /// Vanjski kljuc za skladistara
        /// </summary>
        //public ICollection<Skladistar> Skladistari { get; set; }
        /// <summary>
        /// Napomena max 250 karaktera u bazi
        /// </summary>
        public string? Napomena { get; set; }
       
    }
}
