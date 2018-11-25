CREATE DEFINER=`admin`@`%` PROCEDURE `selectscan`()
BEGIN
SELECT *
FROM TbInterBeer;
END