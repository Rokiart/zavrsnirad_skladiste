namespace SKladisteAppl.Models
{
    public class Izdatnica

    {
        public int IzdatnicaSifra{ get; set; }
        public string BrojIzdatnice{ get; set; }
        public DateTime DatumIzdavanja { get; set; }
        public ICollection<Osoba> Osobe { get; set; }
        public ICollection<Skladistar> Skladistari { get; set; }
        public string Napomena { get; set; }
        public override string ToString()
        {
            return BrojIzdatnice + DatumIzdavanja;
        }
    }
}
