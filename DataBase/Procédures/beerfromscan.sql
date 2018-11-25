CREATE DEFINER=`admin`@`%` PROCEDURE `beerfromscan`(idscan int, nom varchar(32), alcool float(3,1), typebiere varchar(16), img varchar(128), bar varchar(32))
BEGIN
INSERT INTO TbBiere(nombiere, alcoolemie, typebiere, image)
VALUES(nom, alcool, typebiere, img);

INSERT INTO TbCarte(idbiere, idbar)
VALUES((SELECT idbiere FROM TbBiere WHERE nombiere = nom), (SELECT idbar FROM TbBar WHERE nombar = bar));


SET SQL_SAFE_UPDATES=0;
DELETE FROM TbInterBeer
WHERE idbiere = idscan;
SET SQL_SAFE_UPDATES=0;
END