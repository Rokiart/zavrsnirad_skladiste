using System.ComponentModel.DataAnnotations.Schema;

namespace SKladisteAppl.Models
{
    public class IzdatniceProizvodi : Entitet

    {
        public string? Kolicina { get; set; }

        [ForeignKey("izdatnica")]
        public Izdatnica? Izdatnica  { get; set; }
        [ForeignKey("proizvod")]
        public Proizvod? Proizvod { get; set; }

       

    }
}
