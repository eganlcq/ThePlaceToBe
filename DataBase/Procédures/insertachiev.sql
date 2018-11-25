CREATE DEFINER=`admin`@`%` PROCEDURE `insertachiev`(user int, achiev int)
BEGIN
INSERT INTO TbOrnement(Iduser, idsucces)
VALUES(user, achiev);
END