var saveliste;
let player = mp.players.local;

var tabelle;
var eintrag;
var zelle1;
var zelle2;
var zelle3;
var zelle4;
var zelle5;
var zelle6;
var zelle7;
var zelle8;

var inhalt1;
var inhalt2;
var inhalt3;
var inhalt4;
var inhalt5;
var inhalt6;
var inhalt7;
var inhalt8;

mp.events.add('savelisteoeffnen', (Typ) => {
    saveliste = mp.browsers.new('package://Server/Saveliste/index.html');
	mp.gui.cursor.show(true, true);
});

mp.events.add('Saveliste_Eintragen', (Id, Beschreibung, Von, posX, posY, posZ) => {
	Eintragen(Id, Beschreibung, Von, posX, posY, posZ);
});

function Eintragen(Id, Beschreibung, Von, posX, posY, posZ) 
{
	saveliste.execute(`tabelle = document.getElementById('savetabelle').getElementsByTagName('tbody')[0];`);
	saveliste.execute(`eintrag = tabelle.insertRow(tabelle.rows.length);`);
	saveliste.execute(`zelle1 =  eintrag.insertCell(0);`);
	saveliste.execute(`zelle2 = eintrag.insertCell(1);`);
	saveliste.execute(`zelle3 = eintrag.insertCell(2);`);
	saveliste.execute(`zelle4 = eintrag.insertCell(3);`);
	saveliste.execute(`zelle5 = eintrag.insertCell(4);`);
	saveliste.execute(`zelle6 = eintrag.insertCell(5);`);
	saveliste.execute(`zelle7 = eintrag.insertCell(6);`);
	saveliste.execute(`zelle8 = eintrag.insertCell(7);`);
	

	saveliste.execute(`inhalt1 = document.createTextNode('${Id}');`);
	saveliste.execute(`inhalt2 = document.createTextNode('${Beschreibung}');`);
	saveliste.execute(`inhalt3 = document.createTextNode('${Von}');`);
	saveliste.execute(`inhalt4 = document.createTextNode('${posX}');`);
	saveliste.execute(`inhalt5 = document.createTextNode('${posY}');`);
	saveliste.execute(`inhalt6 = document.createTextNode('${posZ}');`);
	
	saveliste.execute(`inhalt7 = document.createElement('input');`);
	saveliste.execute(`inhalt7.type = "button";`);
	saveliste.execute(`inhalt7.className = "btn btn-success btn-success";`);
	saveliste.execute(`inhalt7.value = "TP";`);
	saveliste.execute(`inhalt7.dataset.target = ${Id};`);
	saveliste.execute(`inhalt7.id = ${Id};`);
	saveliste.execute(`inhalt7.onclick = saveTeleport;`);
	
	saveliste.execute(`inhalt8 = document.createElement('input');`);
	saveliste.execute(`inhalt8.type = "button";`);
	saveliste.execute(`inhalt8.className = "btn btn-success btn-danger";`);
	saveliste.execute(`inhalt8.value = "DEL";`);
	saveliste.execute(`inhalt8.dataset.target = ${Id};`);
	saveliste.execute(`inhalt8.id = ${Id};`);
	saveliste.execute(`inhalt8.onclick = deleteSave;`);
	
	saveliste.execute(`zelle1.appendChild(inhalt1);`);
	saveliste.execute(`zelle2.appendChild(inhalt2);`);
	saveliste.execute(`zelle3.appendChild(inhalt3);`);
	saveliste.execute(`zelle4.appendChild(inhalt4);`);
	saveliste.execute(`zelle5.appendChild(inhalt5);`);
	saveliste.execute(`zelle6.appendChild(inhalt6);`);
	saveliste.execute(`zelle7.appendChild(inhalt7);`);
	saveliste.execute(`zelle8.appendChild(inhalt8);`);
	
}

mp.events.add('savelisteschliessen', () => {
	saveliste.destroy();
	mp.gui.cursor.show(false, false);
});

mp.events.add('SaveTeleport', (Id) => {
	mp.events.callRemote('SaveListeTeleportieren', Id);
});

mp.events.add('SaveLöschen', (Id) => {
	mp.events.callRemote('SaveListeLöschen', Id);
});






