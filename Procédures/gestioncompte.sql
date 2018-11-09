CREATE DEFINER=`admin`@`%` PROCEDURE `gestioncompte`(id int, nom varchar(32), prenom varchar(32), pseudo varchar(32), mail varchar(64), photo varchar(128))
BEGIN
UPDATE TbUser AS tbu
SET tbu.nom = nom, tbu.prenom = prenom, tbu.pseudo = pseudo, tbu.email = mail, tbu.photo = photo
WHERE tbu.iduser = id;

END