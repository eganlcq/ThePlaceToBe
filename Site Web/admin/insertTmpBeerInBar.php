<?php
    require_once 'database.php';

    $beer              = "";
    $bar               = "";
    $idUser            = "";

    if(!empty($_POST)) {
        $beer          = $_POST['beer'];
        $bar           = $_POST['bar'];
        $idUser        = $_POST['idUser'];
        
    }

    $db = Database::connect();
    $preSql = 'alter table TbInsertCarte auto_increment = 1;';
    $preStatement = $db->prepare($preSql);
    $preStatement->execute();

    $sql =  'call insertcarte(?, ?, ?);';
    $statement = $db->prepare($sql);
    $statement->execute([$beer, $bar, $idUser]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();

    echo json_encode($item);