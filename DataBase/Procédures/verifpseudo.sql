CREATE DEFINER=`admin`@`%` PROCEDURE `verifpseudo`(pseudo varchar(32))
BEGIN
SELECT DISTINCT IF(pseudo IN (SELECT tbu.pseudo FROM TbUser AS tbu), 'FALSE', 'TRUE') AS verif
FROM TbUser;
END