using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skladiste2.KonzolnaAplikacija
{
    internal class ObradaProizvoda
    {
        public void PrikaziIzbornik()
        {
            Console.WriteLine("Izbornik za rad s proizvodima");
            Console.WriteLine("1. Pregled postojećih proizvod");
            Console.WriteLine("2. Unos novog proizvoda");
            Console.WriteLine("3. Promjena postojećeg proizvoda");
            Console.WriteLine("4. Brisanje proizvoda");
            Console.WriteLine("5. Povratak na glavni izbornik");
        }
    }
}
