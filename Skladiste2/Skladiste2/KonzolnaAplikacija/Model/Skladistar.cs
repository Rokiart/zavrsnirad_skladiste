using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skladiste2.KonzolnaAplikacija.Model
{
    internal class Skladistar : Osoba
    {
     

        public override string ToString()
        {
            return Ime + " " + Prezime;
        }
    }
}
