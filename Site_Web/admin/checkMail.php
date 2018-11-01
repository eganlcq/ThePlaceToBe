<?php
require_once 'database.php';

    $email             = "";

    if(!empty($_POST)) {
        $email         = $_POST['email'];
    }

    $db = Database::connect();
    $sql =  'call verifmail(?);';
    $statement = $db->prepare($sql);
    $statement->execute([$email]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();
    echo json_encode($item);