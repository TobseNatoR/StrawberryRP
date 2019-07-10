var tankstellenliste;
let player = mp.players.local;


var tabelle;
var eintrag;
var zelle1;
var zelle2;
var zelle3;
var zelle4;
var zelle5;
var zelle6;

var inhalt1;
var inhalt2;
var inhalt3;
var inhalt4;
var inhalt5;
var inhalt6;

mp.events.add('Berufskraftfahrer_Tankstellenbrowseroeffnen', (Typ) => {
    tankstellenliste = mp.browsers.new('package://Jobs/Berufskraftfahrer/tankstellen.html');
	mp.gui.cursor.show(true, true);
	
	if(Typ == 1)
	{
		tankstellenliste.execute(`document.getElementById('tankstellentext').innerHTML = 'Welche Tankstelle möchtest du mit Diesel beliefern?';`);﻿
	}
	else if(Typ == 2)
	{
		tankstellenliste.execute(`document.getElementById('tankstellentext').innerHTML = 'Welche Tankstelle möchtest du mit E10 beliefern?';`);﻿
	}
	else if(Typ == 3)
	{
		tankstellenliste.execute(`document.getElementById('tankstellentext').innerHTML = 'Welche Tankstelle möchtest du mit Super beliefern?';`);﻿
    }
});

mp.events.add('Berufskraftfahrer_Tankstelleeintragen', (Id, Name, Kraftstoff, Entfernung, Verdienst) => {
	Eintragen(Id, Name, Kraftstoff, Entfernung, Verdienst);
});

function Eintragen(Id, Name, Kraftstoff, Entfernung, Verdienst) 
{
	tankstellenliste.execute(`tabelle = document.getElementById('jobtankentabelle').getElementsByTagName('tbody')[0];`);
	tankstellenliste.execute(`eintrag = tabelle.insertRow(tabelle.rows.length);`);
	tankstellenliste.execute(`zelle1 =  eintrag.insertCell(0);`);
	tankstellenliste.execute(`zelle2 = eintrag.insertCell(1);`);
	tankstellenliste.execute(`zelle3 = eintrag.insertCell(2);`);
	tankstellenliste.execute(`zelle4 = eintrag.insertCell(3);`);
	tankstellenliste.execute(`zelle5 = eintrag.insertCell(4);`);
	tankstellenliste.execute(`zelle6 = eintrag.insertCell(5);`);
	

	tankstellenliste.execute(`inhalt1 = document.createTextNode('${Id}');`);
	tankstellenliste.execute(`inhalt2 = document.createTextNode('${Name}');`);
	tankstellenliste.execute(`inhalt3 = document.createTextNode('${Kraftstoff} Liter');`);
	tankstellenliste.execute(`inhalt4 = document.createTextNode('${Entfernung} KM');`);
	tankstellenliste.execute(`inhalt5 = document.createTextNode('${Verdienst}');`);
	tankstellenliste.execute(`inhalt6 = document.createElement('input');`);
	
	tankstellenliste.execute(`inhalt6.type = "button";`);
	tankstellenliste.execute(`inhalt6.className = "btn btn-success btn-success";`);
	tankstellenliste.execute(`inhalt6.value = "Annehmen";`);
	tankstellenliste.execute(`inhalt6.dataset.target = ${Id};`);
	tankstellenliste.execute(`inhalt6.id = ${Id};`);
	
	tankstellenliste.execute(`inhalt6.onclick = tankeAngenommen;`);
	
	tankstellenliste.execute(`zelle1.appendChild(inhalt1);`);
	tankstellenliste.execute(`zelle2.appendChild(inhalt2);`);
	tankstellenliste.execute(`zelle3.appendChild(inhalt3);`);
	tankstellenliste.execute(`zelle4.appendChild(inhalt4);`);
	tankstellenliste.execute(`zelle5.appendChild(inhalt5);`);
	tankstellenliste.execute(`zelle6.appendChild(inhalt6);`);
	
}

mp.events.add('Berufskraftfahrer_Tankstellenbrowserschliessen', () => {
	tankstellenliste.destroy();
	mp.gui.cursor.show(false, false);
});

mp.events.add('TankeAngenommen', (Id) => {
	mp.events.callRemote('JobBerufskraftfahrerTanke', Id);
});






