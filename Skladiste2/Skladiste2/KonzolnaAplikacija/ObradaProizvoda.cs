using Skladiste2.KonzolnaAplikacija.Model;

namespace Skladiste2.KonzolnaAplikacija
{
    internal class ObradaProizvoda
    {
       

        public List<Proizvod> Proizvodi{ get; }
        

        public ObradaProizvoda()
        {
            Proizvodi = new List<Proizvod>();
            if (Pomocno.dev)
            {
                TestniPodaci();
            }
        }

        private void TestniPodaci()
        {
            
            Proizvodi.Add(new Proizvod
            {
                Sifra = 1,
                Naziv = "Lopata",
                Sifraproizvoda = "123 456",
                MjernaJedinica ="agal@gmail.com",
                Kolicina = 3
            });
            Proizvodi.Add(new Proizvod
            {
                Sifra = 2,
                Naziv = "Ašov",
                Sifraproizvoda = "133 456",
                MjernaJedinica = "ffb@gmail.com",
                Kolicina = 6
            });
        }


        public void PrikaziIzbornik()
        {
            Console.WriteLine("Izbornik za rad s proizvodima");
            Console.WriteLine("1. Pregled postojećih proizvod");
            Console.WriteLine("2. Unos novog proizvoda");
            Console.WriteLine("3. Promjena postojećeg proizvoda");
            Console.WriteLine("4. Brisanje proizvoda");
            Console.WriteLine("5. Povratak na glavni izbornik");
            switch (Pomocno.ucitajBrojRaspona("Odaberite stavku izbornika proizvodi : ",
               "Odabir mora biti 1-5", 1, 5))
            {
                case 1:
                    PregledProizvoda();
                    PrikaziIzbornik();
                    break;
                case 2:
                    UnosProizvoda();
                    PrikaziIzbornik();
                    break;
                case 3:
                    IzmjenaProizvoda();
                    PrikaziIzbornik();
                    break;
                case 4:
                    ObrisiProizvod();
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("Gotov rad s proizvodima");
                    break;


            }
        }

        private void ObrisiProizvod()
        {
            PregledProizvoda();
            int index = Pomocno.ucitajBrojRaspona("Odaberi redni broj proizvoda : ", "Nije dobar odabir", 1, Proizvodi.Count());
            Proizvodi.RemoveAt(index - 1);
        }

        private void IzmjenaProizvoda()
        {
            PregledProizvoda();
            int index = Pomocno.ucitajBrojRaspona("Odaberi redni broj proizvoda",
                "Nije dobar odabir", 1, Proizvodi.Count());
            var o = Proizvodi[index - 1];
            o.Sifra = Pomocno.ucitajCijeliBroj("Unesite šifru proizvoda (" + o.Sifra + ") : ",
                 "Unos mora biti pozitivni cijeli broj");
            o.Naziv = Pomocno.UcitajString("Unesi naziv proizvoda : (" + o.Naziv + ") : ", "unos obavezno");
            o.Sifraproizvoda = Pomocno.UcitajString("Unesi šifru proizvoda :  (" + o.Sifraproizvoda + ") : ", "unos obavezno");
            o.MjernaJedinica = Pomocno.UcitajString("Unesi mjernu jedinicu proizvoda :  (" + o.MjernaJedinica + ") : ", "unos obavezno");
            o.Kolicina = Pomocno.ucitajCijeliBroj("Unesi količinu proizvoda :  (" + o.Kolicina + ") : ", "unos obavezno");
        }

        private void UnosProizvoda()
        {
            var o = new Proizvod();
            o.Sifra = Pomocno.ucitajCijeliBroj("Unesite šifru proizvoda : ",
                "Unos mora biti pozitivni cijeli broj");
            o.Naziv = Pomocno.UcitajString("Unesi naziv proizvod :  ", "Obavezan unos");
            o.Sifraproizvoda = Pomocno.UcitajString("Unesi šifru  proizvod :  ", "Obavezan unos");
            o.MjernaJedinica= Pomocno.UcitajString("Unesi mjernu jedinicu proizvod : ", "Obavezan unos");
            o.Kolicina = Pomocno.ucitajCijeliBroj("Unesi kolicinu proizvod :  ", "Obavezan unos");
            Proizvodi.Add(o);
        }

        private void PregledProizvoda()
        {
            Console.WriteLine("---------------------");
            Console.WriteLine("----- Proizvodi -----");
            Console.WriteLine("---------------------");
            int b = 1;
            foreach (Proizvod proizvod  in Proizvodi)
            {
                Console.WriteLine("{0}.{1}", b++, proizvod);
            }
            Console.WriteLine("--------------------");
        }
    }
}
