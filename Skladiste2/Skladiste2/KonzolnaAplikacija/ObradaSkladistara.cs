using Skladiste2.KonzolnaAplikacija.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Skladiste2.KonzolnaAplikacija
{
    internal class ObradaSkladistara
    {
        public List<Skladistar> Skladistar { get;  }

        public ObradaSkladistara()
        {
            Skladistar = new list<Skladistar>();
            if (Pomocno.dev)
            {
                TestniPodaci();

            }
        }

        private void TestniPodaci()
        {
            Skladistari.Add(new Model.Skladistar
            {
                Sifra = 1,
                Ime = "Roman",
                Prezime = "Žarić",
                Email = "roman.zaric@gmail.com",
                BrojTelefona = 0995906456
            });
        }

        public void PrikaziIzbornik()
        {
            Console.WriteLine("Izbornik za rad s skladištarima");
            Console.WriteLine("1. Pregled postojučih skladištara : ");
            Console.WriteLine("2. Unos novog skladištara : ");
            Console.WriteLine("3. Promjena postojučeg skladištara : ");
            Console.WriteLine("4. Brisanje skladištara : ");
            Console.WriteLine("5. Povratak na glavni izbornik");
            switch (Pomocno.ucitajBrojRaspona"Odaberite stavku izbornika skladištari : ",
                 "Odabir mora biti 1-5", 1, 5))
            {
                case 1:
                    PregledSkladistara();
                    PrikaziIzbornik();
                    break;
            }
                
        }
    }

        private void PregledSkladistara()
        {
            var o = new Osoba();
            o.Sifra = Pomocno.ucitajCijeliBroj("Unesite šifru osobe",
                "Unos mora biti pozitivni cijeli broj");
            o.Ime = Pomocno.UcitajString("Unesi ime ", "Obavezan unos");
            o.Prezime = Pomocno.UcitajString("Unesi prezime ", "Obavezan unos");
            o.Email = Pomocno.UcitajString("Unesi Email polaznika");
            o.BrojTelefona = Pomocno.UcitajString("Unesi BrojTelefona polaznika ");
            Osobe.Add(o);
        }
    }
