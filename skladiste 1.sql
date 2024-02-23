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

proizvod int not null references proizvodi(sifra),
izdatnica int not null references izdatnice(sifra),
kolicina int
);






insert into skladistari(ime,prezime) 
values
--1
('Roman','Žarić'),
--2
('Miroslav','Janić');

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
('deterđent',105,'kg'),
--7
('pijesak',106,'t');


insert into osobe(prezime,ime,brojtelefona) 
values
('Božić','Petra',null),
('Farkaš','Dominik',null),
('Glavaš','Natalija',null),
('Janić','Miroslav',null),
('Janješić','Filip',null),
('Joviæć','Nataša',null),
('Barić','Luka',null),
('Kelava','Antonio',null),
('Kešinović','Marijan',null),
('Leninger','Ivan',null),
('Macanga','Antonio',null),
('Miloloža','Antonio',null),
('Pavković','Matija',null),
('Peterfaj','Karlo',null),
('Plećaš','Adriana',null),
('Senčić','Ivan',null),
('Šuler','Zvonimir',null),
('Turček','Mario',null),
('Veseli','Domagoj',null),
('Vukoviæ','Kristijan',null),
('Vukušić','Ivan',null),
('Žarić','Roman',null),
('Pavloviæ','Ivan',null),
('Županić','Andrea',null),
('Županić','Tomislav',null),
('Petak','Martina',null),
('Perak','Marko',null),
('Mokriš','Bartol',null),
('Jularić','Ljubomir',null),
('Čelić','Ivor',null);


insert into izdatnice(brojizdatnice,osoba,skladistar) 
values
(100,1,1),(101,12,2),(102,19,1),(103,24,2);

insert into izdatniceproizvodi(proizvod,izdatnica) 
values
(2,2),(1,2),(3,2),(4,3),(7,4);

select a.sifra,a.brojizdatnice,a.datum,b.ime,b.prezime,c.ime,c.prezime
,e.naziv,d.kolicina,e.naziv,e.mjernajedinica
from izdatnice a inner join osobe b 
on b.sifra=a. osoba
left join skladistari c
on c.sifra=a.skladistar
inner join izdatniceproizvodi d
on a.sifra=d.izdatnica
inner join proizvodi e
on d.proizvod=e.sifra;