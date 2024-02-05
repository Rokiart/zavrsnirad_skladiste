using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skladiste2.KonzolnaAplikacija.Model
{
    internal class Izdatnica : Entitet
    {
        public string BrojIzdatnice{ get; set; }
        public DateTime DatumIzdavanja { get; set; }
        public List<Osoba> Osobe{ get; set; }
        public List<Skladistar> Skladistari{ get; set; }
        public string Napomena{ get; set; }
    }
}
