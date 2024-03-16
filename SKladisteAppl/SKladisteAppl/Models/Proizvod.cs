namespace SKladisteAppl.Models
{
    /// <summary>
    /// Predstavlja proizvod u sustavu skladišta.
    /// </summary>
    public class Proizvod : Entitet
    {
        /// <summary>
        /// Naziv proizvoda.
        /// </summary>
        public string? Naziv { get; set; }

        /// <summary>
        /// Šifra proizvoda.
        /// </summary>
        public string? Sifraproizvoda { get; set; }

        /// <summary>
        /// Mjerna jedinica proizvoda.
        /// </summary>
        public string? Mjernajedinica { get; set; }

        /// <summary>
        /// Izdatnice koje sadrže ovaj proizvod.
        /// </summary>
        public ICollection<Izdatnica>? Izdatnice { get; set; } = new List<Izdatnica>();
    }
}