﻿using AutoMapper;
using SKladisteAppl.Models;

namespace SKladisteAppl.Mappers;

/// <summary>
/// Klasa Mapper za mapiranje entiteta Izdatnica na DTO-ove
/// </summary>
public class IzdatnicaMapper
{

    /// <summary>
    /// Klasa Mapper za mapiranje entiteta naziv
    /// </summary>
    public static string? naziv { get; private set; }

    /// <summary>
    /// Inicijalizira i konfigurira AutoMapper za mapiranje entiteta Izdatnica na IzdatnicaDTORead
    /// </summary>
    /// <returns>Objekt Mapper za mapiranje Izdatnice na IzdatnicaDTORead</returns>
    public static Mapper InicijalizirajReadToDTO()
    {
        return new Mapper(
            new MapperConfiguration(c =>
            {
                c.CreateMap<Izdatnica, IzdatnicaDTORead>()
                 .ConstructUsing(entitet =>
                     new IzdatnicaDTORead(
                         entitet.Sifra,
                         entitet.BrojIzdatnice,
                         entitet.Datum,
                         entitet.Osoba == null ? "" : (entitet.Osoba.Ime + " " + entitet.Osoba.Prezime).Trim(),
                         entitet.Skladistar == null ? "" : (entitet.Skladistar.Ime + " " + entitet.Skladistar.Prezime).Trim(),
                         entitet.Proizvod,

                         entitet.Napomena));
            })
        );
    }
    /// <summary>
    /// Metoda za inicijalizaciju mapiranja iz DTO za čitanje u entitet Izdatnica.
    /// </summary>
    /// <returns>Mapper za mapiranje IzdatnicaDTORead u Izdatnica</returns>

    public static Mapper InicijalizirajInsertUpdateToDTO()
    {
        return new Mapper(
            new MapperConfiguration(c =>
            {
                c.CreateMap<Izdatnica, IzdatnicaDTOInsertUpdate>()
                 .ConstructUsing(entitet =>
                     new IzdatnicaDTOInsertUpdate(
                         entitet.BrojIzdatnice,
                         entitet.Datum,
                         entitet.Osoba == null ? null : entitet.Osoba.Sifra,

                         entitet.Skladistar == null ? null : entitet.Skladistar.Sifra,
                        
                         entitet.Napomena));
            })
        );
    }
}