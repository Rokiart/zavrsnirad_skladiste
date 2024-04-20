use master;
go
drop database if exists Skladiste;
go
create database Skladiste;
go
alter database Skladiste collate Croatian_CI_AS;
go
use Skladiste;


create table proizvodi(

sifra int not null primary key identity(1,1),
naziv varchar(50) not null,
sifraproizvoda varchar(50) ,
mjernajedinica varchar(20) not null,
kolicina int 
 
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
proizvod int not null references proizvodi,
osoba int not null references osobe(sifra) ,
skladistar int not null references skladistari(sifra) ,
napomena varchar(250)
);

create table operateri(
sifra int not null primary key identity(1,1),
email varchar(50) not null,
lozinka varchar(200) not null

);



-- Lozinka roman generirana pomoæu https://bcrypt-generator.com/
insert into operateri values ('roman.zaric@gmail.com',
'$2a$12$KBZen7ZlWS3W9dLS/kZ9Nem2m5mqQE0cXttfY0yndTkUDhWgEW3N2');




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


insert into izdatnice(brojizdatnice,proizvod,osoba,skladistar) 
values
(104,1,1,1),
(105,3,12,2),
(106,5,19,1),
(107,8,24,2),
(108,15,3,1),
(109,6,8,2),
(110,22,15,1),
(111,13,20,2),
(112,14,5,1),
(113,2,14,2),
(114,21,23,1),
(115,12,11,2),
(116,17,16,1),
(117,18,21,2),
(118,16,7,1),
(119,24,18,2),
(120,22,26,1),
(122,9,29,1),
(123,7,10,2),
(124,16,27,1),
(125,4,28,2),
(126,25,25,1),
(127,21,22,2),
(128,20,9,1),
(129,19,17,2),
(130,1,6,1),
(131,2,13,2),
(132,3,4,1),
(133,4,2,2),
(134,5,1,1);



