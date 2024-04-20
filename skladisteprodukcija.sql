SELECT * FROM izdatniceproizvodi;

INSERT INTO izdatniceproizvodi (proizvod, izdatnica, kolicina)
SELECT TOP 20 
    p.sifra AS proizvod,
    i.sifra AS izdatnica,
    (RAND() * 10 + 1) AS kolicina  -- Generira sluèajan broj izmeðu 1 i 10
FROM 
    proizvodi p
CROSS JOIN 
    izdatnice i
ORDER BY 
    NEWID();

-- Prikazivanje tablice izdatniceproizvodi nakon umetanja novih redaka
SELECT * FROM izdatniceproizvodi;