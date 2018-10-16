CREATE DEFINER=`root`@`localhost` PROCEDURE `alcoolasc`()
BEGIN
SELECT *
FROM TbBiere
ORDER BY alcoolemie ASC;
END