CREATE DEFINER=`admin`@`%` PROCEDURE `cartefrominter`(idinsert int, beer varchar(32), bar varchar(32))
BEGIN
INSERT INTO TbCarte(idbiere, idbar)
VALUES((SELECT idbiere FROM TbBiere WHERE nombiere = beer), (SELECT idbar FROM TbBar WHERE nombar = bar));

SET SQL_SAFE_UPDATES=0;
DELETE FROM TbInterCarte
WHERE idcarte = idinsert;
SET SQL_SAFE_UPDATES=0;
END