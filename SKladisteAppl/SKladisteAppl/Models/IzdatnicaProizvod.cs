using System.ComponentModel.DataAnnotations.Schema;

namespace SKladisteAppl.Models
{
    public class IzdatnicaProizvod : Entitet

    {
        public int? Kolicina { get; set; }

        [ForeignKey("izdatnica")]
        public  Izdatnica? Izdatnica  { get; set; }
        [ForeignKey("proizvod")]
        public  Proizvod? Proizvod { get; set; }

       

    }
}
