<?php
require_once 'db.inc.php';
require_once 'compte.inc.php';
require_once 'GestionAdmin.inc.php';
require_once 'functions.inc.php';

if(count(get_included_files())==1) die('--access denied--');

$tosend= [];

function toSend($txt, $action){
    global $tosend;
    if(!isset($tosend[$txt])) $tosend[$action] ="";
    $tosend[$action] .= $txt;
}

function gereRequest($retour){
    switch ($retour){
        case 'about':  $res = chargeTemplate($retour);
                  if($res) toSend($res, 'about');
                  break;
        case 'slogan':
            $res = chargeTemplate($retour);
            if($res) toSend($res, 'slogan');
            break;
        case 'login' :
            $res = chargeTemplate($retour);
            if($res) toSend($res, 'login');
            break;
        case 'logout':
            $res = chargeTemplate('slogan');
            if($res) toSend($res, 'affichage');
            toSend("<h1><a href='login.html'>Mon compte</a></h1>","logout");
            session_unset();
            break;
        case 'compte':
            if(isAuthenticated()){
                $conf = new Config();
                toSend($conf->getForm(), 'compte');
            }
            break;
        case 'achievements':
            $conf = new Config();

            if(!$_SESSION['start']) $_SESSION['start'] = 0;

            if(isAuthenticated()){
                if($_POST['senderId'] === 'prec' && $_SESSION['start'] > 2) $_SESSION['start']-=3;
                if($_POST['senderId'] === 'suiv') $_SESSION['start']+=3;

                toSend($conf->getAchiv($_SESSION['start']), "achiev");
            }
            break;
        case 'erreur': toSend("SLT", ""); break;
        case 'favoris':
            $conf = new Config();

            if(!$_SESSION['startFav']) $_SESSION['startFav'] = 0;

            if(isAuthenticated()) {
                if($_POST['senderId'] === 'prec' && $_SESSION['startFav'] > 2) $_SESSION['startFav']-=3;
                if($_POST['senderId'] === 'suiv') $_SESSION['startFav']+=3;

                toSend($conf->getFav($_SESSION['startFav']), "achiev");
            }
            break;
        case 'admin':
            $res = chargeTemplate('adminlogin');
            if($res) toSend($res, 'adminconnexion');
            break;
        case 'legal':
            $res = chargeTemplate('legal');
            if($res) toSend($res, '');
            break;
        case 'rgdp':
            $res = chargeTemplate('rgdp');
            if($res) toSend($res, '');
            break;
        case 'protection':
            $res = chargeTemplate('protection');
            if($res) toSend($res, '');
            break;
        case 'gestionAdmin':
            $admin = new gestionAdmin();
            toSend($admin->admin($_POST['senderId']), 'gestionCompte');
            break;
        case 'contact':
            $res = chargeTemplate('contact');
            if($res) toSend($res, 'contact');
            break;
        case 'downloadAppli':
            $res = chargeTemplate('downloadappli');
            if($res) toSend($res, '');
            break;
        case 'formSubmit': gereSubmit(); break;
    }
}

function chargeTemplate($name="yololo"){
    $name='INC/'.strtolower($name).'.inc.php';
    return (file_exists($name))? implode("\n", file($name)) : false;
}

function gereSubmit(){
    $db = new Db();

    if(verifErreur($_POST['senderId'])) return;
    if(!isset($_POST['senderId'])) $_POST['senderId']= "";

    switch ($_POST['senderId']){
        case 'formAdmin':
            $admin = new gestionAdmin();
            toSend($admin->admin(), 'gestionCompte');
            break;
        case 'BiereAdmin':
            if($_POST['senderName'] === "suppr") $db->call('deletescan', [$_POST['idbiere']]);
            else {
                $db->call('beerfromscan', [$_POST['idbiere'],$_POST['nombiere'], $_POST['alcoolemie'],$_POST['typebiere'],$_POST['image'], $_POST['nombar']]);
                $file= "image/tmp/".$_POST['image'];
                $target_file = "image/beers/".$_POST['image'];

                rename($file, $target_file);
            }
            unlink("image/tmp/".$_POST['image']);
            $admin = new gestionAdmin();
            toSend($admin->admin(), 'gestionCompte');
            break;
        case 'BarAdmin':
            if($_POST['senderName'] === "suppr") $db->call('deletebar', [$_POST['idbar']]);
            else {
                $db->call('barfrominter', [$_POST['idbar'], $_POST['nombar'], $_POST['rue'], $_POST['numero'], $_POST['localite'], $_POST['ville'], $_POST['latitude'], $_POST['longitude'], $_POST['accessibilite']]);
            }
            $admin = new gestionAdmin();
            toSend($admin->admin('bar'), 'gestionCompte');
            break;
        case 'BarBiereAdmin':
            if($_POST['senderName'] === "suppr") $db->call('deletecarte', [$_POST['idcarte']]);
            else {
                $db->call('cartefrominter', [$_POST['idcarte'], $_POST['nombiere'], $_POST['nombar']]);
            }
            $admin = new gestionAdmin();
            toSend($admin->admin('liaison'), 'gestionCompte');
            break;
        case 'connexion':
            $result = $db->call('userconnexion', [$_POST['pseudo'], $_POST['pwd']]);
            $_SESSION['user'] = $result[0];
            toSend(json_encode($result[0]), 'connexion');
            break;
        case 'inscription':
            $result = $db->call('userinscription', [$_POST['prenom'], $_POST['nom'], $_POST['email'], $_POST['pwdInscr'], $_POST['pseudo'], $_POST['datenaiss']]);
            $_SESSION['user'] = $result[0];
            toSend(json_encode($result[0]), 'connexion');
            break;
        case 'formCompte':
            if($_FILES['file']['name']){
                $_SESSION['user']['photo'] = $_FILES['file']['name'];
            }
            $target_file = "image/avatar/".$_FILES['file']['name'];

            move_uploaded_file($_FILES['file']["tmp_name"], $target_file);

            $_SESSION['user']['nom'] = $_POST['nom'];
            $_SESSION['user']['prenom'] = $_POST['prenom'];
            $_SESSION['user']['email'] = $_POST['email'];
            $_SESSION['user']['pseudo'] = $_POST['pseudo'];

            $db->call('gestioncompte', [$_SESSION['user']['iduser'], $_SESSION['user']['nom'], $_SESSION['user']['prenom'], $_SESSION['user']['pseudo'], $_SESSION['user']['email'], $_SESSION['user']['photo']]);

            toSend(json_encode($_SESSION['user']) ,'connexion');
            $res = chargeTemplate('slogan');

            if($res) toSend($res, 'slogan');
            break;
    }
}

function verifErreur($form){
    $db = new Db();

    $msg = '';
    $temp = 'login';

    $return_pseudo = $db->call('verifpseudo', [$_POST['pseudo']])[0]['verif'];
    $return_mail =  $db->call("verifmail", [$_POST['email']])[0]['verif'];
    $return_mdp = $db->call('verifmdp', [$_POST['pseudo'], $_POST['pwd']])[0]['verif'];

    $date = new DateTime($_POST['datenaiss']);
    $interval = $date->diff(new DateTime())->format("%Y");

    switch($form){
        case 'formAdmin':
            $temp = 'admin';
            $erreur = 4;
            if($_POST['pwdAdmin'] !== "Passw0rd!") $msg = "Mot de passe érroné !";
            break;
        case "connexion":
            $erreur = 2;
            if($return_mdp === "FALSE" || $return_pseudo === "TRUE") $msg = "<b>Nom d'utilisateur ou mot de passe est incorrect !</b>";
            break;
        case "inscription":
            $erreur = 1;
            switch (true){
                case ( $interval < 16):
                    $msg = "<b>Vous n'avez pas l'âge pour créer un compte !</b><br>";
                    break;
                case ( $interval > 100 ):
                    $msg = "<b>Age improbable !</b><br>";
                    break;
                case ($return_pseudo === "FALSE" && $return_mail === "FALSE"):
                    $msg = "<b>Adresse mail et/ou pseudo déjà pris !</b>";
                    break;
                case ( $return_mail === "FALSE"):
                    $msg = "<b>Adresse mail déjà prise !</b><br>";
                    break;
                case ( $return_pseudo === "FALSE"):
                    $msg = "<b>Pseudo déjà pris !</b>";
                    break;
            }
            break;
        case "formCompte":
            $temp = 'compte';
            $erreur = 3;

            if($_SESSION['user']['pseudo'] === $_POST['pseudo']) ;
            elseif ($return_pseudo === 'FALSE') $msg = "<b>Adresse mail et/ou pseudo déjà pris !</b>";

            if($_SESSION['user']['email'] === $_POST['email']) ;
            elseif($return_mail === 'FALSE') $msg = "<b>Adresse mail et/ou pseudo déjà pris !</b>";
            break;
    }
    if($msg) {
        chargeErreur($msg, $temp, $erreur);
    }
    return $msg;
}

function chargeErreur($msg, $temp, $erreur){
    switch ($temp){
        case 'compte':
            if(isAuthenticated()){
                $conf = new Config();
                toSend($conf->getForm(), 'compte');
            }
            break;
        case 'admin':
            if(isAuthenticated()){
                $conf = new Config();
                toSend($conf->getForm(), 'compte');
            }
            $res = chargeTemplate('adminlogin');
            if($res) toSend($res, 'adminconnexion');
            break;
        default:
            $res = chargeTemplate($temp);
            if($res) toSend($res, $temp);
    }
    toSend($msg, 'erreur'.$erreur);
}
