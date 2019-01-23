$('#loginbutton').click(() => {

    $('.alert').remove(); 
    mp.trigger('loginzumserver', $('#loginpasswort').val());
});

$('#registrierenbutton').click(() => {

    $('.alert').remove(); 
    mp.trigger('registrierenzumserver', $('#registrierenpasswort').val());
});

