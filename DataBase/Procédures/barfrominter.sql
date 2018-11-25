CREATE DEFINER=`admin`@`%` PROCEDURE `barfrominter`(idinsert int, nom varchar(32), rue varchar(64), num int, localite int, ville varchar(64), latitude double(10,7), longitude double(10,7), access tinyint(1))
BEGIN
INSERT INTO TbVille(localite, ville)
VALUES(localite, ville);

INSERT INTO TbBar(nombar, latitude, longitude, rue, numero, localite, accessibilite)
VALUES(nom, latitude, longitude, rue, num, localite, access);


SET SQL_SAFE_UPDATES=0;
DELETE FROM TbInterBar
WHERE idbar = idinsert;
SET SQL_SAFE_UPDATES=0;
END