var fahrzeugverwaltung;
let player = mp.players.local;

mp.events.add('Fahrzeugverwaltung_Privat', (Name, Tank, KM, HU, Abgeschlossen) => {
    fahrzeugverwaltung = mp.browsers.new('package://Fahrzeuge/FVerwaltung_Privat/fahrzeugverwaltung_privat.html');
	mp.gui.cursor.show(true, true);
	var verschlossen = Abgeschlossen;
	
	fahrzeugverwaltung.execute(`document.getElementById('Name').innerHTML = '${Name}';`);﻿
	fahrzeugverwaltung.execute(`document.getElementById('Tank').innerHTML = '${Tank}';`);﻿
	fahrzeugverwaltung.execute(`document.getElementById('Kilometerstand').innerHTML = '${KM}';`);﻿
	fahrzeugverwaltung.execute(`document.getElementById('HU').innerHTML = '${HU}';`);﻿

	if(verschlossen == 1)
	{
		fahrzeugverwaltung.execute(`document.getElementById("abgeschlossen").checked = true;`);﻿
	}
	else
	{
		fahrzeugverwaltung.execute(`document.getElementById("abgeschlossen").checked = false;`);﻿
	}
});

mp.events.add('FVerwaltung_Privat_Abbrechen', () => {
    mp.events.callRemote('Fahrzeugverwaltung_Privat_Abbrechen');
	fahrzeugverwaltung.destroy();
	mp.gui.cursor.show(false, false);
});

mp.events.add('abschließen', (state) => {
    mp.events.callRemote('Fahrzeug_Privat_Abschliessen', state);
});




