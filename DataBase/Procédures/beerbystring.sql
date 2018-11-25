CREATE DEFINER=`admin`@`%` PROCEDURE `beerbystring`(str varchar(32))
BEGIN
SELECT *
FROM TbBiere
WHERE TbBiere.nombiere LIKE CONCAT('%', str, '%');
END