
using Skladiste.KonzolnaAplikacija.Model;

namespace Skladiste.KonzolnaAplikacija
{
    internal class ObradaSkladistara
    {
        public List<Skladistar> Skladistari { get; }
        public ObradaSkladistara()
        {
            Skladistari = new List<Skladistar>();
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
            Console.WriteLine("1. Pregled postojučih skladištara");
            Console.WriteLine("2. Unos novog skladištara");
            Console.WriteLine("3. Promjena postojučeg skladištara");
            Console.WriteLine("4. Brisanje skladištara");
            Console.WriteLine("5.Povratak na glavni izbornik");
            switch (Pomocno.ucitajBrojRaspon("Odaberite stavku izbornika skladištari",
           "Odabir mora biti 1-5", 1, 5))
            {
                case 1:
                    PregledSkladistar();
                    PrikaziIzbornik();
                    break;
                case 2:
                    UcitajSkladištara();
                    PrikaziIzbornik();
                    break;
                case 3:
                    PromijenaSkladištara();
                    PrikaziIzbornik();
                    break;
                case 4:
                    BrisanjeSkladištara();
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("Gotov rad s osobama");
                    break;




            }
        }

        private void BrisanjeSkladištara()
        {
            PregledSkladistar();
            int index = Pomocno.ucitajBrojRaspon("Odaberi redni broj skladištara: ", "Nije dobar odabir", 1, Skladistari.Count());
            Skladistari.RemoveAt(index - 1);
        }

        private void PromijenaSkladištara()
        {
            PregledSkladistar();
            int index = Pomocno.ucitajBrojRaspon("Odaberi redni broj polaznika: ", "Nije dobar odabir", 1, Polaznici.Count());
            var s = Skladistari[index - 1];
            s.Sifra = Pomocno.ucitajCijeliBroj("Unesite šifru skladištara (" + s.Sifra + "): ",
                "Unos mora biti pozitivni cijeli broj");
            s.Ime = Pomocno.UcitajString("Unesi ime skladištara (" + s.Ime + "): ", "Ime obavezno");
            s.Prezime = Pomocno.UcitajString("Unesi Prezime skladištara (" + s.Prezime + "): ", "Prezime obavezno");
            s.Email = Pomocno.UcitajString("Unesi Email skladištara (" + s.Email + "): ", "Email obavezno");
            s.BrojTelefona = Pomocno.ucitajCijeliBroj("Unesi broj telefona skladištara (" + s.BrojTelefona + "): ", "OIB obavezno");
        }

        private void UcitajSkladištara()
        {
            var s = new Skladistar();
            s.Sifra = Pomocno.ucitajCijeliBroj("Unesite šifru skladištara: ",
                "Unos mora biti pozitivni cijeli broj");
            s.Ime = Pomocno.UcitajString("Unesi ime skladištara: ", "Ime obavezno");
            s.Prezime = Pomocno.UcitajString("Unesi Prezime skladištara: ", "Prezime obavezno");
            s.Email = Pomocno.UcitajString("Unesi Email skladištara: ", "Email obavezno");
            s.BrojTelefona = Pomocno.ucitajCijeliBroj("Unesi broj telefona skladištara: ", "unos obavezno");
            Skladistari.Add(s);
        }

        private void PregledSkladistar()
        {
            Console.WriteLine("------------------");
            Console.WriteLine("-- Skladistari ---");
            Console.WriteLine("------------------");
            int b = 1;
            foreach (Skladistar skladistari in Skladistari)
            {
                Console.WriteLine("{0}. {1}", b++, skladistari);
            }
            Console.WriteLine("------------------");
        }
    }
}

