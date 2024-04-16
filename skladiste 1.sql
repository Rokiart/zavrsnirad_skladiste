--SELECT name, collation_name FROM sys.databases;
--GO
---- Doma primjeniti na ime svoje baze 3 puta
--ALTER DATABASE db_aa599c_romanprodukcija SET SINGLE_USER WITH
--ROLLBACK IMMEDIATE;
--GO
--ALTER DATABASE db_aa599c_romanprodukcija COLLATE Croatian_CI_AS;
--GO
--ALTER DATABASE db_aa599c_romanprodukcija SET MULTI_USER;
--GO
--SELECT name, collation_name FROM sys.databases;
--GO

use master;
go
drop database if exists skladisnoposlovanje;
go

create database skladisnoposlovanje;
go
alter database skladisnoposlovanje collate Croatian_CI_AS;
go
use skladisnoposlovanje;

create table operateri(
sifra int not null primary key identity(1,1),
email varchar(50) not null,
lozinka varchar(200) not null

);

-- Lozinka roman generirana pomoæu https://bcrypt-generator.com/
insert into operateri values ('roman.zaric@gmail.com',
'$2a$12$tlc2ATQlkYPyp/woj.1fY.cJj.TQGzM1kuazcKBQuObbHE2jOVELy');


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
email varchar(50),
brojtelefona varchar(20)
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
osoba int not null references osobe(sifra),
skladistar int not null references skladistari(sifra),
napomena varchar(250)
);

create table izdatniceproizvodi (

sifra int not null primary key identity(1,1),
proizvod int not null references proizvodi(sifra),
izdatnica int not null references izdatnice(sifra),
kolicina int
);






insert into skladistari(ime,prezime) 
values
--1
('Roman','Žariæ'),
--2
('Miroslav','Janiæ');

insert into proizvodi(naziv,sifraproizvoda,mjernajedinica) 
values
--1
('lopata',100,'kom'),
--2
('metla',101,'kom'),
--3
('sapun',102,'kom'),
--4
('šampon',103,'lit'),
--5
('gedore',104,'gar'),
--6
('deterðent',105,'kg'),
--7
('pijesak',106,'t');


insert into osobe(prezime,ime,brojtelefona) 
values
('Božiæ','Petra',null),
('Farkaš','Dominik',null),
('Glavaš','Natalija',null),
('Janiæ','Miroslav',null),
('Janješiæ','Filip',null),
('Jovi?æ','Nataša',null),
('Bariæ','Luka',null),
('Kelava','Antonio',null),
('Kešinoviæ','Marijan',null),
('Leninger','Ivan',null),
('Macanga','Antonio',null),
('Miloloža','Antonio',null),
('Pavkoviæ','Matija',null),
('Peterfaj','Karlo',null),
('Pleæaš','Adriana',null),
('Senèiæ','Ivan',null),
('Šuler','Zvonimir',null),
('Turèek','Mario',null),
('Veseli','Domagoj',null),
('Vukovi?','Kristijan',null),
('Vukušiæ','Ivan',null),
('Žariæ','Roman',null),
('Pavlovi?','Ivan',null),
('Županiæ','Andrea',null),
('Županiæ','Tomislav',null),
('Petak','Martina',null),
('Perak','Marko',null),
('Mokriš','Bartol',null),
('Julariæ','Ljubomir',null),
('Èeliæ','Ivor',null);


insert into izdatnice(brojizdatnice,osoba,skladistar) 
values
(100,1,1),(101,12,2),(102,19,1),(103,24,2);

insert into izdatniceproizvodi(proizvod,izdatnica) 
values
(2,2),(1,2),(3,2),(4,3),(7,4);

