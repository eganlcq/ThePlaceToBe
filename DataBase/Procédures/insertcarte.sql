CREATE DEFINER=`admin`@`%` PROCEDURE `insertcarte`(beer varchar(32), bar varchar(32))
BEGIN
INSERT INTO TbInterCarte(nombiere, nombar)
VALUES(beer, bar);
END