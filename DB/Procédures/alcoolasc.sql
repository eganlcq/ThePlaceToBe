CREATE DEFINER=`admin`@`%` PROCEDURE `alcoolasc`()
BEGIN
SELECT *
FROM TbBiere
ORDER BY alcoolemie ASC;
END