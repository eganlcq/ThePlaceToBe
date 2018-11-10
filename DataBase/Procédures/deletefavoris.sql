CREATE DEFINER=`admin`@`%` PROCEDURE `deletefavoris`(biereid int(4), userid int(5))
BEGIN
SET SQL_SAFE_UPDATES=0;
DELETE FROM TbFavoris
WHERE idbiere = biereid AND iduser = userid;
SET SQL_SAFE_UPDATES=1;
END