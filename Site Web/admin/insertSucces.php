<?php
require_once 'database.php';

    $idSucces             = "";
    $idUser               = "";

    if(!empty($_POST)) {
        $idSucces        = $_POST['idSucces'];
        $idUser          = $_POST['idUser'];
    }

    $db = Database::connect();
    $sql =  'call insertachiev(?,?);';
    $statement = $db->prepare($sql);
    $statement->execute([ $idUser,$idSucces]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();
    echo json_encode($item);