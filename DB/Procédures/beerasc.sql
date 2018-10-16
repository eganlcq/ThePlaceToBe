CREATE DEFINER=`root`@`localhost` PROCEDURE `beerasc`()
BEGIN
SELECT *
FROM TbBiere
ORDER BY nombiere ASC;
END