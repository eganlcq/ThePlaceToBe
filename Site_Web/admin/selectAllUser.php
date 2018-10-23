<?php
    require_once 'database.php';
    $db = Database::connect();
    $sql =  'select * from TbUser;';
    $statement = $db->prepare($sql);
    $statement->execute();
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();
    echo json_encode($item);
?>