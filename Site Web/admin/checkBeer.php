<?php
require_once 'database.php';

    $beer             = "";

    if(!empty($_POST)) {
        $beer         = $_POST['beer'];
    }

    $db = Database::connect();
    $sql =  'call checkbeer(?);';
    $statement = $db->prepare($sql);
    $statement->execute([$beer]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();
    echo json_encode($item);