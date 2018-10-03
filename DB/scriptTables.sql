
create table TbSucces(
idsucces int(2) not null,
nom varchar(16) not null,
primary key (idsucces)
);

create table TbBar(
idbar int(3) not null,
nom varchar(16) not null,
longitude varchar(11) not null,
latitude varchar(11) not null,
primary key (idbar)
);

create table TbAvis(
idavis int(5) not null,
idbar int(3) not null,
avis varchar(500) not null,
primary key (idavis),
foreign key (idbar) references TbBar(idbar)
);

create table TbUser(
iduser int(5) not null,
nom varchar(32) not null,
prenom varchar(32) not null,
mdp varchar(32) not null,
email varchar(64) not null,
photo blob,
primary key (iduser)
);

create table TbOrnement(
iduser int(5) not null,
idsucces int(2) not null,
primary key (iduser, idsucces),
foreign key (iduser) references TbUser(iduser),
foreign key (idsucces) references TbSucces(idsucces)
);

create table TbBiere(
idbiere int(3) not null,
nom varchar(16) not null,
alcoolemie decimal(2,1) not null,
saveur varchar(16) not null,
image blob not null,
primary key (idbiere)
);



create table TbCarte(
idbiere int(3) not null,
idbar int(3) not null,
primary key (idbiere , idbar),
foreign key (idbiere) references TbBiere(idbiere),
foreign key (idbar) references TbBar(idbar)
);



create table TbFavoris(
idbiere int(3) not null,
iduser int(5) not null,
primary key (idbiere, iduser),
foreign key (idbiere) references TbBiere(idbiere),
foreign key (iduser) references TbUser(iduser)
);





