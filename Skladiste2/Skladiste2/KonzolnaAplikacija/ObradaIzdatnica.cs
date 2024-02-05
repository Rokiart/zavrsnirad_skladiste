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
        public List<Izdatnica> izdatnice { get; }

        private Izbornik izbornik;

        public ObradaIzdatnica (Izbornik izbornik) : this()
        {
            this.izbornik = izbornik;
        }

        public ObradaIzdatnica() 
        {
            izdatnice = new List<Izdatnica>();
        }

        public void PrikaziIzbornik()
        {
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
            PrikazIzdatnica();
            int index = Pomocno.ucitajBrojRaspona("Odaberi redni broj grupe: ", "Nije dobar odabir", 1, izdatnice.Count());
            izdatnice.RemoveAt(index - 1);
        }

        private void PromjenaIzdatnice()
        {
            PrikazIzdatnica();
            int index = Pomocno.ucitajBrojRaspona("Odaberi redni broj grupe: ", "Nije dobar odabir", 1,izdatnice.Count());
            var p = izdatnice[index - 1];
            p.Sifra = Pomocno.ucitajCijeliBroj("Unesite šifra grupe (" + p.Sifra + "): ",
                "Unos mora biti pozitivni cijeli broj");
            p.BrojIzdatnice = Pomocno.UcitajString("Unesite naziv grupe (" + p.BrojIzdatnice + "): ",
                "Unos obavezan");
            Console.WriteLine("Trenutni smjer: {0}", p.Osobe);
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
            var i = new Izdatnica();
            i.Sifra = Pomocno.ucitajCijeliBroj("Unesite šifra izdatnice: ",
                "Unos mora biti pozitivni cijeli broj");
            i.BrojIzdatnice = Pomocno.UcitajString("Unesite Broj Izdatnice : ",
                "Unos obavezan");
            i.DatumIzdavanja = Pomocno.ucitajDatum("Unesi datum izdatnice dd.MM.yyyy.", "Greška");
            i.Osobe = PostaviOsobe();
            i.Skladistari = PostaviSkladistare();
            i.Napomena = Pomocno.UcitajString("Unesi napomenu : ", "Greška");
            izdatnice.Add(i);
        }


        private List<Osoba> PostaviOsobe()
        {
            List<Osoba> osobe = new List<Osoba>();
            while (Pomocno.ucitajBool("Želite li dodati osobu? (da ili bilo što drugo za ne): "))
            {
                osobe.Add(PostaviOsobu());
            }

            return osobe;
        }

        private Osoba PostaviOsobu()
        {
            izbornik.ObradaOsobe.PrikaziIzbornik();
            int index = Pomocno.ucitajBrojRaspona("Odaberi redni broj polaznika: ", "Nije dobar odabir", 1, izbornik.ObradaOsobe.Osobe.Count());
            return izbornik.ObradaOsobe.Osobe[index - 1];
        }

        private List<Skladistar> PostaviSkladistare()
        {
            List<Skladistar> skladistari = new List<Skladistar>();
            while (Pomocno.ucitajBool("Želite li dodati skladistare? (da ili bilo što drugo za ne): "))
            {
                skladistari.Add(PostaviSkladistara());
            }

            return skladistari;
        }

        private Skladistar PostaviSkladistara()
        {
            izbornik.ObradaSkladistara.PrikaziIzbornik();
            int index = Pomocno.ucitajBrojRaspona("Odaberi redni broj skladistara: ", "Nije dobar odabir", 1, izbornik.ObradaSkladistara.SKladistari.Count());
            return izbornik.ObradaSkladistara.SKladistari[index - 1];

        }

       

        private void PrikazIzdatnica()
        {
            Console.WriteLine("------------------");
            Console.WriteLine("--- Izdatnice ----");
            Console.WriteLine("------------------");
            int b = 1;
            foreach (Izdatnica izdatnica in izdatnice)
            {
                Console.WriteLine("{0}. {1}", b++, izdatnica.BrojIzdatnice);
            }
            Console.WriteLine("------------------");
        }
    }
}
