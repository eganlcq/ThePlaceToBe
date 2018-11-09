CREATE DEFINER=`admin`@`%` PROCEDURE `verifmail`(mail varchar(64))
BEGIN
SELECT DISTINCT IF(mail IN (SELECT tbu.email FROM TbUser AS tbu), 'FALSE', 'TRUE') AS verif
FROM TbUser;
END