<?php
require_once 'database.php';

    $flavor              = "";

    if(!empty($_POST)) {
        $flavor          = $_POST['flavor'];
    }

    $db = Database::connect();
    $sql =  'call beerbytype(?);';
    $statement = $db->prepare($sql);
    $statement->execute([$flavor]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();
    echo json_encode($item);