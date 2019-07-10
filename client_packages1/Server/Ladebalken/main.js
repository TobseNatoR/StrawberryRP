var Ladebalken;
let player = mp.players.local;

mp.events.add('ladebalkenoeffnen', () => {
    Ladebalken = mp.browsers.new('package://Server/Ladebalken/index.html');
	mp.gui.cursor.show(false, false);
});

mp.events.add('ladebalkenschliessen', () => {
	Ladebalken.destroy();
	mp.gui.cursor.show(false, false);
});

mp.events.add('Ladebalken', (Typ, Zeit) => {
	Ladebalken.execute(`Ladebalken('${Typ}', '${Zeit}');`);
});







