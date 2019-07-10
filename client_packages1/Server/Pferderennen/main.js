var pferderennen;
let player = mp.players.local;

mp.events.add('Pferderennenoeffnen', (Typ) => {
    pferderennen = mp.browsers.new('package://Server/Pferderennen/pferderennen.html');
	mp.gui.cursor.show(true, true);
	
});

mp.events.add('Pferderennenschliessen', () => {
	pferderennen.destroy();
	mp.gui.cursor.show(false, false);
});








