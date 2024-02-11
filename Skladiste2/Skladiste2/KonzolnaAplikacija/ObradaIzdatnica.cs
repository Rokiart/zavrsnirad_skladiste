using Skladiste2.KonzolnaAplikacija.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Skladiste2.KonzolnaAplikacija
{
    internal class ObradaIzdatnica
    {
        public List<Izdatnica> Izdatnice { get; }

        private Izbornik Izbornik;

        public ObradaIzdatnica (Izbornik izbornik) : this()
        {
            this.Izbornik = izbornik;
        }

        public ObradaIzdatnica() 
        {
            Izdatnice = new List<Izdatnica>();
        }

        public void PrikaziIzbornik()
        {
            Console.WriteLine("**************************************************");
            Console.WriteLine("Izbornik za rad sa izdatnicama ");
            Console.WriteLine("1. Pregled postoječih izdatnica ");
            Console.WriteLine("2. Unos nove izdatnice ");
            Console.WriteLine("3. Promjena postoječe izdatnice");
            Console.WriteLine("4. Brisanje izdatnice");
            Console.WriteLine("5. Povratak na glavni izbornik");
            switch(Pomocno.ucitajBrojRaspona("Odaberite stavku iz izbornika izdatnice : ",
                "Odabir mora biti 1-5", 1, 5))
            {
                case 1:
                    PrikazIzdatnica();
                    PrikaziIzbornik();
                    break;
                case 2:
                    UnosNoveIzdatnice();
                    PrikaziIzbornik();
                    break;
                case 3:
                    PromjenaIzdatnice();
                    PrikaziIzbornik();
                    break;
                case 4:
                    BrisanjeIzdatnice();
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("Gotov je rad sa izdatnicama");
                    break;





            }
        }

        private void BrisanjeIzdatnice()
        {
            Console.WriteLine("**************************************************");
            PrikazIzdatnica();
            int index = Pomocno.ucitajBrojRaspona("Odaberi redni broj izdatnice: ", "Nije dobar odabir", 1, Izdatnice.Count());
            Izdatnice.RemoveAt(index - 1);
        }

        private void PromjenaIzdatnice()
        {
            Console.WriteLine("**************************************************");
            PrikazIzdatnica();
            int index = Pomocno.ucitajBrojRaspona("Odaberi redni broj izdatnice: ", "Nije dobar odabir", 1,Izdatnice.Count());
            var p = Izdatnice[index - 1];
            p.Sifra = Pomocno.ucitajCijeliBroj("Unesite šifru izdatnice (" + p.Sifra + "): ",
                "Unos mora biti pozitivni cijeli broj");
            p.BrojIzdatnice = Pomocno.UcitajString("Unesite broj izdatnice (" + p.BrojIzdatnice + "): ",
                "Unos obavezan");
            Console.WriteLine("Trenutni broj osobe: {0}", p.Osobe);
            p.Osobe = PostaviOsobe();
            Console.WriteLine("Trenutne    Osobe:");
            Console.WriteLine("------------------");
            Console.WriteLine("----   Osobe  ----");
            Console.WriteLine("------------------");
            int b = 1;
            foreach (Osoba osoba in p.Osobe)
            {
                Console.WriteLine("{0}. {1}", b++, osoba);
            }
            Console.WriteLine("------------------");
            p.Osobe = PostaviOsobe();
        }

        private void UnosNoveIzdatnice()
        {
            Console.WriteLine("**************************************************");
            var i = new Izdatnica();
            i.Sifra = Pomocno.ucitajCijeliBroj("Unesite šifra izdatnice: ",
                "Unos mora biti pozitivni cijeli broj");
            i.BrojIzdatnice = Pomocno.UcitajString("Unesite Broj Izdatnice : ",
                "Unos obavezan");
            i.DatumIzdavanja = Pomocno.ucitajDatum("Unesi datum izdatnice dd.MM.yyyy.", "Greška");
            i.Osobe = PostaviOsobe();
            i.Skladistari = PostaviSkladistare();
            i.Napomena = Pomocno.UcitajString("Unesi napomenu : ", "Greška");
            Izdatnice.Add(i);
        }
        public List<Osoba> PostaviOsobe()
        {
            Console.WriteLine("**************************************************");
            List<Osoba> osobe = new List<Osoba>();
            Console.WriteLine("1. Dodaj novu osobu");
            Console.WriteLine("2. Odaberi postojeću osobu: ");
            switch (Pomocno.ucitajBrojRaspona("Odaberi opciju: ", "Greška", 1, 2))
            {
                case 1:
                    Izbornik.ObradaOsobe.UcitajOsobu();
                    break;
                case 2:
                    osobe.Add(PostaviOsobu());
                    break;
            }
            return osobe;
        }
        public Osoba PostaviOsobu()
        {
            Console.WriteLine("**************************************************");
            Izbornik.ObradaOsobe.PregledOsoba();
            int index = Pomocno.ucitajBrojRaspona("Odaberi osobu: ", "Nije dobar odabir", 1, Izbornik.ObradaOsobe.Osobe.Count());
            return Izbornik.ObradaOsobe.Osobe[index - 1];
        }

      

        private List<Skladistar> PostaviSkladistare()
        {
            Console.WriteLine("**************************************************");
            List<Skladistar> skladistari = new List<Skladistar>();
            while (Pomocno.ucitajBool("Želite li dodati skladistare? (da ili bilo što drugo za ne): "))
            {
                skladistari.Add(PostaviSkladistara());
            }

            return skladistari;
        }

        private Skladistar PostaviSkladistara()
        {
            Console.WriteLine("**************************************************");
            Izbornik.ObradaSkladistara.PrikaziIzbornik();
            int index = Pomocno.ucitajBrojRaspona("Odaberi redni broj skladistara: ", "Nije dobar odabir", 1, Izbornik.ObradaSkladistara.SKladistari.Count());
            return Izbornik.ObradaSkladistara.SKladistari[index - 1];

        }

       

        private void PrikazIzdatnica()
        {
            Console.WriteLine("------------------");
            Console.WriteLine("--- Izdatnice ----");
            Console.WriteLine("------------------");
            int b = 1;
            foreach (Izdatnica izdatnica in Izdatnice)
            {
                Console.WriteLine("{0}. {1}", b++, izdatnica.BrojIzdatnice);
            }
            Console.WriteLine("------------------");
        }
    }
}
