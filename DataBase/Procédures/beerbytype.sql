CREATE DEFINER=`admin`@`%` PROCEDURE `beerbytype`(typebiere varchar(16))
BEGIN
SELECT *
FROM TbBiere
WHERE TbBiere.typebiere = typebiere;
END