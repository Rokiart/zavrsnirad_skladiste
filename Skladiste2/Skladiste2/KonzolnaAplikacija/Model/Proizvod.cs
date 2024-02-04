

namespace Skladiste2.KonzolnaAplikacija.Model
{
    internal class Proizvod: Entitet
    {
        public string Naziv{ get; set; }
        public string Sifraproizvoda{ get; set; }
        public string MjernaJedinica{ get; set; }
        public int Kolicina{ get; set; }
        public override string ToString()
        {
            return   Naziv ;
        }

    }
}
