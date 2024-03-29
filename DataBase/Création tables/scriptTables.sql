create table TbSucces(
idsucces int(2) not null auto_increment,
nom varchar(32) not null,
descr text not null,
image varchar(64) not null, 
primary key (idsucces)
);

create table TbBar(
idbar int(4) not null auto_increment,
nombar varchar(32) not null,
latitude double(10,7) not null,
longitude double(10,7) not null,
rue varchar(64) not null,
numero int not null DEFAULT 1,
localite int not null,
accessibilite tinyint(1) not null DEFAULT 0,
primary key (idbar),
foreign key (localite) references TbVille(localite)
); 

create table TbVille(
localite int not null,
ville varchar(64) not null,
primary	key (localite)
);	

create table TbUser(
iduser int not null auto_increment,
nom varchar(32) not null,
prenom varchar(32) not null,
pseudo varchar(32) not null,
datenaiss date not null,
email varchar(64) not null,
mdp varchar(64) not null,
photo varchar(128) not null DEFAULT 'default.png',
nbrecherche int DEFAULT 0,
nbajoutprecedent int DEFAULT 0,
nbajout int DEFAULT 0,
datelastco datetime not null DEFAULT current_timestamp,
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
idbiere int(4) not null auto_increment,
nombiere varchar(32) not null,
alcoolemie float(3,1) DEFAULT null,
typebiere varchar(16) DEFAULT null,
image varchar(128) not null DEFAULT 'oneBeer.png',
primary key (idbiere)
);


create table TbCarte(
idbiere int(4) not null,
idbar int(4) not null,
primary key (idbiere , idbar),
foreign key (idbiere) references TbBiere(idbiere),
foreign key (idbar) references TbBar(idbar)
);


create table TbFavoris(
idbiere int(4) not null,
iduser int(5) not null,
primary key (idbiere, iduser),
foreign key (idbiere) references TbBiere(idbiere),
foreign key (iduser) references TbUser(iduser)
);

create table TbInterBeer(
idbiere int not null auto_increment,
nombiere varchar(32) not null,
image varchar(128) not null,
primary key (idbiere)
);

create table TbInterBar(
idbar int not null auto_increment,
nombar varchar(32) not null,
rue varchar(64) not null,
numero int not null DEFAULT 1,
localite int not null,
ville varchar(64) not null,
primary key (idbar)
);

create table TbInterCarte(
idcarte int not null auto_increment,
nombiere varchar(32) not null,
nombar varchar(32) not null,
primary key (idcarte)
);