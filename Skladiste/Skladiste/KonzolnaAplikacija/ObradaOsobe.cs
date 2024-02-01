
using Skladiste.KonzolnaAplikacija.Model;

namespace Skladiste.KonzolnaAplikacija
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

      

        public void PrikaziIzbornik()
    {
        Console.WriteLine("Izbornik za rad s osobama");
        Console.WriteLine("1. Pregled postojučih osoba");
        Console.WriteLine("2. Unos nove osobe");
        Console.WriteLine("3. Promjena postojuče osobe");
        Console.WriteLine("4. Brisanje osobe");
        Console.WriteLine("5.Povratak na glavni izbornik");
        switch (Pomocno.ucitajBrojRaspon("Odaberite stavku izbornika osobe",
           "Odabir mora biti 1-5",1,5 ))
        {
            case 1:
                PregledOsoba();
                PrikaziIzbornik();
            case 2:
                UcitajOsobu();
                PrikaziIzbornik();
            case 3:
                PromijenaOsobe();
                PrikaziIzbornik();
                case 4:
                    BrisanjeOsobe();
                    PrikaziIzbornik();
                case 5:
                    Console.WriteLine("Gotov rad s osobama");
                    break;




            }
    }

        private void BrisanjeOsobe()
        {
            PregledOsoba();
            int index = Pomocno.ucitajBrojRaspon("Odaberi redni broj osobe: ", "Nije dobar odabir", 1, Osobe.Count());
            Osobe.RemoveAt(index - 1);
        }

        private void PromijenaOsobe()
    {
        PregledOsoba();
        int index = Pomocno.ucitajBrojRaspon("Odaberi redni broj osobe",
            "Nije dobar odabir", 1, Osobe.Count());
        var o = Osobe[index - 1];
        o.Sifra = Pomocno.ucitajCijeliBroj("Unesite šifru osobe (" + o.Sifra + "): ",
             "Unos mora biti pozitivni cijeli broj");
        o.Ime = Pomocno.UcitajString("Unesi ime osobe (" + o.Ime + "): ", "Ime obavezno");
        o.Prezime = Pomocno.UcitajString("Unesi Prezime osobe (" + o.Prezime + "): ", "Prezime obavezno");
        o.Email = Pomocno.UcitajString("Unesi Email osobe (" + o.Email + "): ", "Email obavezno");
        o.BrojTelefona = Pomocno.UcitajString("Unesi broj telefona osobe (" + o.BrojTelefona + "): ", "broj telefona obavezno");
    }

    private void UcitajOsobu()
    {
        var o = new Osoba();
        o.Sifra = Pomocno.ucitajCijeliBroj("Unesite šifru osobe",
            "Unos mora biti pozitivni cijeli broj");
        o.Ime = Pomocno.UcitajString("Unesi ime ", "Obavezan unos");
        o.Prezime = Pomocno.UcitajString("Unesi prezime ", "Obavezan unos");
        o.Email = Pomocno.UcitajString("Unesi Email polaznika" );
        o.BrojTelefona = Pomocno.UcitajString("Unesi BrojTelefona polaznika ");
        Osobe.Add(o);





    }

        private void TestniPodaci()
        {
            Polaznici.Add(new Polaznik
            {
                Sifra = 1,
                Ime = "Dragica",
                Prezime = "Žarić",
                Email = "dra@gmail.com",
                BrojTelefona = "868486472822"
            });

            Polaznici.Add(new Polaznik
            {
                Sifra = 2,
                Ime = "Michael",
                Prezime = "Žarić",
                Email = "miki@gmail.com",
                BrojTelefona = "35854872821"
            });
        }

        private void PregledOsoba()
    {
        Console.WriteLine("---------------------");
        Console.WriteLine("-----   Osobe   -----");
        Console.WriteLine("---------------------");
        int b = 1;
        foreach (Osoba osoba in Osobe)
        {
            Console.WriteLine("{0}.{1}", b++ , osoba);
        }
        Console.WriteLine("-------------------------");
    }
}

public class Osoba
{
    internal string Prezime;
    internal string Email;
    internal string BrojTelefona;

    public string Ime { get; internal set; }
    public int Sifra { get; internal set; }
}
}