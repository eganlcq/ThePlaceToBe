CREATE DEFINER=`root`@`localhost` FUNCTION `hashage`(sem char(32), mot char(32)) RETURNS char(64) CHARSET utf8
BEGIN

DECLARE hash char(64);
SET hash = concat(sem, mot);
RETURN MD5(hash);

END