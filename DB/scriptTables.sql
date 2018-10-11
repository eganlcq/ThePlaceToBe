create table TbSucces(
idsucces int(2) not null,
nom varchar(16) not null,
primary key (idsucces)
);

create table TbBar(
nombar varchar(32) not null,
latitude double(10,7) not null,
longitude double(10,7) not null,
primary key (nombar)
);

create table TbAvis(
idavis int(5) not null,
nombar varchar(32) not null,
avis text not null,
primary key (idavis),
foreign key (nombar) references TbBar(nombar)
);

create table TbUser(
iduser int not null,
nom varchar(32) not null,
prenom varchar(32) not null,
datenaiss date not null,
email varchar(64) not null,
mdp varchar(64) not null,
photo blob DEFAULT null,
nbrecherche int DEFAULT 0,
nbajout int DEFAULT 0,
datelastco datetime not null DEFAULT current_timestamp,
primary key (iduser)
);

create table TbOrnement(
iduser int(5) not null,
idsucces int(3) not null,
primary key (iduser, idsucces),
foreign key (iduser) references TbUser(iduser),
foreign key (idsucces) references TbSucces(idsucces)
);

create table TbBiere(
nombiere varchar(32) not null,
alcoolemie varchar(4) not null,
typebiere varchar(16) not null,
image blob not null,
primary key (nombiere)
);



create table TbCarte(
nombiere varchar(32) not null,
nombar varchar(32) not null,
primary key (nombiere , nombar),
foreign key (nombiere) references TbBiere(nombiere),
foreign key (nombar) references TbBar(nombar)
);



create table TbFavoris(
nombiere varchar(32) not null,
iduser int(5) not null,
primary key (nombiere, iduser),
foreign key (nombiere) references TbBiere(nombiere),
foreign key (iduser) references TbUser(iduser)
);