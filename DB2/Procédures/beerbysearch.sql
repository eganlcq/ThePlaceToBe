CREATE DEFINER=`root`@`localhost` PROCEDURE `beerbysearch`(beername varchar(32))
BEGIN
SELECT *
FROM TbBiere
WHERE TbBiere.nombiere = beername;
END