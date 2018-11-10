<?php
    require_once 'database.php';

    $firstName              = "";
    $name                   = "";
    $pseudo                 = "";
    $email                  = "";
    $password               = "";
    $dateNaiss              = "";

    if(!empty($_POST)) {
        $firstName          = $_POST['firstName'];
        $name               = $_POST['name'];
        $pseudo             = $_POST['pseudo'];
        $email              = $_POST['email'];
        $password           = $_POST['password'];
        $dateNaiss          = $_POST['dateNaiss'];
        
    }
        
   
    $db = Database::connect();
    $preSql = 'alter table TbUser auto_increment = 1;';
    $preStatement = $db->prepare($preSql);
    $preStatement->execute();

    $sql =  'call userinscriptionapp(?, ?, ?, ?, ?, ?);';
    $statement = $db->prepare($sql);
    $statement->execute([$firstName, $name, $pseudo, $email, $dateNaiss, $password]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();

    echo json_encode($item);