<?php
require_once 'database.php';

    $pseudo             = "";

    if(!empty($_POST)) {
        $pseudo         = $_POST['pseudo'];
    }

    $db = Database::connect();
    $sql =  'call verifpseudo(?);';
    $statement = $db->prepare($sql);
    $statement->execute([$pseudo]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    Database::disconnect();
    echo json_encode($item);