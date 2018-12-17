<?php
require_once 'database.php';

    $bar             = "";

    if(!empty($_POST)) {
        $bar         = $_POST['bar'];
    }

    $db = Database::connect();
    $sql =  'call checkbar(?);';
    $statement = $db->prepare($sql);
    $statement->execute([$bar]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();
    echo json_encode($item);