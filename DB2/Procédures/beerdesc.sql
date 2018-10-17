CREATE DEFINER=`root`@`localhost` PROCEDURE `beerdesc`()
BEGIN
SELECT *
FROM TbBiere
ORDER BY nombiere DESC;
END