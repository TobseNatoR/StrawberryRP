var laden;
let player = mp.players.local;
mp.events.add('Laden', () => {
    laden = mp.browsers.new('package://Login/Laden/index.html');
	mp.gui.cursor.show(true, true);
});

mp.events.add('LadenBeenden', () => {
	laden.destroy();
	mp.gui.cursor.show(false, false);
});



