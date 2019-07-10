var fahrzeugverwaltung;
let player = mp.players.local;

var tabelle;
var eintrag;
var zelle1;
var zelle2;
var zelle3;
var zelle4;
var zelle5;

var inhalt1;
var inhalt2;
var inhalt3;
var inhalt4;
var inhalt5;

mp.events.add('Fahrzeugverwaltung_Privat_Liste', () => {
    fahrzeugverwaltung = mp.browsers.new('package://Fahrzeuge/FVerwaltung_Privat_Liste/fahrzeugverwaltung_privat_liste.html');
	mp.gui.cursor.show(true, true);
});

mp.events.add('Fahrzeugverwaltung_Privat_Liste_Add', (Name, Tank, KM, HU, Abgeschlossen) => {
	Eintragen(Name, Tank, KM, HU, Abgeschlossen);
});

function Eintragen(Name, Tank, KM, HU, Abgeschlossen) 
{
	fahrzeugverwaltung.execute(`tabelle = document.getElementById('ftabelle').getElementsByTagName('tbody')[0];`);
	fahrzeugverwaltung.execute(`eintrag = tabelle.insertRow(tabelle.rows.length);`);
	fahrzeugverwaltung.execute(`zelle1 =  eintrag.insertCell(0);`);
	fahrzeugverwaltung.execute(`zelle2 = eintrag.insertCell(1);`);
	fahrzeugverwaltung.execute(`zelle3 = eintrag.insertCell(2);`);
	fahrzeugverwaltung.execute(`zelle4 = eintrag.insertCell(3);`);
	fahrzeugverwaltung.execute(`zelle5 = eintrag.insertCell(4);`);
	

	fahrzeugverwaltung.execute(`inhalt1 = document.createTextNode('${Name}');`);
	fahrzeugverwaltung.execute(`inhalt2 = document.createTextNode('${Tank}');`);
	fahrzeugverwaltung.execute(`inhalt3 = document.createTextNode('${KM}');`);
	fahrzeugverwaltung.execute(`inhalt4 = document.createTextNode('${HU}');`);
	fahrzeugverwaltung.execute(`inhalt5 = document.createTextNode('${Abgeschlossen}');`);
	
	fahrzeugverwaltung.execute(`zelle1.appendChild(inhalt1);`);
	fahrzeugverwaltung.execute(`zelle2.appendChild(inhalt2);`);
	fahrzeugverwaltung.execute(`zelle3.appendChild(inhalt3);`);
	fahrzeugverwaltung.execute(`zelle4.appendChild(inhalt4);`);
	fahrzeugverwaltung.execute(`zelle5.appendChild(inhalt5);`);
}

mp.events.add('FVerwaltung_Privat_Liste_Abbrechen', () => {
    mp.events.callRemote('Fahrzeugverwaltung_Privat_Abbrechen');
	fahrzeugverwaltung.destroy();
	mp.gui.cursor.show(false, false);
});






