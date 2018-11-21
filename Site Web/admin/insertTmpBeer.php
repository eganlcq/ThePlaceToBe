<?php
    require_once 'database.php';

    $nameBeer              = "";
    $imgBeer               = "";

    if(!empty($_POST)) {
        $nameBeer          = $_POST['nameBeer'];
        $imgBeer           = $_POST['imgBeer'];
        
    }
        
   
    $db = Database::connect();
    $preSql = 'alter table TbUser auto_increment = 1;';
    $preStatement = $db->prepare($preSql);
    $preStatement->execute();

    $sql =  'call insertscan(?, ?);';
    $statement = $db->prepare($sql);
    $statement->execute([$nameBeer, $imgBeer]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();

    echo json_encode($item);