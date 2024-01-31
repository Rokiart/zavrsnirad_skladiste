using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skladiste.KonzolnaAplikacija.Model
{
    internal class Izdatnice : Entitet
    {
        public string BrojIzdatnice{ get; set; }
        public DateOnly DatumIzdavanja{ get; set; }
        public string Osoba{ get; set; }
        public string Skladistar{ get; set; }
        public string Napomena{ get; set; }

    }
}
