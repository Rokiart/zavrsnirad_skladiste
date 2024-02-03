

namespace Skladiste.KonzolnaAplikacija.Model
{
    internal class Skladistar : Entitet
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int BrojTelefona { get; set; }
        public string Email { get; internal set; }

        public override string ToString()
        {
            return Ime + " " + Prezime;
        }

    }
}
