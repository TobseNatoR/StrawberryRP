let Player = mp.players.local;

var gruppenErstellenBrowser;
var meineGruppierungBrowser;
var inviteBrowser;

var tabelle;
var eintrag;
var zelle1;
var zelle2;
var zelle3;

var inhalt1;
var inhalt2;
var inhalt3;

var tabelle1;
var eintrag1;
var zelle11;
var zelle22;
var zelle33;
var zelle44;

var inhalt11;
var inhalt22;
var inhalt33;
var inhalt44;

mp.events.add('gruppenerstellenbrowseroeffnen', () => {
    gruppenErstellenBrowser = mp.browsers.new('package://Server/Gruppensystem/gruppierungerstellen.html');
	mp.gui.cursor.show(true, true);
});

mp.events.add('meinegruppierungbrowseroeffnen', () => {
    meineGruppierungBrowser = mp.browsers.new('package://Server/Gruppensystem/meinegruppierung.html');
	mp.gui.cursor.show(true, true);
});

mp.events.add('invitebrowseroeffnen', (Gruppenname) => {
    inviteBrowser = mp.browsers.new('package://Server/Gruppensystem/invite.html');
	mp.gui.cursor.show(true, true);
	
	inviteBrowser.execute(`document.getElementById('text').innerHTML = 'Möchtest du der Gruppe <b>${Gruppenname}</b> beitreten?';`);
});

mp.events.add('GruppenDaten', (Name, Leader, Tag, Farbe, Geld, IstLeader) => {
	meineGruppierungBrowser.execute(`GruppenDaten('${Name}', '${Leader}', '${Tag}', '${Farbe}', '${Geld}', '${IstLeader}');`);
});

mp.events.add('RangDaten', (Rang5, Rang4, Rang3, Rang2, Rang1) => {
	meineGruppierungBrowser.execute(`RangDaten('${Rang5}', '${Rang4}', '${Rang3}', '${Rang2}', '${Rang1}');`);
});

mp.events.add('gruppenerstellenbrowserschliessen', () => {
    gruppenErstellenBrowser.destroy();
	mp.gui.cursor.show(false, false);
	mp.gui.chat.show(true);
});

mp.events.add('meinegruppierungbrowserschliessen', () => {
    meineGruppierungBrowser.destroy();
	mp.gui.cursor.show(false, false);
	mp.gui.chat.show(true);
});

mp.events.add('invitebrowserschliessen', () => {
    inviteBrowser.destroy();
	mp.gui.cursor.show(false, false);
	mp.gui.chat.show(true);
});

mp.events.add('gruppierungerstellenzumserver', (Gruppenname, Gruppentag, Gruppenfarbe) => {
    mp.events.callRemote('GruppierungErstellen', Gruppenname, Gruppentag, Gruppenfarbe);
});

mp.events.add('rangezumserver', (Rang5, Rang4, Rang3, Rang2, Rang1) => {
    mp.events.callRemote('GruppenRaengeSpeichern', Rang5, Rang4, Rang3, Rang2, Rang1);
});

mp.events.add('gruppenspielerspeichernzumserver', (Id, Rang) => {
    mp.events.callRemote('GruppenSpielerSpeichern', Id, Rang);
});

mp.events.add('invitenzumserver', (Name) => {
    mp.events.callRemote('GruppeSpielerInviten', Name);
});

mp.events.add('uninvitezumserver', (Name) => {
    mp.events.callRemote('GruppeSpielerUninviten', Name);
});

mp.events.add('einladungannehmenzumserver', () => {
    mp.events.callRemote('GruppeEinladungAnnehmen');
});

mp.events.add('einladungablehnenzumserver', () => {
    mp.events.callRemote('GruppeEinladungAblehnen');
});

mp.events.add('Mitglieder_Eintragen', (Id, Name, Lastonline, RangName, Rang, Leader) => {
	Eintragen(Id, Name, Lastonline, RangName);
	
	if(Leader == 1)
	{
		EintragenVerwaltung(Id, Name, Lastonline, Rang);
	}
});

function Eintragen(Id, Name, Lastonline, Rang, Leader) 
{
	meineGruppierungBrowser.execute(`tabelle = document.getElementById('gruppentabelle').getElementsByTagName('tbody')[0];`);
	meineGruppierungBrowser.execute(`eintrag = tabelle.insertRow(tabelle.rows.length);`);
	meineGruppierungBrowser.execute(`zelle1 =  eintrag.insertCell(0);`);
	meineGruppierungBrowser.execute(`zelle2 = eintrag.insertCell(1);`);
	meineGruppierungBrowser.execute(`zelle3 = eintrag.insertCell(2);`);

	meineGruppierungBrowser.execute(`inhalt1 = document.createTextNode('${Name}');`);
	meineGruppierungBrowser.execute(`inhalt2 = document.createTextNode('${Lastonline}');`);
	meineGruppierungBrowser.execute(`inhalt3 = document.createTextNode('${Rang}');`);
	
	meineGruppierungBrowser.execute(`zelle1.appendChild(inhalt1);`);
	meineGruppierungBrowser.execute(`zelle2.appendChild(inhalt2);`);
	meineGruppierungBrowser.execute(`zelle3.appendChild(inhalt3);`);
	
}

function EintragenVerwaltung(Id, Name, Lastonline, Rang) 
{
	meineGruppierungBrowser.execute(`tabelle1 = document.getElementById('verwaltentabelle').getElementsByTagName('tbody')[0];`);
	meineGruppierungBrowser.execute(`eintrag1 = tabelle1.insertRow(tabelle1.rows.length);`);
	meineGruppierungBrowser.execute(`zelle11 =  eintrag1.insertCell(0);`);
	meineGruppierungBrowser.execute(`zelle22 = eintrag1.insertCell(1);`);
	meineGruppierungBrowser.execute(`zelle33 = eintrag1.insertCell(2);`);
	meineGruppierungBrowser.execute(`zelle44 = eintrag1.insertCell(3);`);

	meineGruppierungBrowser.execute(`inhalt11 = document.createTextNode('${Name}');`);
	
	meineGruppierungBrowser.execute(`inhalt22 = document.createElement('input');`);
	meineGruppierungBrowser.execute(`inhalt22.type = "number";`);
	meineGruppierungBrowser.execute(`inhalt22.className = "form-control";`);
	meineGruppierungBrowser.execute(`inhalt22.value = ${Rang};`);
	meineGruppierungBrowser.execute(`inhalt22.dataset.target = 'Rang${Id}';`);
	meineGruppierungBrowser.execute(`inhalt22.id = 'Rang${Id}';`);
	
	meineGruppierungBrowser.execute(`inhalt33 = document.createElement('input');`);
	meineGruppierungBrowser.execute(`inhalt33.type = "button";`);
	meineGruppierungBrowser.execute(`inhalt33.className = "btn btn-danger";`);
	meineGruppierungBrowser.execute(`inhalt33.value = "✓";`);
	meineGruppierungBrowser.execute(`inhalt33.dataset.target = ${Id};`);
	meineGruppierungBrowser.execute(`inhalt33.id = ${Id};`);
	meineGruppierungBrowser.execute(`inhalt33.onclick = uninvite;`);
	
	meineGruppierungBrowser.execute(`inhalt44 = document.createElement('input');`);
	meineGruppierungBrowser.execute(`inhalt44.type = "button";`);
	meineGruppierungBrowser.execute(`inhalt44.className = "btn btn-warning";`);
	meineGruppierungBrowser.execute(`inhalt44.value = "✓";`);
	meineGruppierungBrowser.execute(`inhalt44.dataset.target = ${Id};`);
	meineGruppierungBrowser.execute(`inhalt44.id = ${Id};`);
	meineGruppierungBrowser.execute(`inhalt44.onclick = speichern;`);
	
	meineGruppierungBrowser.execute(`zelle11.appendChild(inhalt11);`);
	meineGruppierungBrowser.execute(`zelle22.appendChild(inhalt22);`);
	meineGruppierungBrowser.execute(`zelle33.appendChild(inhalt33);`);
	meineGruppierungBrowser.execute(`zelle44.appendChild(inhalt44);`);
	
}
