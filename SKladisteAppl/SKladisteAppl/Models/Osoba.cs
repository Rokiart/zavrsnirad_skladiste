using System.Text.RegularExpressions;

namespace SKladisteAppl.Models
{
    /// <summary>
    /// Predstavlja osobu u sustavu skladišta.
    /// </summary>
    public class Osoba : Covjek
    {
        public ICollection<Izdatnica>? Izdatnice { get; } = new List<Izdatnica>();
        // Ova klasa nasljeđuje svojstva iz klase Covjek, kao što su Ime, Prezime, Email i BrojTelefona.
        // Dodatna svojstva ili metode mogu se dodati ovdje ako su specifična za osobu u sustavu skladišta.
    }
}