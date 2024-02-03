using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skladiste2.KonzolnaAplikacija
{
    internal class ObradaIzdatnica
    {
        public void PrikaziIzbornik()
        {
            Console.WriteLine("Izbornik za rad sa izdatnicama ");
            Console.WriteLine("1. Pregled postoječih izdatnica ");
            Console.WriteLine("2. Unos nove izdatnice ");
            Console.WriteLine("3. Promjena postoječe izdatnice");
            Console.WriteLine("4. Brisanje izdatnice");
            Console.WriteLine("5. Povratak na glavni izbornik");
        }
    }
}
