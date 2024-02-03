using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skladiste2.KonzolnaAplikacija
{
    internal class ObradaOsobe
    {
        public void PrikaziIzbornik()
        {
            Console.WriteLine("Izbornik za rad s osobama");
            Console.WriteLine("1. Pregled postojučih osoba");
            Console.WriteLine("2. Unos nove osobe");
            Console.WriteLine("3. Promjena postojuče osobe");
            Console.WriteLine("4. Brisanje osobe");
            Console.WriteLine("5.Povratak na glavni izbornik");
        }
    }
}
