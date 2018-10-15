<!DOCTYPE html>
<html lang=fr>
<head>
    <?php if(count(get_included_files())==1) die('--access denied--'); ?>
    <title>ThePlaceToBe</title>
    <meta charset=utf-8>
    <link rel="stylesheet" href="CSS/mainPage.css">
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
        <div id='gestion'>
            <ul>
                <li><a href="compte.html">Gérer compte</a></li>
                <li><a href="logout.html">Déconnexion</a></li>
            </ul>
        </div>
    </menu>
    <main>
        <div id="slogan">
            <h1>Que la bière</h1><br>
            <h1 id="second">coule à flot</h1>
        </div>
        <div id="app">
            <img src="../image/ThePlaceToBe.png">
            <input type="button" value="Download application">
        </div>
    </main>
    <footer>
        <div class="headFoot">
            <img src="../image/iconFooter.png">
            <h1>ThePlaceToBe</h1>
        </div>
        <div id="societe" class="headFoot">
            <h1>Société</h1>
            <ul>
                <li><a href="about.html">à propos</a></li>
                <li><a href="contact.html">contact</a></li>
            </ul>
        </div>
        <div class="headFoot">
            <ul id="social">
                <li><a href="https://www.facebook.com/The-place-to-be-520936104980844/?notif_id=1539260456516354&notif_t=page_admin"><img src="../image/facebook.png"></a></li>
                <li><a href="https://twitter.com/ThePlaceToBe5"><img src="../image/twitter.png"></a></li>
                <li><a href="https://www.instagram.com/theplacetobe6/"><img src="../image/insta.png"></a></li>
            </ul>
        </div>
        <div id="legal">
            <ul>
                <li><a>protection des données</a></li>
                <li><a>cookies</a></li>
                <li><a>legal</a></li>
                <li><a>centre de confidentialité</a></li>
            </ul>
        </div>
    </footer>
</body>
</html>
