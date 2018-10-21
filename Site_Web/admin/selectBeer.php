<?php
    require_once 'database.php';
    $db = Database::connect();
    $sql =  'call beerasc();';
    $statement = $db->prepare($sql);
    $statement->execute();
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();
    echo json_encode($item);
?>