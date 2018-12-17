<?php
    require_once 'database.php';

    $nameBeer              = "";
    $alcool                = "";
    $type                  = "";
    $imgBeer               = "";
    $bar                   = "";
    $idUser                = "";

    if(!empty($_POST)) {
        $nameBeer          = $_POST['nameBeer'];
        $alcool            = $_POST['alcool'];
        $type              = $_POST['type'];
        $imgBeer           = $_POST['imgBeer'];
        $bar               = $_POST['bar'];
        $idUser            = $_POST['idUser'];
    }
        
   
    $db = Database::connect();
    $preSql = 'alter table TbInterBeer auto_increment = 1;';
    $preStatement = $db->prepare($preSql);
    $preStatement->execute();

    $sql =  'call insertscan(?, ?, ?, ?, ?, ?);';
    $statement = $db->prepare($sql);
    $statement->execute([$nameBeer, $alcool, $type, $imgBeer, $bar, $idUser]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();

    echo json_encode($item);