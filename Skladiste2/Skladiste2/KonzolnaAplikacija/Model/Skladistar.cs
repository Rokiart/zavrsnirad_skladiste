using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skladiste2.KonzolnaAplikacija.Model
{
    internal class Skladistar : Entitet
    {
        public string Ime{ get; set; }
        public string Prezime{ get; set; }
        public string BrojTelefona{ get; set; }
        public string Email{ get; set; }

        public override string ToString()
        {
            return Ime + "" + Prezime;
        }
    }
}
