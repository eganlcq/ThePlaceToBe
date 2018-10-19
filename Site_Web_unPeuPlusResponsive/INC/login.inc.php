<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Login</title>
</head>
<body>
<div id="fond" class="log"></div>
<div id="login" class="log">
    <div>
        <form id="inscription" name="inscription" method="post" action="formSubmit.html">
            <legend>Inscription</legend>
            <input type="text" name="prenom" id="prenom" placeholder="Prénom" required>
            <input type="text" name="nom" id="nom" placeholder="Nom" required>
            <input type="text" name="mail" id="mail" placeholder="Adresse électronique" required>
            <input type="password" name="pwd" id="pwd" placeholder="Mot de passe" required>
            <input type="text" name="pseudo" id="pseudo" placeholder="nom d'utilisateur" required>
            <input type="submit" value="S'INSCRIRE">
        </form>
    </div>
    <div>
        <form id="connexion" name="connexion" method="post" action="formSubmit.html">
            <legend>Connexion</legend>
            <input type="text" name="pseudo" id="pseudo" placeholder="nom d'utilisateur" required>
            <input type="password" name="pwd" id="pwd" placeholder="Mot de passe" required>
            <input type="submit" value="SE CONNECTER">
        </form>
    </div>
    <input type="button" id="retour" name="retour" value="RETOUR">
</div>
</body>
</html>
