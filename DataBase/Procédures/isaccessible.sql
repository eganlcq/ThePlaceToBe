CREATE DEFINER=`admin`@`%` PROCEDURE `isaccessible`(bar int)
BEGIN
SELECT accessibilite
FROM TbBar
WHERE idbar = bar;
END