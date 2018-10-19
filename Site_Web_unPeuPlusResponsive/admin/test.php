<?php
    require_once 'database.php';

    $id = 0;

    $db = Database::connect();
    $sql =  'select * from TbBiere;';
    $statement = $db->prepare($sql);
    $statement->execute();
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);

    //echo '<pre>' . print_r($item, true) . '</pre>';
    Database::disconnect();
    echo json_encode($item);
?>