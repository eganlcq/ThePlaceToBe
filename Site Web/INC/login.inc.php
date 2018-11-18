<?php if(count(get_included_files())==1) die('--access denied--'); ?>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Login</title>
</head>
<body>
<div id="fond" class="log"></div>
<div id="login" class="log container">
    <div class="row">
        <div class="col-lg-6 col-xs-6 col-md-6 col-sm-6">
            <form id="inscription" name="inscription" method="post" action="formSubmit.html">
                <legend>Inscription</legend>
                <input type="text" name="pseudo" id="pseudoInscr" placeholder="nom d'utilisateur" required>
                <input type="password" name="pwdInscr" id="pwdInscr" placeholder="Mot de passe" required>
                <input type="text" name="nom" id="nom" placeholder="Nom" required>
                <input type="text" name="prenom" id="prenom" placeholder="Prénom" required>
                <input type="text" name="email" id="mail" placeholder="Adresse électronique" required>
                <input type="date" name="datenaiss" id="datenaiss" placeholder="YYYY MM DD" required>
                <p id="erreur1"></p>
                <input type="submit" value="S'INSCRIRE">
            </form>
        </div>
        <div class="col-lg-6 col-xs-6 col-md-6 col-sm-6">
            <form id="connexion" name="connexion" method="post" action="formSubmit.html">
                <legend>Connexion</legend>
                <input type="text" name="pseudo" id="pseudo" placeholder="nom d'utilisateur" required>
                <input type="password" name="pwd" id="pwd" placeholder="Mot de passe" required>
                <p id="erreur2"></p>
                <input type="submit" value="SE CONNECTER">
            </form>
        </div>
    </div>
    <div class="row">
        <input type="button" id="retour" class="col-lg-6 col-xs-6 col-md-6 col-sm-6" name="retour" value="RETOUR">
    </div>
</div>
</body>
</html>
