<?php

require_once 'db.inc.php';
require_once 'functions.inc.php';
if(count(get_included_files())==1) die('--access denied--');

class Config extends Db
{
    private $config = [];
    private $db;

    function __construct($fileName=null)
    {
        $this->db = new Db();
        if(isset($_SESSION['user'])) $this->config = $_SESSION['user'];
    }

    public function getForm() {
        $form = "<div id=gestionCompte>";
        $form .= "<form method='post' action='formSubmit.html' id='formCompte' name='formCompte enctype=\"multipart/form-data\"'>";
        $form .= "<div id='image'><img id='imageAvatar' src=image/avatar/".$this->config['photo']." width=100px height= 100px>";
        $form .= "<label for='file'><img src=image/crayon.png id=crayon width=30px height= 30px></label><input type=file name=file id=file accept=image/*></div>";
        foreach ($this->config as $key=>$value){
            switch ($key){
                case 'datenaiss': break;
                case 'iduser': break;
                case 'photo':  break;
                default : $form.= "<label for=".$key.">".$key."</label><input type=text id=".$key." name=".$key." value=".$value." required><br>";
            }
        }
        $form.= "<p id='erreur3'></p>";
        $form.= "<input type='submit' value='VALIDER'></form></div>";
        return $form;
    }

    public function getAchiv($start){
        $return = $this->db->call("achievementbyuser", [$_SESSION['user']['iduser']]);

        if($start > count($return)){
            $_SESSION['start'] = 0;
            $start = 0;
        }

        $array = array_slice($return, $start, 3);

        $fav = "<div class='sectionFav'>";
        $fav.= "<h1>Achievements</h1>";
        $fav.= "<ul class=achievFav>";
        $fav.= "<a id='prec' href='achievements.html'><img src='image/fleche gauche.png' class='fleche'></a>";
        foreach ($array as $key=>$value){
            $fav.= "<li id=".$value['idsucces']." title='".$value['descr']."'><img src='image/Achievements/".$value['image']."'>".$value['nom']."</li>";
        }
        $fav.= "<a id='suiv' href='achievements.html'><img src='image/fleche droite.png' class='fleche'></a>";
        $fav.= "</ul>";
        $fav.= "</div>";
        return $fav;
    }

    public function getFav($start){
        $return = $this->db->call("favorisbyuser", [$_SESSION['user']['iduser']]);

        if($start > count($return)){
            $_SESSION['startFav'] = 0;
            $start = 0;
        }

        $array = array_slice($return, $start, 3);

        $fav = "<div class='sectionFav'>";
        $fav.= "<h1>Favoris</h1>";
        $fav.= "<ul class=achievFav>";
        $fav.= "<a id='prec' href='favoris.html'><img src='image/fleche gauche.png' class='fleche2'></a>";
        foreach ($array as $key=>$value){
            $fav.= "<li id=".$value['idbiere']." title='".$value['typebiere']."(".$value['alcoolemie'].")'><img src='image/beers/".$value['image']."'>".$value['nombiere']."</li>";
        }
        $fav.= "<a id='suiv' href='favoris.html'><img src='image/fleche droite.png' class='fleche2'></a>";
        $fav.= "</ul>";
        $fav.= "</div>";
        return $fav;
    }
}