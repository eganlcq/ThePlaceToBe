CREATE DEFINER=`root`@`localhost` PROCEDURE `alcooldesc`()
BEGIN
SELECT *
FROM TbBiere
ORDER BY alcoolemie DESC;
END