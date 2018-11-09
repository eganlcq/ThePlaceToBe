CREATE DEFINER=`admin`@`%` PROCEDURE `favorisbyuser`(id int)
BEGIN
SELECT tbb.* FROM TbBiere as tbb
JOIN TbFavoris as tbf ON tbf.idbiere = tbb.idbiere
JOIN TbUser as tbu ON tbu.iduser = tbf.iduser
WHERE tbu.iduser = id;
END