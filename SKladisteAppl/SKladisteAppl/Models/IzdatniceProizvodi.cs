using System.ComponentModel.DataAnnotations.Schema;

namespace SKladisteAppl.Models
{
    public class IzdatniceProizvodi : Entitet

    {
        

        [ForeignKey("izdatnica")]
        public Izdatnica? Izdatnica  { get; set; }
        [ForeignKey("proizvod")]
        public Proizvod? Proizvod { get; set; }

        public int? Kolicina { get; set; }

    }
}
