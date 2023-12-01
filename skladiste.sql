use master;
go
drop database if exists skladisnoposlovanje;
go
create database skladisnoposlovanje;
go
alter database skladisnoposlovanje collate Croatian_CI_AS;
go
use skladisnoposlovanje;


create table proizvodi(

sifra int not null primary key identity(1,1),
naziv varchar(50) not null,
sifraproizvoda int,
mjernajedinica varchar(20) not null
); 

create table skladistari(

sifra int not null primary key identity(1,1),
ime varchar(50) not null,
prezime varchar(50) not null,
brojtelefona varchar(20)
);

create table osobe(

sifra int not null primary key identity(1,1),
ime varchar(50) not null,
prezime varchar(50) not null,
brojtelefona varchar(20)
);

create table izdatnice(

sifra int not null primary key identity(1,1),
brojizdatnice int not null,
datum datetime,
osoba int not null references osobe(sifra),
skladistar int not null references skladistari(sifra),
napomena varchar(250)
);

create table izdatniceproizvodi (

proizvod int not null references proizvodi(sifra),
izdatnica int not null references izdatnice(sifra),
kolicina int
);


--alter table izdatniceproizvodi add foreign key (proizvod) references proizvodi(sifra);
--alter table izdatniceproizvodi add foreign key (izdatnica) references izdatnice(sifra);
--alter table izdatnice add foreign key (skladistar) references skladistari(sifra);
--alter table izdatnice add foreign key (osoba) references osobe(sifra);



insert into skladistari(ime,prezime) 
values
--1
('Roman','Žariæ'),
--2
('Miroslav','Janiæ');

insert into proizvodi(naziv,mjernajedinica) 
values
--1
('lopata','kom'),
--2
('metla','kom'),
--3
('sapun','kom'),
--4
('šampon','lit'),
--5
('gedore','gar'),
--6
('deterðent','kg'),
--7
('pijesak','t');


insert into osobe(prezime,ime,brojtelefona) 
values
('Božiæ','Petra',null),
('Farkaš','Dominik',null),
('Glavaš','Natalija',null),
('Janiæ','Miroslav',null),
('Janješiæ','Filip',null),
('Joviæ','Nataša',null),
('Bariæ','Luka',null),
('Kelava','Antonio',null),
('Kešinoviæ','Marijan',null),
('Leninger','Ivan',null),
('Macanga','Antonio',null),
('Miloloža','Antonio',null),
('Pavkoviæ','Matija',null),
('Peterfaj','Karlo',null),
('Pleèaš','Adriana',null),
('Senèiæ','Ivan',null),
('Šuler','Zvonimir',null),
('Turèek','Mario',null),
('Veseli','Domagoj',null),
('Vukoviæ','Kristijan',null),
('Vukušiæ','Ivan',null),
('Žariæ','Roman',null),
('Pavloviæ','Ivan',null),
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

select a.sifra,a.brojizdatnice,a.datum,b.ime,b.prezime,c.ime,c.prezime
,e.naziv,d.kolicina,
concat(e.naziv,e.sifraproizvoda , e.mjernajedinica) as proizvod
from izdatnice a inner join osobe b 
on b.sifra=a. osoba
left join skladistari c
on c.sifra=a.skladistar
inner join izdatniceproizvodi d
on a.sifra=d.izdatnica
inner join proizvodi e
on d.proizvod=e.sifra;