<?php
    require_once 'database.php';

    $idUser                 = "";
    $idBeer                 = "";

    if(!empty($_POST)) {
        $idUser             = $_POST['idUser'];
        $idBeer             = $_POST['idBeer'];
        
    }
        
   
    $db = Database::connect();
    $sql =  'call insertfavoris(?, ?);';
    $statement = $db->prepare($sql);
    $statement->execute([$idBeer, $idUser]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();

    echo json_encode($item);