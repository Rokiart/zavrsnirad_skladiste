using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skladiste.KonzolnaAplikacija.Model
{
    internal class Izdatnica : Entitet
    {
        public int BrojIzdatnice { get; set; }
        
        public string Osoba{ get; set; }
        public string Skladistar{ get; set; }
       
   
        public object DatumIzdatnice { get; internal set; }
        public object DatumPocetka { get; internal set; }

       
    }
}
