using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKladisteAppl.Models
{
    /// <summary>
    /// Predstavlja izdatnicu u sustavu skladišta.
    /// </summary>
    public class Izdatnica : Entitet
    {
        /// <summary>
        /// Broj izdatnice u bazi podataka.
        /// </summary>
        public string? BrojIzdatnice { get; set; }

        /// <summary>
        /// Datum izdatnice u bazi podataka.
        /// </summary>
        public DateTime? Datum { get; set; }

        /// <summary>
        /// Osoba koja je izdala izdatnicu.
        /// </summary>
        [ForeignKey("osoba")]
        public Osoba? Osoba { get; set; }

        /// <summary>
        /// Skladištar koji je povezan s izdatnicom.
        /// </summary>
        [ForeignKey("skladistar")]
        public Skladistar? Skladistar { get; set; }

        /// <summary>
        /// Napomena uz izdatnicu, maksimalno 250 karaktera.
        /// </summary>
        public string? Napomena { get; set; }

        /// <summary>
        /// Lista proizvoda koji su vezani uz izdatnicu.
        /// </summary>
        public List<Proizvod>? Proizvodi { get; set; }
        public string? Proizvod { get; internal set; }
    }
}