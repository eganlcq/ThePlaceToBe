<?php
    require_once 'database.php';
    $idUser = "";
    if(!empty($_POST)) {
      
        $idUser = $_POST['idUser'];
    }
    $db = Database::connect();
    $sql =  'SELECT t1.idsucces, t1.nom, t1.descr, t1.image FROM TbSucces as t1
JOIN TbOrnement as t2
ON t1.idsucces = t2.idsucces
JOIN TbUser as t3
ON t3.iduser = t2.iduser
WHERE t3.iduser = ?;';
    $statement = $db->prepare($sql);
    $statement->execute([$idUser]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();
    echo json_encode($item);
?>
