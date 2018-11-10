CREATE DEFINER=`admin`@`%` PROCEDURE `beerbyid`(beerid int(4))
BEGIN
SELECT * 
FROM TbBiere
WHERE TbBiere.idbiere = beerid;
END