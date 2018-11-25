CREATE DEFINER=`admin`@`%` PROCEDURE `checkbeer`(beer varchar(32))
BEGIN
SELECT DISTINCT IF(beer IN (SELECT tbbe.nombiere FROM TbBiere AS tbbe), 'FALSE', 'TRUE') AS verif
FROM TbBiere;
END