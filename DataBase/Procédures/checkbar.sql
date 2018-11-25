CREATE DEFINER=`admin`@`%` PROCEDURE `checkbar`(bar varchar(32))
BEGIN
SELECT DISTINCT IF(bar IN (SELECT tbba.nombar FROM TbBar AS tbba), 'FALSE', 'TRUE') AS verif
FROM TbBar;
END