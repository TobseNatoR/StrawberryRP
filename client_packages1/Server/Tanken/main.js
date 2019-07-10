var tanken;
let player = mp.players.local;
mp.events.add('Tanken', (Tankvolumen, Tankinhalt, Restwert, Diesel, E10, Super) => {
    tanken = mp.browsers.new('package://Server/Tanken/tanken.html');
	mp.gui.cursor.show(true, true);
	
	tanken.execute(`document.getElementById('Diesel').innerHTML = 'Diesel: <b>${Diesel}</b>$ pro Liter.';`);﻿
	tanken.execute(`document.getElementById('E10').innerHTML = 'Super E10: <b>${E10}</b>$ pro Liter.';`);﻿
	tanken.execute(`document.getElementById('Super').innerHTML = 'Super: <b>${Super}</b>$ pro Liter.';`);﻿
	
	tanken.execute(`document.getElementById('tankvolumen').innerHTML = 'Dein Tank kann <b>${Tankvolumen}</b> Liter fassen.';`);﻿
	tanken.execute(`document.getElementById('tankinhalt').innerHTML = 'Der Tankinhalt beträgt <b>${Tankinhalt}</b> Liter';`);﻿
	tanken.execute(`document.getElementById('tankvoll').innerHTML = 'Es fehlen <b>${Restwert}</b> Liter bis der Tank voll ist.';`);﻿
	
});

mp.events.add('tankenabbrechen', () => {
    mp.events.callRemote('TankenAbbrechen');
	tanken.destroy();
	mp.gui.cursor.show(false, false);
});

mp.events.add('tankenschliessen', () => {
    tanken.destroy();
	mp.gui.cursor.show(false, false);
});

mp.events.add('auftanken', (liter, diesel, e10, superbenzin) => {
    mp.events.callRemote('Tanken', liter, diesel, e10, superbenzin);
});



