<?php
require_once 'database.php';

    $flavor              = "";
    $txt                 = "";

    if(!empty($_POST)) {
        $flavor          = $_POST['flavor'];
        $txt             = $_POST['text'];
    }

    $db = Database::connect();
    $sql =  'call beerbynameandtype(?, ?);';
    $statement = $db->prepare($sql);
    $statement->execute([$flavor, $txt]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();
    echo json_encode($item);