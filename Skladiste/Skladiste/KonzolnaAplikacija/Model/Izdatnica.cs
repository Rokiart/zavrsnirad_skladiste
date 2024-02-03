

namespace Skladiste.KonzolnaAplikacija.Model
{
    internal class Izdatnica : Entitet
    {


        public int BrojIzdatnice { get; set; }
        public DateTime DatumIzdatnice { get; set; }

        public Osoba Osoba{ get; set; }
        public List<Skladistar> Skladistari{ get; set; }
        public string Napomena{ get; set; }
       
    }
}
