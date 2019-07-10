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

function HatAccount(Typ, SocialClub, Nickname) {
	if(Typ == 1)
	{
		$('.nav-tabs a[href="#loginTab"]').tab('show');
		document.getElementById('loginsocialClub').innerHTML = SocialClub;
		document.getElementById('loginnickName').innerHTML = Nickname;
		document.getElementById('registersocialClub').innerHTML = SocialClub;
		document.getElementById('registernickName').innerHTML = Nickname;
	}
	else
	{
		$('.nav-tabs a[href="#registerTab"]').tab('show');
		document.getElementById('registersocialClub').innerHTML = SocialClub;
		document.getElementById('registernickName').innerHTML = 'Nicht vorhanden';
		document.getElementById('loginsocialClub').innerHTML = SocialClub;
		document.getElementById('loginnickName').innerHTML = 'Nicht vorhanden';
	}
}


