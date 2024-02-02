using Skladiste.KonzolnaAplikacija.Model;

namespace Skladiste.KonzolnaAplikacija
{
    internal class ObradaProizvoda
    {
        public List<Proizvodi> Proizvodi { get; }

        public ObradaProizvoda()
        {
            Proizvodi = new List<Proizvodi>();
            if (Pomocno.dev)
            {
                TestniPodaciProizvoda();
            }
        }

       

        public void PrikaziIzbornik()
        {
            Console.WriteLine("Izbornik za rad s proizvodima");
            Console.WriteLine("1. Pregled postojećih proizvod");
            Console.WriteLine("2. Unos novog proizvoda");
            Console.WriteLine("3. Promjena postojećeg proizvoda");
            Console.WriteLine("4. Brisanje proizvoda");
            Console.WriteLine("5. Povratak na glavni izbornik");
            switch (Pomocno.ucitajBrojRaspon("Odaberite stavku izbornika polaznika: ",
                "Odabir mora biti 1-5", 1, 5))
            {
                case 1:
                    PregledProizvoda();
                    PrikaziIzbornik();
                case 2:
                    UcitajProizvod();
                    PrikaziIzbornik();
                case 3:
                    PromijeniProizvod();
                    PrikaziIzbornik();
                case 4:
                    BrisanjeProizvoda();
                    PrikaziIzbornik();
                case 5:
                    Console.WriteLine("Gotov rad s proizvodima");
                    break;


            }
        }

        private void BrisanjeProizvoda()
        {
            PregledProizvoda();
            int index = Pomocno.ucitajBrojRaspon("Odaberi redni broj proizvoda: ", "Nije dobar odabir", 1, Proizvodi.Count());
            Proizvodi.RemoveAt(index - 1);
        }

        private void PromijeniProizvod()
        {
            PregledProizvoda();
            int index = Pomocno.ucitajBrojRaspon("Odaberi redni broj proizvoda: ", "Nije dobar odabir", 1, Proizvodi.Count());
            var p = Proizvodi[index - 1];
            p.Sifra = Pomocno.ucitajCijeliBroj("Unesite šifra proizvoda (" + p.Sifra + "): ",
              "Unos mora biti pozitivni cijeli broj");
            p.Naziv = Pomocno.UcitajString("Unesi naziv proizvoda (" + p.Ime + "): ", "naziv obavezno");
            p.SifraProizvoda = Pomocno.ucitajCijeliBroj("Unesi šifru proizvoda (" + p.SifraProizvoda + "): ", "naziv obavezno");
            p.MjernaJedinica = Pomocno.UcitajString("Unesi mjernu jedinicu proizvoda (" + p.MjernaJedinica + "): ", "unos obavezan");
            p.Kolicina = Pomocno.ucitajDecimalniBroj("Unesi količinu (" + p.Kolicina + "): ", "unos obavezan");

        }

        private void UcitajProizvod()
        {
            var p = new Proizvod();
            p.Sifra = Pomocno.ucitajCijeliBroj("Unesite šifra proizvoda: ",
                "Unos mora biti pozitivni cijeli broj");
            p.Naziv = Pomocno.UcitajString("Unesi naziv proizvoda: ", "naziv obavezno");
            p.SifraProizvoda = Pomocno.ucitajCijeliBroj("Unesi šifru proizvoda " , "naziv obavezno");
            p.MjernaJedinica = Pomocno.UcitajString("Unesi mjernu jedinicu proizvoda", "unos obavezan");
            p.Kolicina = Pomocno.ucitajDecimalniBroj("Unesi količinu", "Unos obavezan");
            Proizvodi.Add(p);
        }

        private void PregledProizvoda()
        {
            Console.WriteLine("------------------");
            Console.WriteLine("---- Proizvodi ---");
            Console.WriteLine("------------------");
            int b = 1;
            foreach (Proizvodi proizvod in Proizvodi)
            {
                Console.WriteLine("{0}. {1}", b++, proizvod);
            }
            Console.WriteLine("------------------");
        }
        private void TestniPodaciProizvoda()
        {
           Proizvodi.Add(new Proizvodi
           {
               Sifra = 1,
               Naziv = "Lopata",
               SifraProizvoda = 101,
               MjernaJedinica = "kom",
               Kolicina = 5 ,

           }   )

             Proizvodi.Add(new Proizvodi
             {
                 Sifra = 2,
                 Naziv = "Ašov",
                 SifraProizvoda = 102,
                 MjernaJedinica = "kom",
                 Kolicina = 3,

             })
        }

    }

   
}
