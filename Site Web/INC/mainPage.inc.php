<!DOCTYPE html>
<html lang=fr>
<head>
    <?php if(count(get_included_files())==1) die('--access denied--'); ?>
    <title>ThePlaceToBe</title>
    <meta charset=utf-8>
    <meta name="viewport" content="width=device-width">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous">
    <link rel="stylesheet" href="CSS/mainPage.css">
    <link rel="stylesheet" media="screen and (max-width: 500px) and (orientation: portrait)" href="CSS/mobile.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript" src="JS/index.js"></script>
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
    <div id='gestion'>
        <ul>
            <li><a href="compte.html">Gérer compte</a></li>
            <li><a href="achievements.html">Achievements</a></li>
            <li><a href="favoris.html">Favoris</a></li>
            <li><a href="logout.html">Déconnexion</a></li>
        </ul>
    </div>
    <main class="container">
        <div class="row">
            <div id="slogan" class="col-lg-6 col-xs-12 col-md-6 col-sm-12">
                <h1>Que la bière</h1><br>
                <h1 id="second">coule à flot</h1>
            </div>
            <div id="app" class="col-lg-6 col-xs-12 col-md-6 col-sm-12">
                <img class="imgResize" src="../image/ThePlaceToBe.png">
                <input type="button" value="Download application">
            </div>
        </div>
    </main>
    <footer class="container">
        <div class="row">
            <div class="headFoot col-lg-4 col-xs-4 col-md-4 col-sm-4">
                <img src="../image/iconFooter.png">
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
                    <li><a href="https://www.facebook.com/The-place-to-be-520936104980844/?notif_id=1539260456516354&notif_t=page_admin" target="_blank"><img src="../image/facebook.png"></a></li>
                    <li><a href="https://twitter.com/ThePlaceToBe5" target="_blank"><img src="../image/twitter.png"></a></li>
                    <li><a href="https://www.instagram.com/theplacetobe6/" target="_blank"><img src="../image/insta.png"></a></li>
                </ul>
            </div>
        </div>
        <div class="row legalDiv">
            <div id="legal" class="col-lg-12 col-xs-12 col-md-12 col-sm-12">
                <ul>
                    <li><a>protection des données</a></li>
                    <li><a>legal</a></li>
                    <li><a>centre de confidentialité</a></li>
                </ul>
            </div>
        </div>
    </footer>
</body>
</html>
