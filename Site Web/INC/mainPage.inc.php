<!DOCTYPE html>
<html lang=fr>
<head>
    <?php if(count(get_included_files())==1) die('--access denied--'); ?>
    <title>ThePlaceToBe</title>
    <meta charset=utf-8>
    <meta name="viewport" content="width=device-width">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous">
    <link rel="stylesheet" href="CSS/mainPage.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript" src="JS/index.js"></script>
    <noscript>
        <div id="javascript">

        </div>
    </noscript>
</head>
<body>
    <menu>
        <div id=bandeau>
            <h1><a href="slogan.html">ThePlaceToBe</a></h1>
        </div>
        <div id="compte">
            <?php echo $compte ?>
        </div>
    </menu>
    <div id="blanc"></div>
    <div id='gestion'>
        <ul>
            <li><a href="compte.html">Gérer compte</a></li>
            <li><a href="logout.html">Déconnexion</a></li>
        </ul>
    </div>
    <main class="container">
        <div class="row" id="acceuil">
            <div id="slogan" class="col-lg-6 col-xs-12 col-md-6 col-sm-12">
                <h1>Que la bière</h1><br>
                <h1 id="second">coule à flot</h1>
            </div>
            <div id="app" class="col-lg-6 col-xs-12 col-md-6 col-sm-12">
                <img class="imgResize" src="../image/ThePlaceToBe.png" alt="Image_Logo_ThePlaceToBe">
                <input type="button" value="Télécharger application">
            </div>
        </div>
    </main>
    <footer class="container">
        <div class="row">
            <div class="headFoot col-lg-4 col-xs-4 col-md-4 col-sm-4">
                <img src="../image/iconFooter.png" alt="Image_Logo_ThePlaceToBe">
                <h1>ThePlaceToBe</h1>
            </div>
            <div id="societe" class="headFoot col-lg-4 col-xs-4 col-md-4 col-sm-4">
                <h1>Société</h1>
                <ul>
                    <li><a href="about.html">à propos</a></li>
                    <li><a href="contact.html">contact</a></li>
                </ul>
            </div>
            <div class="headFoot col-lg-4 col-xs-4 col-md-4 col-sm-4">
                <ul id="social">
                    <li><a href="https://www.facebook.com/The-place-to-be-520936104980844/?notif_id=1539260456516354&notif_t=page_admin" target="_blank"><img src="../image/facebook.png" alt="Logo_Facebook"></a></li>
                    <li><a href="https://twitter.com/ThePlaceToBe5" target="_blank"><img src="../image/twitter.png" alt="Logo_Twitter"></a></li>
                    <li><a href="https://www.instagram.com/theplacetobe6/" target="_blank"><img id=insta src="../image/insta.png" alt="Logo_Instagram"></a></li>
                </ul>
            </div>
        </div>
        <div class="row legalDiv">
            <div id="legal" class="col-lg-12 col-xs-12 col-md-12 col-sm-12">
                <ul>
                    <li><a href="rgdp.html">protection des données</a></li>
                    <li><a href="legal.html">legal</a></li>
                    <li><a>centre de confidentialité</a></li>
                </ul>
            </div>
        </div>
    </footer>
</body>
</html>
