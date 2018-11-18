CREATE DEFINER=`admin`@`%` PROCEDURE `achievementbyuser`(userid int)
BEGIN
SELECT tbs.*
FROM TbSucces AS tbs
JOIN TbOrnement AS tbo ON tbo.idsucces = tbs.idsucces
JOIN TbUser AS tbu ON tbu.iduser = tbo.iduser 
WHERE tbo.iduser = userid;
END