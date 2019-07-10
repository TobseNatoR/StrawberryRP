var busfahrer;
let player = mp.players.local;

mp.events.add('busfahrerbrowseroeffnen', () => {
    busfahrer = mp.browsers.new('package://Jobs/Busfahrer/busfahrer.html');
	mp.gui.cursor.show(true, true);
	mp.gui.chat.show(false);
});

mp.events.add('busfahrerbrowserschliessen', () => {
	busfahrer.destroy();
	mp.gui.cursor.show(false, false);
	mp.gui.chat.show(true);
});

mp.events.add('busdateneintragen', () => {
	mp.events.callRemote('JobBusfahrerRoute1');
});

mp.events.add('route1zumserver', () => {
	mp.events.callRemote('JobBusfahrerRoute1');
});

mp.events.add('route2zumserver', () => {
	mp.events.callRemote('JobBusfahrerRoute2');
});

mp.events.add('route3zumserver', () => {
	mp.events.callRemote('JobBusfahrerRoute3');
});

mp.events.add('route4zumserver', () => {
	mp.events.callRemote('JobBusfahrerRoute4');
});






