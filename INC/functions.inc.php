<?php
function monPrint_r($liste){
    return "<pre>".print_r($liste, 1)."</pre>";
}

function isAuthenticated(){
    return (isset($_SESSION['user']))? true : false;
}