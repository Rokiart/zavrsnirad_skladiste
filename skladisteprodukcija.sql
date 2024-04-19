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

-- Lozinka roman generirana pomoæu https://bcrypt-generator.com/
insert into operateri values ('roman.zaric@gmail.com',
'$2a$12$tlc2ATQlkYPyp/woj.1fY.cJj.TQGzM1kuazcKBQuObbHE2jOVELy');




insert into skladistari(ime,prezime,brojtelefona,email) 
values
--1
('Roman','Žariæ', 0995906456 , 'roman.zaric@gmail.com'),
--2
('Miroslav','Janiæ', 0998511475 , 'miroslav.janic@gmail.com'),
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
('Pisaè', 'PS011', 'kom'),
('Skener', 'SK012', 'kom'),
('Punjaè za mobitel', 'PM013', 'kom'),
('USB stick', 'US014', 'kom'),
('Eksterni hard disk', 'EH015', 'kom'),
('Torba za laptop', 'TL016', 'kom'),
('Napajanje', 'NP017', 'kom'),
('Monitor stoliæ', 'MS018', 'kom'),
('Mrežni kabel', 'MK019', 'kom'),
('Vezice za kablove', 'VK020', 'kom'),
('Dugmad', 'DG021', 'kom'),
('Papuèe', 'PP022', 'par'),
('Hlaèe', 'HL023', 'kom'),
('Majica', 'MJ024', 'kom'),
('Èarape', 'ÈP025', 'par'),
('Šešir', 'ŠR026', 'kom'),
('Ogrlica', 'OG027', 'kom'),
('Naoèale', 'NC028', 'kom'),
('Sat', 'ST029', 'kom'),
('Kišobran', 'KB030', 'kom'),
('Šal', 'ŠL031', 'kom'),
('Rukavice', 'RK032', 'kom'),
('Košulja', 'KL033', 'kom'),
('Kaput', 'KT034', 'kom'),
('Èizme', 'ÈZ035', 'par'),
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
('Biciklistièka kaciga', 'BK047', 'kom'),
('Ploèa za snowboard', 'PS048', 'kom'),
('Tenisice za trèanje', 'TT049', 'par'),
('Štap za hodanje', 'ŠH050', 'kom');

insert into osobe(prezime,ime,brojtelefona,email) 
values
('Kovaèiæ', 'Ivan', '0991234567', 'ivan.kovacic@gmail.com'),
('Mariæ', 'Ana', '0992345678', 'ana.maric@gmail.com'),
('Horvat', 'Marko', '0993456789', 'marko.horvat@gmail.com'),
('Kneževiæ', 'Ivana', '0994567890', 'ivana.knezevic@gmail.com'),
('Šimiæ', 'Petar', '0995678901', 'petar.simic@gmail.com'),
('Juriæ', 'Marija', '0996789012', 'marija.juric@gmail.com'),
('Pavloviæ', 'Ante', '0997890123', 'ante.pavlovic@gmail.com'),
('Babiæ', 'Martina', '0998901234', 'martina.babic@gmail.com'),
('Živkoviæ', 'Tomislav', '0999012345', 'tomislav.zivkovic@gmail.com'),
('Kovaèeviæ', 'Kristina', '0990123456', 'kristina.kovacevic@gmail.com'),
('Radmanoviæ', 'Luka', '0991234567', 'luka.radmanovic@gmail.com'),
('Matijeviæ', 'Mia', '0992345678', 'mia.matijevic@gmail.com'),
('Barišiæ', 'Filip', '0993456789', 'filip.barisic@gmail.com'),
('Petroviæ', 'Ana', '0994567890', 'ana.petrovic@gmail.com'),
('Tomiæ', 'Ivan', '0995678901', 'ivan.tomic@gmail.com'),
('Horvat', 'Petra', '0996789012', 'petra.horvat@gmail.com'),
('Kovaè', 'Marko', '0997890123', 'marko.kovac@gmail.com'),
('Babiæ', 'Marta', '0998901234', 'marta.babic@gmail.com'),
('Šimiæ', 'Ivan', '0999012345', 'ivan.simic@gmail.com'),
('Juriæ', 'Martina', '0990123456', 'martina.juric@gmail.com'),
('Pavloviæ', 'Luka', '0991234567', 'luka.pavlovic@gmail.com'),
('Kovaèeviæ', 'Lucija', '0992345678', 'lucija.kovacevic@gmail.com'),
('Radmanoviæ', 'Marko', '0993456789', 'marko.radmanovic@gmail.com'),
('Matijeviæ', 'Mia', '0994567890', 'mia.matijevic@gmail.com'),
('Barišiæ', 'Petra', '0995678901', 'petra.barisic@gmail.com'),
('Petroviæ', 'Ante', '0996789012', 'ante.petrovic@gmail.com'),
('Tomiæ', 'Mia', '0997890123', 'mia.tomic@gmail.com'),
('Živkoviæ', 'Ivan', '0998901234', 'ivan.zivkovic@gmail.com'),
('Kneževiæ', 'Petra', '0999012345', 'petra.knezevic@gmail.com');


insert into izdatnice(brojizdatnice,osoba,skladistar,proizvod,kolicina) 
values
(104,1,1,1,5),
(105,12,2,2,10),
(106,19,1,3,3),
(107,24,2,4,8),
(108,3,1,5,6),
(109,8,2,6,4),
(110,15,1,7,7),
(111,20,2,1,2),
(112,5,1,2,9),
(113,14,2,3,1),
(114,23,1,4,5),
(115,11,2,5,3),
(116,16,1,6,7),
(117,21,2,7,8),
(118,7,1,1,4),
(119,18,2,2,6),
(120,26,1,3,10),
(121,30,2,4,2),
(122,29,1,5,5),
(123,10,2,6,7),
(124,27,1,7,3),
(125,28,2,1,8),
(126,25,1,2,9),
(127,22,2,3,2),
(128,9,1,4,6),
(129,17,2,5,4),
(130,6,1,6,5),
(131,13,2,7,7),
(132,4,1,1,3),
(133,2,2,2,2),
(134,1,1,3,4);

