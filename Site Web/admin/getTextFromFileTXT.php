<?php 
/*
$nomFichier = "";

if(!empty($_POST)) {
      
    $nomFichier = $_POST['text'];
}
*/

// 
$fichier = fopen("http://www.theplacetobe.ovh/textes/ProtectionDeDonnees.txt",'r');
$text = "";

while(!feof($fichier)){
    $line = fgets($fichier);
    $text .= $line;
}

fclose($fichier);



$fichier2 = fopen("http://www.theplacetobe.ovh/textes/TauxAlcoolParVerre.txt",'r');
$text2 = "";

while(!feof($fichier2)){
    $line = fgets($fichier2);
    $text2 .= $line;
}

fclose($fichier2);



$fichier3 = fopen("http://www.theplacetobe.ovh/textes/Rappel.txt",'r');
$text3 = "";

while(!feof($fichier3)){
    $line = fgets($fichier3);
    $text3 .= $line;
}

fclose($fichier3);


$tab = Array(Array("TexteProdData" => $text),Array("TexteTauxAlcool" => $text2),Array("TexteRappel" => $text3));


echo json_encode($tab);