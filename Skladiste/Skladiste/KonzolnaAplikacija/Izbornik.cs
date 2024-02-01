using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skladiste.KonzolnaAplikacija
{
    internal class Izbornik
    {
        public ObradaIzdatnice ObradaIzdatnice{ get; }
        public ObradaOsobe ObradaOsobe{ get; }
        public ObradaProizvoda ObradaProizvoda{ get; set; }
        public ObradaSkladistara ObradaSkladistara{ get; set; }
        public Izbornik()
        {
            Pomocno.dev = true;
            ObradaIzdatnice = new ObradaIzdatnice();
            ObradaOsobe = new ObradaOsobe();
            ObradaProizvoda = new ObradaProizvoda();
            ObradaSkladistara = new ObradaSkladistara();
            PozdravnaPoruka();
            PrikaziIzbornik();
        }
        private void PozdravnaPoruka()
        {
            Console.WriteLine("*************************************");
            Console.WriteLine("**** Skladište Console APP v 1.0 ****");
            Console.WriteLine("*************************************");
        }

        private void PrikaziIzbornik()
        {
            Console.WriteLine("Glavni izbornik");
            Console.WriteLine("1. Izdatnice ");
            Console.WriteLine("2. Osobe ");
            Console.WriteLine("3. Proizvodi ");
            Console.WriteLine("4. Skladistari ");
            Console.WriteLine("5. Izlaz iz programa ");

            switch(Pomocno.ucitajBrojRaspon("Odaberite broj izbornika : ",
                "Odabir mora biti 1 -5 ", 1, 5))
            {
                case 1:
                    ObradaIzdatnice.PrikaziIzbornik();
                    PrikaziIzbornik();
                    break;
                case 2:
                    ObradaOsobe.PrikaziIzbornik();
                    PrikaziIzbornik();
                    break;
                case 3:
                    ObradaProizvoda.PrikaziIzbornik();
                    PrikaziIzbornik();
                    break;
                case 4:
                    ObradaSkladistara.PrikaziIzbornik();
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("Hvala što ste koristili program , doviđenja");
                    break;

            }

        }

      
    }
}
