<?php
require_once 'database.php';

    $idUser             = "";
    $idSucces          = "";


    if(!empty($_POST)) {
        $idUser         = $_POST['idUser'];
        $idSucces        = $_POST['idSucces'];
    }

    $db = Database::connect();
    $sql =  'call checksuccess(?,?)';
    $statement = $db->prepare($sql);
    $statement->execute([$idUser,$idSucces]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();
    echo json_encode($item);