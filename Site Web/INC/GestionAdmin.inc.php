<?php
require_once 'db.inc.php';
require_once 'functions.inc.php';

if(count(get_included_files())==1) die('--access denied--');

class gestionAdmin extends Db {
    private $db;

    public function __construct(){
        $this->db = new Db();
    }

    public function admin(){
        $sth = $this->db->getIPdo()->prepare('SELECT * from TbInter');
        $sth->execute();
        $return = $sth->fetchAll(PDO::FETCH_ASSOC);

        $affichage = "<div id='table'><span>id</span><span>nom</span><span>image</span><span>alcoolemie</span><span>type</span><span>Suppresion</span><span>Valider</span>";
        foreach ($return as $key=>$value){
            $affichage.= "<form method='post' action='formSubmit.html' id=formAdminGestion name='formAdminGestion'>";
            foreach ($value as $key2=>$value2){
                if($key2 === "image"){
                    $affichage.="<input value='".$value2."' name=".$key2." style=\"display: none;\">";
                    $affichage.="<img src='image/tmp/".$value2."' alt=Image_Upload>";
                }
                else $affichage.="<input value='".$value2."' name=".$key2.">";
            }
            $affichage.="<input type='text' id='alcoolemie' name='alcoolemie' ><input type='text' id='type' name='type'><input type='submit' name='suppr' value='suppression'><input type='submit' name=encoder value='Encoder'></form>";
        }
        $affichage.= "</div>";
        return $affichage;
    }
}