CREATE DEFINER=`admin`@`%` PROCEDURE `userinscription`(prenom varchar(32), nom varchar(32), mail varchar(64), pwdInscr varchar(64), pseudoInscr varchar(32), datenaiss date)
BEGIN
INSERT INTO TbUser(prenom, nom, pseudo, datenaiss, email, mdp)
VALUES(prenom, nom, pseudoInscr, CAST(datenaiss AS date), mail, pwdInscr);

SELECT pseudo, photo
FROM TbUser AS tbu
WHERE tbu.pseudo = pseudoInscr;
END