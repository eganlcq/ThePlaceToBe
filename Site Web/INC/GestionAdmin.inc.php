<?php
require_once 'db.inc.php';
require_once 'functions.inc.php';

if(count(get_included_files())==1) die('--access denied--');

class gestionAdmin extends Db {
    private $db;

    public function __construct(){
        $this->db = new Db();
    }

    public function admin($func= 'biere'){
        $affichage ="<hr><div id=menuAdmin><ul><li><a href='gestionAdmin.html' id='biere'>Ajout bière</a></li><li><a href='gestionAdmin.html' id='bar'>Ajout bar</a></li><li><a href='gestionAdmin.html' id='liaison'>Liaison bière/bar</a></li></ul></div>";
        $affichage.= "<div id='contenuAdmin'>";
        $affichage.= $this->$func();
        $affichage.= "<div>";
        return $affichage;
    }

    public function biere(){
        $sth = $this->db->getIPdo()->prepare('SELECT * from TbInterBeer');
        $sth->execute();
        $return = $sth->fetchAll(PDO::FETCH_ASSOC);

        $affichage="<ul>";
        foreach ($return as $key=>$value) {
            $affichage.="<hr><li>".$value['nombiere']."</li>";
            $affichage.="<form method='post' action='formSubmit.html' class='biereAdmin' id='BiereAdmin' style='display:none'>";
            foreach ($value as $cle=>$valeur){
                switch ($cle){
                    case "idbiere": $affichage.="<input type=text name=".$cle." id=".$cle." value='".$valeur."' style=display:none>"; break;
                    case "image": $affichage.="<img class=imgBiere src=image/tmp/".$valeur." style='height:100%; width:60%'><input type=text value=".$valeur." name=".$cle." id=".$cle." style='display:none'>"; break;
                    default: $affichage.="<label for='".$cle."'>".$cle."</label><input type=text name=".$cle." id=".$cle." value='".$valeur."'>";
                }
            }
            $affichage.="<input type='submit' name='suppr' value='Supprimer'><span></span><input type='submit' name='Encoder' value='Encoder'></form>";
        }
        $affichage.="</ul>";

        return $affichage;
    }

    public function bar(){
        $sth = $this->db->getIPdo()->prepare('SELECT * from TbInterBar');
        $sth->execute();
        $return = $sth->fetchAll(PDO::FETCH_ASSOC);

        $affichage="<ul>";
        foreach ($return as $key=>$value) {
            $affichage.="<hr><li>".$value['nombar']."</li>";
            $affichage.="<form method='post' action='formSubmit.html' class='barAdmin' id='BarAdmin' style='display:none'>";
            foreach ($value as $cle=>$valeur){
                switch ($cle){
                    case "idbar": $affichage.="<input type=text name=".$cle." id=".$cle." value='".$valeur."' style=display:none>"; break;
                    case "localite": case "latitude": case "numero": $affichage.="<label for='".$cle."'>".$cle."</label><input type=number name=".$cle." id=".$cle." value='".$valeur."'>"; break;
                    default: $affichage.="<label for='".$cle."'>".$cle."</label><input type=text name=".$cle." id=".$cle." value='".$valeur."'>";
                }
            }
            $affichage.="<label for='longitude'>longitude</label><input type=number id=longitude name=longitude><label for='latitude'>latitude</label><input type=number id=latitude name=latitude>";
            $affichage.="<input type='submit' name='suppr' value='Supprimer'><input type='submit' name='Encoder' value='Encoder'></form>";
        }
        $affichage.="</ul>";

        return $affichage;
    }

    public function liaison(){

    }
}