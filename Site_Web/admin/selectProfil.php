<?php
    require_once 'database.php';

	// créer user par défaut?
    $idUser = 1;

	if(!empty($_POST)) {
        $idUser          = $_POST['idBiere'];
    }

    $db = Database::connect();

    $sql =  'select iduser, nom, prenom, datenaiss, photo from TbUser where iduser = ?;';
    $statement = $db->prepare($sql);
    $statement->execute([$idUser]);
    $item = $statement->fetchAll(PDO::FETCH_ASSOC);
    //echo '<pre>' . print_r($item, true) . '</pre>';
    Database::disconnect();
    echo json_encode($item);
?>