CREATE DEFINER=`admin`@`%` PROCEDURE `checkfavoris`(beer int, user int)
BEGIN
SELECT DISTINCT IF(beer IN (SELECT tbf.idbiere FROM TbFavoris AS tbf WHERE tbf.iduser = user), 'TRUE', 'FALSE') AS verif
FROM TbFavoris;
END