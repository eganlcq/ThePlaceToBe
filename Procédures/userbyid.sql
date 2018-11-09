CREATE DEFINER=`admin`@`%` PROCEDURE `userbyid`(userid int)
BEGIN
SELECT *
FROM TbUser
WHERE TbUser.iduser = userid;
END