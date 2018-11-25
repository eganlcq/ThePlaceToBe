CREATE DEFINER=`admin`@`%` PROCEDURE `insertbar`(nom varchar(32), rue varchar(64), num int, localite int, ville varchar(64), access tinyint(1))
BEGIN
INSERT INTO TbInterBar(nombar, rue, numero, localite, ville, accessibilite)
VALUES(nom, rue, num, localite, ville, access);
END