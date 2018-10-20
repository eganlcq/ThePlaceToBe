<?php
require_once 'database.php';

    $idBiere              = "";

    if(!empty($_POST)) {
        $idBiere          = $_POST['idBiere'];
    }

    $db = Database::connect();
    $sql =  'select * from TbBiere where idBiere = ?;';
    $statement = $db->prepare($sql);
    $statement->execute([$idBiere]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();
    echo json_encode($item);