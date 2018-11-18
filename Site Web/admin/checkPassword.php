<?php
require_once 'database.php';

    $pseudo             = "";
    $pswd               = "";

    if(!empty($_POST)) {
        $pseudo         = $_POST['pseudo'];
        $pswd           = $_POST['pswd'];
    }

    $db = Database::connect();
    $sql =  'call verifmdp(?, ?);';
    $statement = $db->prepare($sql);
    $statement->execute([$pseudo, $pswd]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();
    echo json_encode($item);