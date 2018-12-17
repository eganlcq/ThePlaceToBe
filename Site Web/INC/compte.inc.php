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
        $form = "<div id=gestionCompte><ul id='listeGestionCompte'>";
        if(isAdmin($_SESSION['user']['pseudo'], $_SESSION['user']['email'])){
            $form .= "<li><a href=admin.html>Admin</a></li>";
        }
        $form .= "<li><a class='selected' href=compte.html>Profile</a></li>";
        $form .= "<li><a href=achievements.html>Achievements</a></li>";
        $form .= "<li><a href=favoris.html>Favoris</a></li>";
        $form .= "<li style='vertical-align:middle; float: right;'><a href='slogan.html'><i class='glyphicon glyphicon-remove' style='color: white'></i></a></li></ul><div id='contenuCompte'>";
        $form .= "<form method='post' action='formSubmit.html' id='formCompte' name='formCompte enctype=\"multipart/form-data\"'>";
        $form .= "<div id='image'><img id='imageAvatar' src=image/avatar/".$this->config['photo']." width=100px height= 100px alt='Avatar'>";
        $form .= "<label for='file'><img src=image/crayon.png id=crayon width=30px height= 30px alt='Crayon'></label><input type=file name=file id=file accept=image/*></div>";
        foreach ($this->config as $key=>$value){
            switch ($key){
                case 'datenaiss': break;
                case 'iduser': break;
                case 'photo':  break;
                case 'nbajout': break;
                case 'nbajoutprecedent': break;
                default : $form.= "<label for=".$key.">".$key."</label><input type=text id=".$key." name=".$key." value=".$value." required><br>";
            }
        }
        $form.= "<p id='erreur3'></p>";
        $form.= "<input type='submit' value='VALIDER'></form></div></div>";
        return $form;
    }

    public function getAchiv($start){
        $return = $this->db->call("achievementbyuser", [$_SESSION['user']['iduser']]);

        if($start > count($return)){
            $_SESSION['start'] = 0;
            $start = 0;
        }
        $array = array_slice($return, $start, 3);

        $fav = "<div class=containerAchiev class='container'><div class='sectionFav row'>";
        $fav.= "<a id='prec' href='achievements.html' class='col-lg-3 col-xs-4 col-md-3 col-sm-3'><img class='imgResize' src='image/fleche gauche.png' alt='fleche'></a>";
        $fav.= "<ul class='achievFav col-lg-6 col-xs-4 col-md-6 col-sm-6'>";
        foreach ($array as $key=>$value){
            $fav.= "<li id=".$value['idsucces']." title='".$value['descr']."'><img class=imgResize src='image/Achievements/".$value['image']."' alt='fleche'>".$value['nom']."</li>";
        }
        $fav.= "</ul>";
        $fav.= "<a id='suiv' href='achievements.html' class='col-lg-3 col-xs-4 col-md-3 col-sm-3'><img class='imgResize' src='image/fleche droite.png' alt='fleche'></a>";
        $fav.= "</div></div>";
        return $fav;
    }

    public function getFav($start){
        $return = $this->db->call("favorisbyuser", [$_SESSION['user']['iduser']]);

        if($start > count($return)){
            $_SESSION['startFav'] = 0;
            $start = 0;
        }

        $array = array_slice($return, $start, 3);

        $fav = "<div class=containerAchiev class='container'><div class='sectionFav row'>";
        $fav.= "<a id='prec' href='favoris.html' class='col-lg-3 col-xs-4 col-md-3 col-sm-3'><img class='imgResize' src='image/fleche gauche.png' alt='fleche'></a>";
        $fav.= "<ul class='achievFav col-lg-6 col-xs-4 col-md-6 col-sm-6'>";
        foreach ($array as $key=>$value){
            $fav.= "<li id=".$value['idbiere']." title='".$value['typebiere']."(".$value['alcoolemie'].")'><img class=imgResize src='image/beers/".$value['image']."' alt='Bière_favorite'>".$value['nombiere']."</li>";
        }
        $fav.= "</ul>";
        $fav.= "<a id='suiv' href='favoris.html' class='col-lg-3 col-xs-4 col-md-3 col-sm-3'><img class='imgResize' src='image/fleche droite.png' alt='fleche'></a>";
        $fav.= "</div>";
        return $fav;
    }
}