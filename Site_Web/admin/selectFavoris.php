<?php
    require_once 'database.php';
    $idUser = "";
    if(!empty($_POST)) {
      
        $idUser = $_POST['idUser'];
    }
    $db = Database::connect();
    $sql =  'SELECT t3.* FROM TbUser as t1
JOIN TbFavoris as t2
ON t1.iduser = t2.iduser
JOIN TbBiere as t3
ON t2.idbiere = t3.idbiere
WHERE t1.iduser = ?;';
    $statement = $db->prepare($sql);
    $statement->execute([$idUser]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();
    echo json_encode($item);
?>
