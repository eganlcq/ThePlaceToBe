CREATE DEFINER=`admin`@`%` PROCEDURE `alcooldesc`()
BEGIN
SELECT *
FROM TbBiere
ORDER BY alcoolemie DESC;
END