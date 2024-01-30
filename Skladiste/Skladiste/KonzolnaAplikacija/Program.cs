using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skladiste.KonzolnaAplikacija
{
    internal class Program
    {
        public Program()
        {
            PozdravnaPoruka();
            Izbornik();

            
        }

        private void Izbornik()
        {
            Console.WriteLine("Izbornik");
            Console.WriteLine("1. Rad sa skladištarima");
            Console.WriteLine("2. Rad sa izdatnicama");
            Console.WriteLine("3. Rad sa osobama");
            Console.WriteLine("4. Rad sa proizvodima");
            Console.WriteLine("5. Izlaz iz programa");
            var izbor = Pomocno.UcitajString("Unesi odabir : ");


        }

        private void PozdravnaPoruka()
        {
            Console.WriteLine("****************");
            Console.WriteLine("*   SKLADIŠTE  *");
            Console.WriteLine("****************");
        }
    }
}
