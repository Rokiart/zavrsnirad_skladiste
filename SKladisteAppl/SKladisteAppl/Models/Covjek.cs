namespace SKladisteAppl.Models
{
    /// <summary>
    /// Apstraktna klasa koja predstavlja osobu u sustavu skladišta.
    /// Nasljeđuje osnovne osobine entiteta.
    /// </summary>
    public abstract class Covjek : Entitet
    {
        /// <summary>
        /// Ime osobe u bazi podataka.
        /// </summary>
        public string? Ime { get; set; }

        /// <summary>
        /// Prezime osobe u bazi podataka.
        /// </summary>
        public string? Prezime { get; set; }

        /// <summary>
        /// Email osobe u bazi podataka.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Broj telefona osobe u bazi podataka.
        /// </summary>
        public string? BrojTelefona { get; set; }
    }
}
