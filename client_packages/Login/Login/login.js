$('form').keypress(function(e) { 
    return e.keyCode != 13;
});

$('#loginbutton').click(() => {

    $('.alert').remove(); 
    mp.trigger('loginzumserver', $('#loginpasswort').val());
});

$('#registrierenbutton').click(() => {

    $('.alert').remove(); 
    mp.trigger('registrierenzumserver', $('#registrierenpasswort').val());
});

function LoginRegisterEnter() {
	document.getElementById('loginpasswort').blur();
	document.getElementById('registrierenpasswort').blur();
	let loginPasswort = document.getElementById('loginpasswort').value;
	let registerPasswort = document.getElementById('registrierenpasswort').value;
	
	if(loginPasswort == "")
	{
		mp.trigger('registrierenzumserver', registerPasswort);
		document.getElementById("loginpasswort").reset();
	}
	else
	{
		mp.trigger('loginzumserver', loginPasswort);
		document.getElementById("registrierenpasswort").reset();
	}
}