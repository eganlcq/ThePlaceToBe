<?php if(count(get_included_files())==1) die('--access denied--'); ?>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Login</title>
</head>
<body>
<section id="propos">
    <ul>
        <li id="0" class="selected">Descriptif</li>
        <li id="1">Membres</li>
        <li id="2">Siège</li>
    </ul>
    <div id="first">
        <p>ThePlaceToBe est une application Android dont le but est de permettre aux utilisateurs d'obtenir la localisation des différents bars en fonction d'une bière recherchée et de sa localisation.<br>
            A cela se rajoute le fait que l'utilisateur à la possibilité de stocker ses bières favorites.</p>
        <video poster="/image/ThePlaceToBe.png" controls>
            <source src="video/video.mp4" type="video/mp4">
        </video>
    </div>
    <div>
        <p>L'équipe se constitue de 6 étudiants de l'Ephec de Louvain-la-Neuve en dernière année de baccalauréat.<br></p>
        <section id="profil" class="container">
            <section class="row">
                <section class="col-lg-3 col-xs-6 col-md-3 col-sm-4">
                    <img class="imgResize" src="../image/Chimay.png" alt="Image_Membre_Chimay">
                    <p>Jacobs David</p>
                </section>
                <section class="col-lg-3 col-xs-6 col-md-3 col-sm-4">
                    <img class="imgResize" src="../image/guinness.jpg" alt="Image_Membre_Guinness">
                    <p>Egan Lecocq</p>
                </section>
                <section class="col-lg-3 col-xs-6 col-md-3 col-sm-4">
                    <img class="imgResize" src="../image/tortuga.png" alt="Image_Membre_Tortuga">
                    <p>Luca Mellini</p>
                </section>
            </section>
            <section class="row">
                <section class="col-lg-3 col-xs-6 col-md-3 col-sm-4">
                    <img class="imgResize" src="../image/triple.jpg" alt="Image_Membre_Triple">
                    <p id="jordan">Jordan Vankeerberghen</p>
                </section>
                <section class="col-lg-3 col-xs-6 col-md-3 col-sm-4">
                    <img class="imgResize" src="../image/westmaell.png" alt="Image_Membre_Westmaell">
                    <p>Guillaume Wyart</p>
                </section>
                <section class="col-lg-3 col-xs-6 col-md-3 col-sm-4">
                    <img class="imgResize" src="../image/kidibul.jpg" alt="Image_Membre_Kidibul">
                    <p>Nicolas Rosar</p>
                </section>
            </section>
        </section>
    </div>
    <div>
        <img class="imgResize" src="../image/iconFooter.png" id="logo" alt="Logo_ThePlaceToBe">
        <p><b>Ephec Belgique</b>,<br>Avenue du Ciseau 15,<br>1348 Ottignies-Louvain-la-Neuve</p>
        <img class="imgResize" id="map" src="../image/map.PNG" alt="Map_QG">
    </div>
</section>
</body>
</html>
