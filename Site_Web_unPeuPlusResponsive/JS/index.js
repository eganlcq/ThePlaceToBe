$(document).ready(function(){
    $('#societe a').click(function (event) {
        event.preventDefault();
        appelAjax(this);
    });
    $('menu a').click(function (event) {
        event.preventDefault();
        appelAjax(this);
    });
    $('#liste').click(function (event) {
        event.preventDefault();
        var bool = ($('#gestion').is(':visible'))? $('#gestion').css('display', bool).fadeOut(500) : $('#gestion').css('display', bool).fadeIn(500);
    });
});

function appelAjax(element){
    $.ajaxSetup({ processData: false, contentType : false});
    var data= new FormData();
    var request = 'unknownUri';
    switch(true){
        case Boolean(element.href) : request = $(element).attr('href').split('.html')[0];
            break;
        case Boolean(element.action) : request = $(element).attr('action').split('.html')[0]; data= new FormData(element); break;
    }
    data.append('senderId', element.id);
    $.post('index.php?rq='+request, data, gereRetour);
}

function gereRetour(retour){
    var cle;
    retour =testeJson(retour);
    for(cle in retour) {
        switch (cle) {
            case 'about' : $('main').html(retour[cle]);
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
            case 'login' : $('body').append(retour[cle]);
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
            case 'slogan' : $('main').html(retour[cle]); break;
            case 'compte':
                $('main').html(retour[cle]);
                break;
            case 'connexion':
                var x= JSON.parse(retour[cle]);
                var affiche = "<img id=avatar src="+x['photo']+" height=50px width=50px>"+"<p>"+x["pseudo"]+"</p><p id='liste'>&#9662;</p>";
                $('#compte').html(affiche);
                $('#liste').click(function (event) {
                    event.preventDefault();
                    var bool = ($('#gestion').is(':visible'))? $('#gestion').css('display', bool).fadeOut(500) : $('#gestion').css('display', bool).fadeIn(500);
                });
                break;
            case 'logout':
                $('#compte').html(retour[cle]);
                $('menu a').click(function (event) {
                    event.preventDefault();
                    appelAjax(this);
                });
                $('#gestion').css('display', 'none');
                break;
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