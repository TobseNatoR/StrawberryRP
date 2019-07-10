var loginBrowser;
mp.events.add('browseroeffnen', () => {
    loginBrowser = mp.browsers.new('package://Login/Login/login.html');
	mp.gui.cursor.show(true, true);
});

mp.events.add('browserschliessen', () => {
    loginBrowser.destroy();
	mp.gui.cursor.show(false, false);
});

mp.events.add('loginzumserver', (passwort) => {
    mp.events.callRemote('LoginVersuch', passwort);
});

mp.events.add('registrierenzumserver', (passwort) => {
    mp.events.callRemote('RegistrierenVersuch', passwort);
});

mp.events.add('LoginEnterTaste', () => {
    loginBrowser.execute(`LoginRegisterEnter();`);
});

mp.events.add('HatAccount', (Typ, SocialClub, Nickname) => {
	loginBrowser.execute(`HatAccount('${Typ}', '${SocialClub}', '${Nickname}');`);
});