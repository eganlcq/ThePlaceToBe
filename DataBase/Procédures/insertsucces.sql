CREATE DEFINER=`admin`@`%` PROCEDURE `insertsucces`(iduser int(5), idsucces int(2))
BEGIN
INSERT INTO TbOrnement(iduser, idsucces)
VALUES (iduser, idsucces);
END