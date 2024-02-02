using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skladiste.KonzolnaAplikacija.Model
{
    internal class Proizvodi : Entitet
    {
        public string Naziv{ get; set; }
        public  int SifraProizvoda{ get; set; }
        public string MjernaJedinica{ get; set; }
        public int Kolicina{ get; set; }




    }
}
