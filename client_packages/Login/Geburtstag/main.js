var geburtstagBrowser;
mp.events.add('geburtstagbrowseroeffnen', () => {
    geburtstagBrowser = mp.browsers.new('package://Login/Geburtstag/geburtstag.html');
	mp.gui.cursor.show(true, true);
});

mp.events.add('geburtstagbrowserschliessen', () => {
    geburtstagBrowser.destroy();
	mp.gui.cursor.show(false, false);
});

mp.events.add('geburtstagzumserver', (datum) => {
    mp.events.callRemote('GeburtstagVersuch', datum);
});
