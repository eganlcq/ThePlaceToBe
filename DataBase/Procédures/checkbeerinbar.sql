CREATE DEFINER=`admin`@`%` PROCEDURE `checkbeerinbar`(beer varchar(32), bar varchar(32))
BEGIN
SELECT DISTINCT IF(
	(SELECT idbiere FROM TbBiere WHERE nombiere = beer) in (SELECT DISTINCT idbiere FROM TbCarte WHERE idbar = (SELECT idbar FROM TbBar WHERE nombar = bar))
    ,'TRUE', 'FALSE') as verif
FROM TbCarte;
END