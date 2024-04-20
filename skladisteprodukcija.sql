SELECT name, collation_name FROM sys.databases;
GO
-- Doma primjeniti na ime svoje baze 3 puta
ALTER DATABASE db_aa599c_romanzaric SET SINGLE_USER WITH
ROLLBACK IMMEDIATE;
GO
ALTER DATABASE db_aa599c_romanzaric COLLATE Croatian_CI_AS;
GO
ALTER DATABASE db_aa599c_romanzaric SET MULTI_USER;
GO
SELECT name, collation_name FROM sys.databases;
GO

drop table proizvodi;
drop table skladistari;
drop table osobe;
drop table izdatnice;
drop table izdatniceproizvodi;
drop table operateri;







create table proizvodi(

sifra int not null primary key identity(1,1),
naziv varchar(50) not null,
sifraproizvoda varchar(50) ,
mjernajedinica varchar(20) not null
 
); 

create table skladistari(

sifra int not null primary key identity(1,1),
ime varchar(50) not null,
prezime varchar(50) not null,
email varchar(50) not null,
brojtelefona varchar(20)not null
);

create table osobe(

sifra int not null primary key identity(1,1),
ime varchar(50) not null,
prezime varchar(50) not null,
email varchar(50),
brojtelefona varchar(20)
);

create table izdatnice(

sifra int not null primary key identity(1,1),
brojizdatnice varchar (50) not null,
datum datetime,
osoba int not null references osobe(sifra) ,
skladistar int not null references skladistari(sifra) ,
napomena varchar(250)
);

create table operateri(
sifra int not null primary key identity(1,1),
email varchar(50) not null,
lozinka varchar(200) not null

);

create table izdatniceproizvodi (

sifra int not null primary key identity(1,1),
proizvod int not null references proizvodi(sifra),
izdatnica int not null references izdatnice(sifra),
kolicina int
);

-- Lozinka roman generirana pomoću https://bcrypt-generator.com/
insert into operateri values ('roman.zaric@gmail.com',
'$2a$12$KBZen7ZlWS3W9dLS/kZ9Nem2m5mqQE0cXttfY0yndTkUDhWgEW3N2');




insert into skladistari(ime,prezime,brojtelefona,email) 
values
--1
('Roman','Žarić', 0995906456 , 'roman.zaric@gmail.com'),
--2
('Miroslav','Janić', 0998511475 , 'miroslav.janic@gmail.com'),
--3
('Tomislav','Jakopec', 099565656 , 'tomislav.jakopec@gmail.com');

insert into proizvodi(naziv,sifraproizvoda,mjernajedinica) 
values
('Lopta', 'LP001', 'kom'),
('Bicikl', 'BC002', 'kom'),
('Monitor', 'MN003', 'kom'),
('Tipkovnica', 'TK004', 'kom'),
('Miš', 'MS005', 'kom'),
('Tastatura', 'TS006', 'kom'),
('USB kabel', 'UK007', 'kom'),
('Slušalice', 'SL008', 'kom'),
('Mikrofon', 'MK009', 'kom'),
('HDMI kabel', 'HK010', 'kom'),
('Pisač', 'PS011', 'kom'),
('Skener', 'SK012', 'kom'),
('Punjač za mobitel', 'PM013', 'kom'),
('USB stick', 'US014', 'kom'),
('Eksterni hard disk', 'EH015', 'kom'),
('Torba za laptop', 'TL016', 'kom'),
('Napajanje', 'NP017', 'kom'),
('Monitor stolić', 'MS018', 'kom'),
('Mrežni kabel', 'MK019', 'kom'),
('Vezice za kablove', 'VK020', 'kom'),
('Dugmad', 'DG021', 'kom'),
('Papuče', 'PP022', 'par'),
('Hlače', 'HL023', 'kom'),
('Majica', 'MJ024', 'kom'),
('Čarape', 'ČP025', 'par'),
('Šešir', 'ŠR026', 'kom'),
('Ogrlica', 'OG027', 'kom'),
('Naočale', 'NC028', 'kom'),
('Sat', 'ST029', 'kom'),
('Kišobran', 'KB030', 'kom'),
('Šal', 'ŠL031', 'kom'),
('Rukavice', 'RK032', 'kom'),
('Košulja', 'KL033', 'kom'),
('Kaput', 'KT034', 'kom'),
('Čizme', 'ČZ035', 'par'),
('Cipele', 'CP036', 'par'),
('Haljina', 'HJ037', 'kom'),
('Suknja', 'SK038', 'kom'),
('Košarkaška lopta', 'KL039', 'kom'),
('Teniska loptica', 'TL040', 'kom'),
('Bazen', 'BZ041', 'kom'),
('Daska za surfanje', 'DS042', 'kom'),
('Roleri', 'RL043', 'par'),
('Kajak', 'KJ044', 'kom'),
('Šator', 'ST045', 'kom'),
('Roleri', 'RL046', 'par'),
('Biciklistička kaciga', 'BK047', 'kom'),
('Ploča za snowboard', 'PS048', 'kom'),
('Tenisice za trčanje', 'TT049', 'par'),
('Štap za hodanje', 'ŠH050', 'kom');

insert into osobe(prezime,ime,brojtelefona,email) 
values
('Kovačić', 'Ivan', '0991234567', 'ivan.kovacic@gmail.com'),
('Marić', 'Ana', '0992345678', 'ana.maric@gmail.com'),
('Horvat', 'Marko', '0993456789', 'marko.horvat@gmail.com'),
('Knežević', 'Ivana', '0994567890', 'ivana.knezevic@gmail.com'),
('Šimić', 'Petar', '0995678901', 'petar.simic@gmail.com'),
('Jurić', 'Marija', '0996789012', 'marija.juric@gmail.com'),
('Pavlović', 'Ante', '0997890123', 'ante.pavlovic@gmail.com'),
('Babić', 'Martina', '0998901234', 'martina.babic@gmail.com'),
('Živković', 'Tomislav', '0999012345', 'tomislav.zivkovic@gmail.com'),
('Kovačević', 'Kristina', '0990123456', 'kristina.kovacevic@gmail.com'),
('Radmanović', 'Luka', '0991234567', 'luka.radmanovic@gmail.com'),
('Matijević', 'Mia', '0992345678', 'mia.matijevic@gmail.com'),
('Barišić', 'Filip', '0993456789', 'filip.barisic@gmail.com'),
('Petrović', 'Ana', '0994567890', 'ana.petrovic@gmail.com'),
('Tomić', 'Ivan', '0995678901', 'ivan.tomic@gmail.com'),
('Horvat', 'Petra', '0996789012', 'petra.horvat@gmail.com'),
('Kovač', 'Marko', '0997890123', 'marko.kovac@gmail.com'),
('Babić', 'Marta', '0998901234', 'marta.babic@gmail.com'),
('Šimić', 'Ivan', '0999012345', 'ivan.simic@gmail.com'),
('Jurić', 'Martina', '0990123456', 'martina.juric@gmail.com'),
('Pavlović', 'Luka', '0991234567', 'luka.pavlovic@gmail.com'),
('Kovačević', 'Lucija', '0992345678', 'lucija.kovacevic@gmail.com'),
('Radmanović', 'Marko', '0993456789', 'marko.radmanovic@gmail.com'),
('Matijević', 'Mia', '0994567890', 'mia.matijevic@gmail.com'),
('Barišić', 'Petra', '0995678901', 'petra.barisic@gmail.com'),
('Petrović', 'Ante', '0996789012', 'ante.petrovic@gmail.com'),
('Tomić', 'Mia', '0997890123', 'mia.tomic@gmail.com'),
('Živković', 'Ivan', '0998901234', 'ivan.zivkovic@gmail.com'),
('Knežević', 'Petra', '0999012345', 'petra.knezevic@gmail.com');


insert into izdatnice(brojizdatnice,osoba,skladistar) 
values
(104,1,1),
(105,12,2),
(106,19,1),
(107,24,2),
(108,3,1),
(109,8,2),
(110,15,1),
(111,20,2),
(112,5,1),
(113,14,2),
(114,23,1),
(115,11,2),
(116,16,1),
(117,21,2),
(118,7,1),
(119,18,2),
(120,26,1),
(122,29,1),
(123,10,2),
(124,27,1),
(125,28,2),
(126,25,1),
(127,22,2),
(128,9,1),
(129,17,2),
(130,6,1),
(131,13,2),
(132,4,1),
(133,2,2),
(134,1,1);


insert into izdatniceproizvodi(proizvod,izdatnica,kolicina) 
values
(2,178,3),(1,178,5),(3,178,3),(4,178,6),(7,178,2);

