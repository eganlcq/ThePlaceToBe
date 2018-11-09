CREATE DEFINER=`admin`@`%` PROCEDURE `insertbar`(nom varchar(32))
BEGIN
INSERT INTO TbBar(nombar)
VALUES(nom);
END