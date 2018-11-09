<?php
require_once 'database.php';

    $idBeer             = "";
    $idUser               = "";

    if(!empty($_POST)) {
        $idBeer         = $_POST['idBeer'];
        $idUser          = $_POST['idUser'];
    }

    $db = Database::connect();
    $sql =  'call deletefavoris(?,?);';
    $statement = $db->prepare($sql);
    $statement->execute([$idBeer,$idUser]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();
    echo json_encode($item);