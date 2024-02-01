using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skladiste.KonzolnaAplikacija.Model
{
    internal class Osobe : Entitet
    {
        public string Ime { get; set; }
        public string Prezime{ get; set; }
        public string Email{ get; set; }
        public int BrojTelefona { get; set; }

        public override string ToString()
        {
            return Ime + " " + Prezime ;
        }

    }
}
