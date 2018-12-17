<?php
require_once 'database.php';

    $idUser             = "";


    if(!empty($_POST)) {
        $idUser         = $_POST['idUser'];
    }

    $db = Database::connect();
    $sql =  'call countfavoris(?)';
    $statement = $db->prepare($sql);
    $statement->execute([$idUser]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();
    echo json_encode($item);