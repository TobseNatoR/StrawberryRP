var berufskraftfahrer;
let player = mp.players.local;

mp.events.add('berufskraftfahrerbrowseroeffnen', () => {
    berufskraftfahrer = mp.browsers.new('package://Jobs/Berufskraftfahrer/berufskraftfahrer.html');
	mp.gui.cursor.show(true, true);
});

mp.events.add('berufskraftfahrerbrowserschliessen', () => {
	berufskraftfahrer.destroy();
	mp.gui.cursor.show(false, false);
});

mp.events.add('berufskraftfahrerholz', () => {
	mp.events.callRemote('JobBerufskraftfahrerHolz');
});

mp.events.add('berufskraftfahrerdiesel', () => {
	mp.events.callRemote('JobBerufskraftfahrerKraftstoff', 1);
});

mp.events.add('berufskraftfahrere10', () => {
	mp.events.callRemote('JobBerufskraftfahrerKraftstoff', 2);
});

mp.events.add('berufskraftfahrersuper', () => {
	mp.events.callRemote('JobBerufskraftfahrerKraftstoff', 3);
});





