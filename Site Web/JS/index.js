$(document).ready(function(){
    $('footer a').click(function (event) {
        event.preventDefault();
        appelAjax(this);
    });
    $('menu a').click(function (event) {
        event.preventDefault();
        appelAjax(this);
    });
    $('#gestion a').click(function (event) {
        event.preventDefault();
        appelAjax(this);
        $('#gestion').css('display', 'none');
    });
    $('#compte p').click(function (event) {
        event.preventDefault();
        $('#gestion').slideToggle();
    });
});

function appelAjax(element, name=''){
    $.ajaxSetup({ processData: false, contentType : false});
    var data= new FormData();
    var request = 'unknownUri';
    switch(true){
        case Boolean(element.href) : request = $(element).attr('href').split('.html')[0];
            break;
        case Boolean(element.action) :
            request = $(element).attr('action').split('.html')[0];
            break;
    }
    data= new FormData(element);
    data.append('senderId', element.id);
    data.append('senderName', name);
    $.post('index.php?rq='+request, data, gereRetour);
}

function gereRetour(retour){
    var cle;
    retour =testeJson(retour);
    for(cle in retour) {
        switch (cle) {
            case 'about' :
                $('main').html(retour[cle]);
                $("html,body").animate({ scrollTop: 0 }, "slow");
                $('#propos div:not(#propos div:first)').hide();
                $('#propos li').click(function () {
                    $('#propos div').hide();
                    $('#propos li').removeClass('selected');
                    $(this).addClass('selected');
                    var divId = parseInt($(this).attr('id'));
                    $('#propos div:eq('+divId+')').show(500);
                });
                break;
            case 'login' :
                $('body').append(retour[cle]);

                if($('body').outerWidth(true) < 500) {
                    $("<p id='CreateCompte'>Créer un compte</p>").insertAfter("#connexion input[type=submit]")
                    $('#inscription').hide();
                    $(document).on('click', '#CreateCompte', function (event) {
                        event.preventDefault();
                        $(this).remove();
                        $('#inscription').show();
                        $('#connexion').hide();
                    });
                }

                $('#inscription').submit(function (event){
                    event.preventDefault();
                    appelAjax(this);
                    $('.log').remove();
                });

                $('#connexion').submit(function (event){
                    event.preventDefault();
                    appelAjax(this);
                    $('.log').remove();
                });

                $('#retour').click(function (event) {
                    event.preventDefault();
                    $('.log').remove();
                });
                break;
            case 'connexion':
                var x= JSON.parse(retour[cle]);
                var affiche = "<img id=avatar src=image/avatar/"+x['photo']+" height=50px width=50px alt='Avatar'>"+"<p>"+x["pseudo"]+"</p><p id='liste'>&#9662;</p>";
                $('#compte').html(affiche);
                $('#compte p').click(function (event) {
                    event.preventDefault();
                    $('#gestion').slideToggle();
                });
                break;
            case 'logout':
                $('#compte').html(retour[cle]);
                $('#compte a').click(function (event) {
                    event.preventDefault();
                    appelAjax(this);
                });
                $('#gestion').css('display', 'none');
                break;
            case 'compte':
                $('main').html(retour[cle]);
                $('#file').change(function () {
                    if (this.files[0]) {
                        var reader = new FileReader();          //permet d'intercepté un fichier et de lire dans la mémoire tampon

                        reader.onload = function (e) {          //permet de lancer la fonction quand le fichier est bien chargé
                            $('#imageAvatar')
                                .attr('src', e.target.result);  //e.target = this
                        };

                        reader.readAsDataURL(this.files[0]);    //
                    }
                });
                $('#formCompte').submit(function (event) {
                    event.preventDefault();
                    appelAjax(this);
                });
                $('#gestionCompte a').click(function (event) {
                    event.preventDefault();
                    $('#listeGestionCompte a').removeClass('selected');
                    $(this).addClass('selected');
                    appelAjax(this);
                });
                break;
            case "erreur1":
                $('#erreur1').html(retour[cle]);
                break;
            case "erreur2":
                $('#erreur2').html(retour[cle]);
                break;
            case "erreur3":
                $('#erreur3').html(retour[cle]);
                break;
            case 'erreur4':
                $('#erreur4').html(retour[cle]);
                break;
            case 'achiev':
                $('#contenuCompte').html(retour[cle]);
                $('main a').click(function (event) {
                    event.preventDefault();
                    appelAjax(this);
                });
                break;
            case 'gestionCompte':
                $('#contenuCompte').html(retour[cle]);
                $('#table form input[type=submit]').click(function (e) {
                    var x = e.target.name;
                    $('#table form').submit(function (event) {
                        event.preventDefault();
                        appelAjax(this, x);
                    });
                });
                $('#contenuCompte a').click(function (event) {
                    event.preventDefault();
                    appelAjax(this);
                });
                break;
            case 'adminconnexion':
                $('#contenuCompte').html(retour[cle]);
                $('#listeGestionCompte a').removeClass('selected');
                $('#listeGestionCompte a:first').addClass('selected');
                $("#formAdmin").submit(function (event) {
                    event.preventDefault();
                    appelAjax(this);
                });
                break;
            default: $('main').html(retour[cle]);
        }
    }
}

function testeJson(json){
    var parsed;
    try {
        parsed = JSON.parse(json);
    } catch (e){
        parsed = {
            'jsonError': {
                'Error: ': e,
                'Json: ': json
            }
        }
    }
    return parsed;
}