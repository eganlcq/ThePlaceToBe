<?php
require_once 'db.inc.php';
require_once 'compte.inc.php';

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
                  else error('fichier non chargé !');
                  break;
        case 'slogan':
            $res = chargeTemplate($retour);
            if($res) toSend($res, 'slogan');
            else error('fichier non chargé !');
            break;
        case 'login' :
            $res = chargeTemplate($retour);
            if($res) toSend($res, 'login');
            else error('fichier non chargé !');
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
            if(isAuthenticated()){
                $conf = new Config();
                toSend($conf->getAchiv(), 'compte');
            }
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
    $msg = '';
    $form = '';
    $erreur = '';
    if(!isset($_POST['senderId'])) $_POST['senderId']= "";
    switch ($_POST['senderId']){
        case 'connexion':
            $erreur = 2;
            $form = 'login';
            $return_mdp = $db->call('verifmdp', [$_POST['pseudo'], $_POST['pwd']])[0];
            $return_pseudo = $db->call('verifpseudo', [$_POST['pseudo']])[0];
            switch (true){
                case ($return_mdp['verif'] === "FALSE" || $return_pseudo["verif"] === "FALSE") :
                    $msg = "<b>Nom d'utilisateur ou mot de passe est incorrect !</b>";
                    break;
                default:
                    $result = $db->call('userconnexion', [$_POST['pseudo'], $_POST['pwd']]);
                    $_SESSION['user'] = $result[0];
                    toSend(json_encode($result[0]), 'connexion');
            }
            break;
        case 'inscription':
            $erreur = 1;
            $form = 'login';
            $date = new DateTime($_POST['datenaiss']);
            $interval = $date->diff(new DateTime())->format("%Y");
            $return_pseudo = $db->call('verifpseudo', [$_POST['pseudoInscr']])[0];
            $return_mail =  $db->call("verifmail", [$_POST['mail']])[0];
            switch (true){
                case ( $interval < 16 ):
                    $msg = "<b>Vous n'avez pas l'âge pour créer un compte !</b><br>";
                    break;
                case ($return_pseudo['verif'] === "TRUE" && $return_mail['verif'] === "FALSE"):
                    $msg = "<b>Adresse mail et/ou pseudo déja pris !</b>";
                    break;
                case ( $return_mail['verif'] === "FALSE"):
                    $msg = "<b>Adresse mail déja prise !</b><br>";
                    break;
                case ( $return_pseudo['verif'] === "TRUE"):
                    $msg = "<b>Pseudo déja pris !</b>";
                    break;
                default:
                    $result = $db->call('userinscription', [$_POST['prenom'], $_POST['nom'], $_POST['mail'], $_POST['pwdInscr'], $_POST['pseudoInscr'], $_POST['datenaiss']]);
                    $_SESSION['user'] = $result[0];
                    toSend(json_encode($result[0]), 'connexion');
            }
            break;
        case 'formCompte':
            $erreur = 3;
            $form = 'compte';
            if($_FILES['file']['name']){
                $_SESSION['user']['photo'] = $_FILES['file']['name'];
            }
            $return_pseudo = $db->call('verifpseudo', [$_POST['pseudo']])[0];
            $return_mail =  $db->call("verifmail", [$_POST['email']])[0];
            switch (true){
                case ($return_pseudo['verif'] === "TRUE" && $return_mail['verif'] === "FALSE"):
                    $msg = "<b>Adresse mail et/ou pseudo déja pris !</b>";
                    break;
                case ( $return_mail['verif'] === "FALSE"):
                    $msg = "<b>Adresse mail déja prise !</b><br>";
                    break;
                default:
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
            }
            chargeErreur($msg, $form, $erreur);
            break;
    }
}

function chargeErreur($msg, $form, $erreur){
    if($msg){
        if($form === "compte"){
            if(isAuthenticated()){
                $conf = new Config();
                toSend($conf->getForm(), 'compte');
            }
            toSend($msg, 'erreur'.$erreur);
        }
        else {
            $res = chargeTemplate($form);
            if($res) toSend($res, $form);
            toSend($msg, 'erreur'.$erreur);
        }
    }
}
function monPrint_r($liste){
    return "<pre>".print_r($liste, 1)."</pre>";
}

function isAuthenticated(){
    return (isset($_SESSION['user']))? true : false;
}
