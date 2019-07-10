var perso;
let player = mp.players.local;

mp.events.add('persozeigen', (nickname, gebby, einreise, personummer) => {
    perso = mp.browsers.new('package://Server/Perso/perso.html');
	perso.execute(`document.getElementById('personummer').innerHTML = '&nbsp &nbsp T0000${personummer}';`);﻿
	perso.execute(`document.getElementById('nickname').innerHTML = '&nbsp &nbsp ${nickname}';`);﻿
	perso.execute(`document.getElementById('gebby').innerHTML = '&nbsp &nbsp ${gebby}';`);﻿
	perso.execute(`document.getElementById('einreisedatum').innerHTML = '&nbsp &nbsp ${einreise}';`);﻿
});

mp.events.add('persoschliessen', () => {
    perso.destroy();
	mp.events.callRemote('PersoSchliessen');
});



