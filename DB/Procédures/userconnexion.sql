CREATE DEFINER=`admin`@`%` PROCEDURE `userconnexion`(nom varchar(32), pswd varchar(64))
BEGIN
SELECT tbu.iduser, tbu.nom, tbu.prenom, tbu.pseudo, tbu.email, tbu.datenaiss, tbu.photo 
FROM TbUser AS tbu
WHERE tbu.pseudo = nom AND tbu.mdp = pswd;
END