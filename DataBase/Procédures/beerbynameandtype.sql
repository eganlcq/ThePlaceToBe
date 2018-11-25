CREATE DEFINER=`admin`@`%` PROCEDURE `beerbynameandtype`(typebiere varchar(16), str varchar(32))
BEGIN
SELECT *
FROM TbBiere
WHERE TbBiere.typebiere = typebiere AND TbBiere.nombiere LIKE CONCAT('%', str, '%');
END