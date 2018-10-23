CREATE DEFINER=`admin`@`%` PROCEDURE `userinscriptionapp`(nom varchar(32), prenom varchar(32), pseudo varchar(32), mail varchar(64), datenaiss date, mdp varchar(64))
BEGIN
INSERT INTO TbUser (prenom, nom, pseudo, datenaiss, email, mdp)
VALUES(prneom, nom, pseudo, mail, CAST(datenaiss AS date), mdp);
END