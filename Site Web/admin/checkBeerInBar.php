<?php
require_once 'database.php';

    $beer             = "";
    $bar              = "";

    if(!empty($_POST)) {
        $beer         = $_POST['beer'];
        $bar          = $_POST['bar'];
    }

    $db = Database::connect();
    $sql =  'call checkbeerinbar(?, ?);';
    $statement = $db->prepare($sql);
    $statement->execute([$beer, $bar]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();
    echo json_encode($item);