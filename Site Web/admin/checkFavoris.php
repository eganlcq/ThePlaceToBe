<?php
require_once 'database.php';

    $idUser             = "";
    $idBiere            = "";

    if(!empty($_POST)) {
        $idUser         = $_POST['idUser'];
        $idBiere        = $_POST['idBiere'];
    }

    $db = Database::connect();
    $sql =  'call checkfavoris(?, ?);';
    $statement = $db->prepare($sql);
    $statement->execute([$idBiere, $idUser]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();
    echo json_encode($item);