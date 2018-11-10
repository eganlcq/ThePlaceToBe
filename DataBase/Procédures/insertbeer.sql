CREATE DEFINER=`admin`@`%` PROCEDURE `insertbeer`(nom varchar(32))
BEGIN
INSERT INTO TbBiere(nombiere)
VALUES(nom);
END