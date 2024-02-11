using Skladiste2.KonzolnaAplikacija.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Skladiste2.KonzolnaAplikacija
{
    internal class ObradaOsobe
    {
        public List<Osoba> Osobe { get; }
        

        public ObradaOsobe()
        {
            
            Osobe = new List<Osoba>();
            if (Pomocno.dev)
            {
                TestniPodaci();
            }
        }

        private void TestniPodaci()
        {
            Osobe.Add(new Osoba()
            {
                Sifra = 1,
                Ime = "Dragica",
                Prezime = "Žarić",
                Email = "dra@gmail.com",
                BrojTelefona = "868486472822"
            });

            Osobe.Add(new Osoba()
            {
                Sifra = 2,
                Ime = "Michael",
                Prezime = "Žarić",
                Email = "miki@gmail.com",
                BrojTelefona = "35854872821"
            });
        }

        public void PrikaziIzbornik()
            
        {
            Console.WriteLine("**************************************************");
            Console.WriteLine(" Izbornik za rad s osobama ");
            Console.WriteLine("1. Pregled postojučih osoba ");
            Console.WriteLine("2. Unos nove osobe : ");
            Console.WriteLine("3. Promjena postojuče osobe : ");
            Console.WriteLine("4. Brisanje osobe : ");
            Console.WriteLine("5. Povratak na glavni izbornik ");
            switch (Pomocno.ucitajBrojRaspona("Odaberite stavku izbornika osobe : ",
                "Odabir mora biti 1-5", 1, 5))
            {
                case 1:
                    PregledOsoba();
                    PrikaziIzbornik();
                    break;
                case 2:
                    UcitajOsobu();
                    PrikaziIzbornik();
                    break;
                case 3:
                    PromjenaOsobe();
                    PrikaziIzbornik();
                    break;
                case 4:
                    BrisanjeOsobe();
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("Gotov rad s osobama ");
                    break;

            }
        }

        public void BrisanjeOsobe()
        {
            Console.WriteLine("**************************************************");
            PregledOsoba();
            int index = Pomocno.ucitajBrojRaspona("Odaberi redni broj osobe: ", "Nije dobar odabir", 1, Osobe.Count());
            Osobe.RemoveAt(index - 1);

        }

        public void PromjenaOsobe()
        {
            Console.WriteLine("**************************************************");
            PregledOsoba();
            int index = Pomocno.ucitajBrojRaspona("Odaberi redni broj osobe",
                "Nije dobar odabir", 1, Osobe.Count());
            var o = Osobe[index - 1];
            o.Sifra = Pomocno.ucitajCijeliBroj("Unesite šifru osobe (" + o.Sifra + ") : ",
                 "Unos mora biti pozitivni cijeli broj");
            o.Ime = Pomocno.UcitajString("Unesi ime osobe (" + o.Ime + ") : ", "Ime obavezno");
            o.Prezime = Pomocno.UcitajString("Unesi Prezime osobe (" + o.Prezime + ") : ", "Prezime obavezno");
            o.Email = Pomocno.UcitajString("Unesi Email osobe (" + o.Email + ") : ", "Email obavezno");
            o.BrojTelefona = Pomocno.UcitajString("Unesi broj telefona osobe (" + o.BrojTelefona + ") : ", "broj telefona obavezno");
        }

        public void UcitajOsobu()
        {
            Console.WriteLine("**************************************************");
            var o = new Osoba();
            o.Sifra = Pomocno.ucitajCijeliBroj("Unesite šifru osobe : ",
                "Unos mora biti pozitivni cijeli broj");
            o.Ime = Pomocno.UcitajString("Unesi ime : ", "Obavezan unos");
            o.Prezime = Pomocno.UcitajString("Unesi prezime : ", "Obavezan unos");
            o.Email = Pomocno.UcitajString("Unesi Email osobe : ", "Obavezan unos");
            o.BrojTelefona = Pomocno.UcitajString("Unesi BrojTelefona osobe : ", "Obavezan unos");
            Osobe.Add(o);
           
        }

        public void PregledOsoba()
        {
            Console.WriteLine("---------------------");
            Console.WriteLine("-----   Osobe   -----");
            Console.WriteLine("---------------------");
            int b = 1;
            foreach (Osoba osoba in Osobe)
            {
                Console.WriteLine("{0}.{1}", b++, osoba);
            }
            Console.WriteLine("--------------------");
        }
    }
}
