<?php

if(count(get_included_files())==1) die('--access denied--');

class Config
{
    private $config = [];

    function __construct($fileName=null)
    {
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

    public function getAchiv(){
        /*$achiv = "<div id=achiv>";
        $achiv.=*/
    }
}