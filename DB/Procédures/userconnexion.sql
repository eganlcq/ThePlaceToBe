CREATE DEFINER=`admin`@`%` PROCEDURE `userconnexion`(nom varchar(32), pswd varchar(64))
BEGIN
SELECT * 
FROM TbUser AS tbu
WHERE tbu.pseudo = nom AND tbu.mdp = pswd;
END