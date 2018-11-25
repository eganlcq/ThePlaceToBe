CREATE DEFINER=`admin`@`%` PROCEDURE `insertscan`(nom varchar(32), alcool float(3,1), typebiere varchar(16), img varchar(128), bar varchar(32))
BEGIN
INSERT INTO TbInterBeer(nombiere, alcoolemie, typebiere, image, nombar)
VALUES(nom, alcool, typebiere, img, bar);
END