using System.ComponentModel.DataAnnotations.Schema;

namespace SKladisteAppl.Models
{
    public class IzdatniceProizvodi : Entitet

    {
        internal object kolicina;

        [ForeignKey("izdatnica")]
        public Izdatnica? Izdatnica  { get; set; }
        [ForeignKey("proizvod")]
        public Proizvod? Proizvod { get; set; }

        public string? Kolicina { get; set; }

    }
}
