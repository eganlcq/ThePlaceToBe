CREATE DEFINER=`admin`@`%` PROCEDURE `insertfavoris`(biereid int(4), userid int(5))
BEGIN
INSERT INTO TbFavoris(idbiere, iduser)
VALUES (biereid, userid);
END