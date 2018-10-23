<?php
    require_once 'database.php';

    $firstName              = "";
    $name                   = "";
    $pseudo                 = "";
    $email                  = "";
    $password               = "";

    if(!empty($_POST)) {
        $firstName          = $_POST['firstName'];
        $name               = $_POST['name'];
        $pseudo             = $_POST['pseudo'];
        $email              = $_POST['email'];
        $password           = $_POST['password'];
        
        
    }
        
   
    $db = Database::connect();
    $preSql = 'alter table TbUser auto_increment = 1;';
    $preStatement = $db->prepare($preSql);
    $preStatement->execute();

    $sql =  'insert into TbUser (prenom, nom, pseudo, datenaiss, email, mdp, photo, nbrecherche, nbajout, datelastco)
            values (?, ?, ?, null, ?, ?, default, default, default, default);';
    $statement = $db->prepare($sql);
    $statement->execute([$name, $firstName, $pseudo, $email, $password]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();

    echo json_encode($item);