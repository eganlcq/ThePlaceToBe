CREATE DEFINER=`admin`@`%` PROCEDURE `selecttype`()
BEGIN
SELECT DISTINCT(typebiere)
FROM TbBiere;
END