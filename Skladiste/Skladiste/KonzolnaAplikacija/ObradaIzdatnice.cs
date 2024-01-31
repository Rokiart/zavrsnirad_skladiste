using Skladiste.KonzolnaAplikacija.Model;

namespace Skladiste.KonzolnaAplikacija
{
    internal class ObradaIzdatnice
    {
        public List<Izdatnice> Izdatnices { get; }

        private Izbornik Izbornik;

        public ObradaIzdatnice(Izbornik izbornik):this()
        {
            this.Izbornik = izbornik;
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
                "Odabir mora biti 1-5"1, 5))
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

        private void UnosNoveIzdatnice()
        {
            var i = new Izdatnice();
            i.Sifra = Pomocno.ucitajCijeliBroj("Unesitešifru izdatnice : "),
                "Unos mora biti pozitivan cijeli broj");
            i.BrojIzdatnice = Pomocno.ucitajCijeliBroj("Unesite broj izdatnice : ")
                "Unos obavezan");
            i.DatumIzdavanja = Pomocno.UcitajDatum("Unesite datum");
            i.Osobe = PostaviOsobe();
            i.Skladistar = PosrtaviSkladistara();

        }

        private List<Osoba> PostaviOsobe()
        {
            List<Osoba> osobe = new list<Osoba>();
            while(Pomocno.ucitajBool("Želite dodat Osobu ? (upište da bilo šta drugo je ne): "))
            {
                osobe.Add(PostaviOsobe());
            }
            return osobe;

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

        private void PromjenaIzdatnice()
        {
            PrikaziIzdatnice();
            int index = 

        }
    }
}
