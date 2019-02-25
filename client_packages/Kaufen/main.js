var kaufen;
let player = mp.players.local;
mp.events.add('Kaufen', (Typ, Preis) => {
    kaufen = mp.browsers.new('package://Kaufen/kaufen.html');
	mp.gui.cursor.show(true, true);
	
	if(Typ == 1)
	{
		kaufen.execute(`document.getElementById('kaufentext').innerHTML = 'Tankstelle kaufen';`);﻿
		kaufen.execute(`document.getElementById('text').innerHTML = 'Möchtest du die Tankstelle für <b>${Preis}</b> kaufen?';`);﻿
	}
	else if(Typ == 2)
	{
		kaufen.execute(`document.getElementById('kaufentext').innerHTML = 'Immobilie kaufen';`);﻿
		kaufen.execute(`document.getElementById('text').innerHTML = 'Möchtest du die Immobilie für <b>${Preis}</b> kaufen?';`);﻿
	}
	else if(Typ == 3)
	{
		kaufen.execute(`document.getElementById('kaufentext').innerHTML = '24/7 kaufen';`);﻿
		kaufen.execute(`document.getElementById('text').innerHTML = 'Möchtest du den 24/7 für <b>${Preis}</b> kaufen?';`);﻿
    }
    else if (Typ == 4)
    {
        kaufen.execute(`document.getElementById('kaufentext').innerHTML = 'Autohaus kaufen';`);
        kaufen.execute(`document.getElementById('text').innerHTML = 'Möchtest du das Autohaus für <b>${Preis}</b> kaufen?';`);
    }
	
});

mp.events.add('kaufenabbrechen', () => {
	kaufen.destroy();
	mp.gui.cursor.show(false, false);
	mp.events.callRemote('KaufenAbbrechen');
});

mp.events.add('kaufenversuch', () => {
    mp.events.callRemote('Kaufen');
});



