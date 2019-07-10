var rollermieten;
let player = mp.players.local;
mp.events.add('rollermietenpopupoeffnen', (preis) => {
    rollermieten = mp.browsers.new('package://Server/Popup/popup.html');
	mp.gui.cursor.show(true, true);
	rollermieten.execute(`document.getElementById('miettext').innerHTML = 'Möchtest du das Fahrzeug für ${preis} mieten?';`);﻿
});

mp.events.add('rollermietenpopupschliessen', () => {
    rollermieten.destroy();
	mp.gui.cursor.show(false, false);
});

mp.events.add('rollermieten', () => {
    mp.events.callRemote('RollerMieten');
});

mp.events.add('rollernichtmieten', () => {
    mp.events.callRemote('RollerNichtMieten');
});


