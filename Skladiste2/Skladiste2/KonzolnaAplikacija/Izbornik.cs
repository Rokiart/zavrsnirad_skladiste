using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Skladiste2.KonzolnaAplikacija
{
    internal class Izbornik
    {
        public ObradaOsobe ObradaOsobe { get; }
        public ObradaProizvoda ObradaProizvoda { get; }
        public ObradaSkladistara ObradaSkladistara { get; }

        private ObradaIzdatnica ObradaIzdatnica;

        public Izbornik()
        {
            Pomocno.dev = true;
            ObradaOsobe = new ObradaOsobe();
            ObradaProizvoda = new ObradaProizvoda();
            ObradaSkladistara = new ObradaSkladistara();
            ObradaIzdatnica = new ObradaIzdatnica();
            PozdravnaPoruka();
            PrikaziIzbornik();
        }

        private void PozdravnaPoruka()
        {
            Console.WriteLine("*************************************");
            Console.WriteLine("***  Skladište Console APP v 1.0. ***");
            Console.WriteLine("*************************************");
        }

        private void PrikaziIzbornik()
        {
            Console.WriteLine("Glavni izbornik");
            Console.WriteLine("1. Osobe");
            Console.WriteLine("2. Proizvodi");
            Console.WriteLine("3. Skladištari");
            Console.WriteLine("4. Izdatnice");
            Console.WriteLine("5. Izlaz iz programa");

            switch (Pomocno.ucitajBrojRaspona("Odaberite stavku izbornika: ",
                "Odabir mora biti 1 - 5.", 1, 5))
            {
                case 1:
                    ObradaOsobe.PrikaziIzbornik();
                    PrikaziIzbornik();
                    break;
                case 2:
                    ObradaProizvoda.PrikaziIzbornik();
                    PrikaziIzbornik();
                    break;
                case 3:
                    ObradaSkladistara.PrikaziIzbornik();
                    PrikaziIzbornik();
                    break;
                case 4:
                    ObradaIzdatnica.PrikaziIzbornik();
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("Hvala na korištenju, doviđenja");
                    break;
            }
        }
       
    }
}
