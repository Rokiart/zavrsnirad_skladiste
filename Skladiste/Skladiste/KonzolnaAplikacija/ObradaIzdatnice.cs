﻿using Skladiste.KonzolnaAplikacija.Model;

namespace Skladiste.KonzolnaAplikacija
{
    internal class ObradaIzdatnice
    {
        public List<Izdatnica> Izdatnice { get; }

        private Izbornik Izbornik;

        public ObradaIzdatnice(Izbornik izbornik):this()
        {
            this.Izbornik = izbornik;
        }

        public ObradaIzdatnice()
        {
            Izdatnice = new List<Izdatnica>();
        }

        public void PrikaziIzbornik()
        {
            Console.WriteLine("Izbornik za rad sa izdatnicama ");
            Console.WriteLine("1. Pregled postoječih izdatnica ");
            Console.WriteLine("2. Unos nove izdatnice ");
            Console.WriteLine("3. Promjena postoječe izdatnice");
            Console.WriteLine("4. Brisanje izdatnice");
            Console.WriteLine("5. Povratak na glavni izbornik");
            switch(Pomocno.ucitajBrojRaspon("Odaberite stavku izbornika : ",
                "Odabir mora biti 1-5",1, 5))
            {
                case 1:
                    PrikaziIzdatnice();
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
                    Console.WriteLine("Zavrsen rad sa izdatnicama");
                    break;

            }
        }

        private void BrisanjeIzdatnice()
        {
           PrikaziIzdatnice();
            int index = Pomocno.ucitajBrojRaspon("Odaberi redni broj izdatnice: ", "Nije dobar odabir", 1, Izdatnice.Count());
            Izdatnice.RemoveAt(index - 1);
        }

        private void PromjenaIzdatnice()
        {
            PrikaziIzdatnice();
            int index = Pomocno.ucitajBrojRaspon("Odaberi redni broj izdatnice : ", "Nije dobar odabir", 1, Izdatnice.Count());
            var i = Izdatnice[index - 1];
            i.Sifra = Pomocno.ucitajCijeliBroj("Unesite šifru izdatnice (" + i.Sifra + "): ",
                "Unos mora biti pozitivni cijeli broj");
            i.DatumIzdatnice = Pomocno.ucitajDatum("Unesi datum  u formatu dd.MM.yyyy.", "Greška");
            i.BrojIzdatnice = Pomocno.ucitajCijeliBroj("Unesite broj izdatnice (" + i.BrojIzdatnice + "): ",
                "Unos obavezan");
            Console.WriteLine("Trenutni broj izdatnice : {0}",i.Osoba.Ime );
            i.Osoba = PostaviOsobu();
            Console.WriteLine("Trenutne Osobe:");
            Console.WriteLine("------------------");
            Console.WriteLine("---- Osobe ----");
            Console.WriteLine("------------------");
            int b = 1;
            foreach (Skladistar skladistar in i.Skladistari)

            {
                Console.WriteLine("{0}. {1}", b++, skladistar);
            }
            Console.WriteLine("------------------");
            i.Skladistari = PostaviSkladistare();

        }

        private List<Skladistar> PostaviSkladistare()
        {
            List<Skladistar> skladistars = new List<Skladistar>();
            while (Pomocno.ucitajBool("Želite li dodati skladištare? (da ili bilo što drugo za ne): "))
            {
                skladistars.Add(PostaviSkladistara());
            }

            return skladistars;
        }

        private Skladistar PostaviSkladistara()
        {
            Izbornik.ObradaSkladistara.PregledSkladistara();
            int index = Pomocno.ucitajBrojRaspon("Odaberi redni broj polaznika: ", "Nije dobar odabir", 1, Izbornik.ObradaSkladistara.Skladistari.Count());
            return Izbornik.ObradaSkladistara.Skladistari[index - 1];
        }

        private void UnosNoveIzdatnice()
        {
            var i = new Izdatnica();
            i.Sifra = Pomocno.ucitajCijeliBroj("Unesite šifru izdatnice : ",
            "Unos mora biti pozitivan cijeli broj");
            i.DatumIzdatnice = Pomocno.ucitajDatum("Unesi datum u formatu dd.MM.yyyy.", "Greška");
                i.BrojIzdatnice = Pomocno.ucitajCijeliBroj("Unesite broj izdatnice : ",
                "Unos obavezan");

            i.Osoba = PostaviOsobu();
            i.Skladistari = PostaviSkladistare();
            Izdatnice.Add(i);





        }

        private List<Osoba> PostaviOsobe()
        {
            List<Osoba> osobe = new List<Osoba>();
            while(Pomocno.ucitajBool("Želite dodat Osobu ? (upište da bilo šta drugo je ne): "))
            {
                osobe.Add(PostaviOsobe());
            }
            return osobe;

        }
        private Osoba PostaviOsobu()
        {
            Izbornik.ObradaOsobe.PregledOsobe();
            int index = Pomocno.ucitajBrojRaspon("Odaber redni broj osobe", 1, Izbornik.ObradaOsobe.Osobe.Count());
            return Izbornik.ObradaOsobe.Osobe[index - 1];
        }

        private void PrikaziIzdatnice()
        {
            Console.WriteLine("---------------------");
            Console.WriteLine("----- Izdatnice -----");
            Console.WriteLine("---------------------");
            int b = 1;
            foreach(Izdatnica izdatnica in Izdatnice )
            {
                Console.WriteLine("{0}. {1}",b++,izdatnica.BrojIzdatnice);
            }
            Console.WriteLine("---------------------");
        }

       
    }
}
