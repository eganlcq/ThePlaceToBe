<?php
function monPrint_r($liste){
    return "<pre>".print_r($liste, 1)."</pre>";
}

function isAuthenticated(){
    return (isset($_SESSION['user']))? true : false;
}
function isAdmin($pseudo, $email){
    return ($pseudo === 'Admin')? ($email === 'placetobe.ephec@hotmail.com'):false ? true: false;
}