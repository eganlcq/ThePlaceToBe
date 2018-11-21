<?php
require_once 'database.php';

    $txt                 = "";

    if(!empty($_POST)) {
        $txt             = $_POST['text'];
    }

    $db = Database::connect();
    $sql =  'call beerbystring(?);';
    $statement = $db->prepare($sql);
    $statement->execute([$txt]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();
    echo json_encode($item);