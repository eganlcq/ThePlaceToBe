CREATE DEFINER=`admin`@`%` PROCEDURE `verifmdp`(pseudo varchar(32), pwd varchar(64))
BEGIN
SELECT IF(tbu.mdp = pwd, 'TRUE', 'FALSE') AS verif
FROM TbUser AS tbu
Where tbu.pseudo = pseudo;
END