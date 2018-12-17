<?php
require_once 'INC/request.inc.php';

session_start();

if(isset($_GET['rq']) && !empty($_GET['rq'])){
    gereRequest($_GET['rq']);
    die(json_encode($tosend));
}

$compte = "<h1><a href='login.html'>Mon compte</a></h1>";

if(isAuthenticated()){
    $compte = "<img id=avatar src=image/avatar/".$_SESSION['user']['photo']." height=50px width=50px alt='Logo_Compte'><p>".$_SESSION['user']['pseudo']."</p><p id='liste'>&#9662;</p>";
}

require_once 'INC/mainPage.inc.php';