<?php
require_once 'INC/db.inc.php';

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
        case 'slogan':  $res = chargeTemplate($retour);
                          if($res) toSend($res, 'slogan');
                          else error('fichier non chargé !');
                          break;
        case 'login' : $res = chargeTemplate($retour);
                       if($res) toSend($res, 'login');
                       else error('fichier non chargé !');
                       break;
        case 'logout':
            toSend("<h1><a href='login.html'>Mon compte</a></h1>", 'logout');
            session_unset();
        break;
        case 'compte':
            if(isAuthenticated()){
                $res = chargeTemplate($retour);
                if($res) toSend($res, 'compte');
                else error('fichier non chargé !');
                break;
            }
        case 'formSubmit': gereSubmit(); break;
    }
}

function chargeTemplate($name="yololo"){
    $name='INC/'.strtolower($name).'.inc.php';
    return (file_exists($name))? implode("\n", file($name)) : false;
}

function gereSubmit(){
    if(!isset($_POST['senderId'])) $_POST['senderId']= "";
    switch ($_POST['senderId']){
        case 'connexion':
            $db = new Db();
            $pdo = $db->getIPdo();
            $sql= 'select * from TbUser where pseudo= ? AND mdp= ?';
            $sth = $pdo->prepare($sql);
            $sth->execute([$_POST['pseudo'], $_POST['pwd']]);
            $result = $sth->fetchAll(PDO::FETCH_ASSOC);
            $_SESSION['user'] = $result[0];
            toSend(json_encode($result[0]), 'connexion');
            break;
        case 'inscription': toSend($_POST['pwd'], 'connexion'); break;
    }
}

function monPrint_r($liste){
    return "<pre>".print_r($liste, 1)."</pre>";
}

function isAuthenticated(){
    return (isset($_SESSION['user']))? true : false;
}
