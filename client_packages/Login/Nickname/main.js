var nickNameBrowser;
mp.events.add('nicknamebrowseroeffnen', () => {
    nickNameBrowser = mp.browsers.new('package://Login/Nickname/nickname.html');
	mp.gui.cursor.show(true, true);
});

mp.events.add('nicknamebrowserschliessen', () => {
    nickNameBrowser.destroy();
	mp.gui.cursor.show(false, false);
});

mp.events.add('nicknamezumserver', (nickname) => {
    mp.events.callRemote('NicknameVersuch', nickname);
});
