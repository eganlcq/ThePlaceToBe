<?php
    require_once 'database.php';

    $nameBar              = "";
    $numStreet            = "";
    $nameStreet           = "";
    $ZIPCode              = "";
    $nameCity             = "";
    $accessibility        = "";
    $idUser               = "";

    if(!empty($_POST)) {
        $nameBar          = $_POST['nameBar'];
        $numStreet        = $_POST['numStreet'];
        $nameStreet       = $_POST['nameStreet'];
        $ZIPCode          = $_POST['ZIPCode'];
        $nameCity         = $_POST['nameCity'];
        $accessibility    = $_POST['accessibility'];
        $idUser           = $_POST['idUser'];
    }
        
   
    $db = Database::connect();
    $preSql = 'alter table TbInterBar auto_increment = 1;';
    $preStatement = $db->prepare($preSql);
    $preStatement->execute();

    $sql =  'call insertbar(?, ?, ?, ?, ?, ?, ?);';
    $statement = $db->prepare($sql);
    $statement->execute([$nameBar, $nameStreet, $numStreet, $ZIPCode, $nameCity, $accessibility, $idUser]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();

    echo json_encode($item);