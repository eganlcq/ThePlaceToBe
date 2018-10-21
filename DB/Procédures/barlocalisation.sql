CREATE DEFINER=`admin`@`%` PROCEDURE `barlocalisation`(beerid varchar(32))
BEGIN
SELECT tbba.idbar, tbba.nombar, tbba.latitude, tbba.longitude
FROM TbBar AS tbba
JOIN TbCarte AS tbc ON tbc.idbar = tbba.idbar
JOIN TbBiere AS tbbi ON tbbi.idbiere = tbc.idbiere
WHERE tbbi.idbiere = beerid;
END