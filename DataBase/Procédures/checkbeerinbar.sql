CREATE DEFINER=`admin`@`%` PROCEDURE `checkbeerinbar`(beer varchar(32), bar varchar(32))
BEGIN
SELECT DISTINCT IF(
	(SELECT idbiere FROM TbBiere WHERE nombiere = beer) in (SELECT (SELECT idbiere FROM TbBiere WHERE nombiere = beer) FROM TbCarte WHERE idbar = (SELECT idbar FROM TbBar WHERE nombar = bar))
    ,'FALSE', 'TRUE') as verif
FROM TbCarte;
END