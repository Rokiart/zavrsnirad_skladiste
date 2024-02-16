namespace SKladisteAppl.Models
{
    public class Skladistar : Osoba
    {
        //public int Sifra { get; set; }
        //public string Ime { get; set; }
        //public string Prezime { get; set; }
        //public string Email { get; set; }
        //public string BrojTelefona { get; set; }
        public override string ToString()
        {
            return Ime + " " + Prezime;
        }
    }
}
