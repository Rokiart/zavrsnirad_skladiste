using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skladiste2.KonzolnaAplikacija
{
    internal class ObradaSkladistara
    {
        public void PrikaziIzbornik()
        {
            Console.WriteLine("Izbornik za rad s skladištarima");
            Console.WriteLine("1. Pregled postojučih skladištara");
            Console.WriteLine("2. Unos novog skladištara");
            Console.WriteLine("3. Promjena postojučeg skladištara");
            Console.WriteLine("4. Brisanje skladištara");
            Console.WriteLine("5.Povratak na glavni izbornik");
        }
    }
}
