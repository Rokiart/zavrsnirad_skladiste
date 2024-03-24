using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;


namespace SKladisteAppl.Models
{

    /// <summary>
    /// Predstavlja podatke o osobi za čitanje.
    /// </summary>
    public record OsobaDTORead(int sifra, string ime, string prezime,
        string brojtelefona, string email);

    /// <summary>
    /// Predstavlja podatke o osobi za unos i ažuriranje.
    /// </summary>
    public record OsobaDTOInsertUpdate(string ime, string prezime,
        string brojtelefona, string email);

    /// <summary>
    /// Predstavlja podatke o proizvodu za čitanje.
    /// </summary>
    public record ProizvodDTORead(int sifra, string naziv, string sifraproizvoda,
        string mjernajedinica);

    /// <summary>
    /// Predstavlja podatke o proizvodu za unos i ažuriranje.
    /// </summary>
    public record ProizvodDTOInsertUpdate(string naziv, string sifraproizvoda,
        string mjernajedinica);

    /// <summary>
    /// Predstavlja podatke o skladištaru za čitanje.
    /// </summary>
    public record SkladistarDTORead(int? sifra, string ime, string prezime,
        string brojtelefona, string email);

    /// <summary>
    /// Predstavlja podatke o skladištaru za unos i ažuriranje.
    /// </summary>
    public record SkladistarDTOInsertUpdate(string ime, string prezime,
        string brojtelefona, string email);

    /// <summary>
    /// Predstavlja podatke o izdatnici za čitanje.
    /// </summary>
    public record IzdatnicaDTORead(int sifra, string? brojIzdatnice,
        DateTime? datum, string? osobaImePrezime, string? skladistarImePrezime,
        string napomena);

    /// <summary>
    /// Predstavlja podatke o izdatnici za unos i ažuriranje.
    /// </summary>
    public record IzdatnicaDTOInsertUpdate(string? brojizdatnice,
        DateTime? datum, int? osobasifra, int? skladistarSifra, string napomena);
    
}

