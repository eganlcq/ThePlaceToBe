CREATE DEFINER=`admin`@`%` PROCEDURE `verifmdp`(login varchar(32), pwd varchar(64))
BEGIN
SELECT distinct IF(login in (SELECT pseudo FROM TbUser), IF(pwd in (SELECT mdp FROM TbUser where pseudo = login), 'TRUE' , 'FALSE'), 'FALSE') AS verif
FROM TbUser;
END