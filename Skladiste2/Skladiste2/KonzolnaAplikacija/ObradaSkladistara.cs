using Skladiste2.KonzolnaAplikacija.Model;

namespace Skladiste2.KonzolnaAplikacija
{
    internal class ObradaSkladistara
    {
        public List<Skladistar> SKladistari { get;  }

        public ObradaSkladistara()
        {
            SKladistari = new List<Skladistar>();
            if (Pomocno.dev)
            {
                TestniPodaci();

            }
        }

        private void TestniPodaci()
        {
            SKladistari.Add(new Model.Skladistar
            {
                Sifra = 1,
                Ime = "Roman",
                Prezime = "Žarić",
                Email = "roman.zaric@gmail.com",
                BrojTelefona = "0995906456"
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
            switch (Pomocno.ucitajBrojRaspona("Odaberite stavku izbornika skladištari : ",
                 "Odabir mora biti 1-5", 1, 5))
            {
                case 1:
                    PregledSkladistara();
                    PrikaziIzbornik();
                    break;
                case 2:
                    UnosNovogSkladistara();
                    PrikaziIzbornik();
                    break;
                case 3:
                    IzmjenaSkladištara();
                    PrikaziIzbornik();
                    break;
                case 4:
                    BrisanjeSkladistara();
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("Gotov rad s skladištarima");
                    break;

            }
                
        }

        private void BrisanjeSkladistara()
        {
            PregledSkladistara();
            int index = Pomocno.ucitajBrojRaspona("Odaberi redni broj skladištara : ", "Nije dobar odabir", 1, SKladistari.Count());
            SKladistari.RemoveAt(index - 1);
        }

        private void IzmjenaSkladištara()
        {
            PregledSkladistara();
            int index = Pomocno.ucitajBrojRaspona("Odaberi redni broj skladištara : ", "Nije dobar odabir", 1, SKladistari.Count());
            var s = SKladistari[index - 1];
            s.Sifra = Pomocno.ucitajCijeliBroj("Unesite šifra Skladištara :  (" + s.Sifra + "): ",
                "Unos mora biti pozitivni cijeli broj");
            s.Ime = Pomocno.UcitajString("Unesi ime skladištara : (" + s.Ime + "): ", "Ime obavezno");
            s.Prezime = Pomocno.UcitajString("Unesi Prezime skladištara : (" + s.Prezime + "): ", "Prezime obavezno");
            s.BrojTelefona = Pomocno.UcitajString("Unesi broj telefona skladištara : (" + s.BrojTelefona + "): ", "unos obavezno");
            s.Email = Pomocno.UcitajString("Unesi Email skladištara : (" + s.Email + "): ", "unos obavezno");
        }

        private void UnosNovogSkladistara()
        {
            var o = new Skladistar();
            o.Sifra = Pomocno.ucitajCijeliBroj("Unesite šifru skladištara : ",
                "Unos mora biti pozitivni cijeli broj");
            o.Ime = Pomocno.UcitajString("Unesi ime skladištara :  ", "Obavezan unos");
            o.Prezime = Pomocno.UcitajString("Unesi prezime skladištara :  ", "Obavezan unos");
            o.Email = Pomocno.UcitajString("Unesi Email skladištara : ", "Obavezan unos");
            o.BrojTelefona = Pomocno.UcitajString("Unesi BrojTelefona skladištara :  ", "Obavezan unos");
            SKladistari.Add(o);
        }

        private void PregledSkladistara()
        {
            Console.WriteLine("------------------");
            Console.WriteLine("-- Skladištari ---");
            Console.WriteLine("------------------");
            int b = 1;
            foreach (Skladistar skladistar in SKladistari)
            {
                Console.WriteLine("{0}. {1}", b++, skladistar);
            }
            Console.WriteLine("------------------");
        }
    }

       
}
