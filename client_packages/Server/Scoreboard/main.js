var scoreboard;

var tabelle;
var eintrag;
var zelle1;
var zelle2;
var zelle3;
var zelle4;

var inhalt1;
var inhalt2;
var inhalt3;
var inhalt4;
var inhalt41;

let Spieler = 0;

//Scrollen
let scrollHeight = 0;
let scrollPosition = 0;
var objDiv;


mp.events.add('Scoreboardoeffnen', () => {
    scoreboard = mp.browsers.new('package://Server/Scoreboard/scoreboard.html');
});

mp.events.add('Scoreboard_Eintragen', (Name, Titel, Spielzeit, Ping) => {
	Eintragen(Name, Titel, Spielzeit, Ping);
});

mp.events.add('ScoreboardScrollDown', () => {
	scoreboard.execute(`objDiv = document.getElementById('scoreBoard');`);
	scoreboard.execute(`scrollHeight = objDiv.scrollWidth;`);
	mp.gui.chat.push("ScrollPosition: " + scrollPosition);
	if(scrollPosition < scrollHeight)
	{
		scrollPosition += 10;
		scoreboard.execute(`objDiv.scrollTop = ${scrollPosition};`);
	}
});

mp.events.add('ScoreboardScrollUp', () => {
	scoreboard.execute(`objDiv = document.getElementById('scoreBoard');`);
	mp.gui.chat.push("ScrollPosition: " + scrollPosition);
	if(scrollPosition > 0)
	{
		scrollPosition -= 10;
		scoreboard.execute(`objDiv.scrollTop = ${scrollPosition};`);
	}
});


mp.events.add('ScoreboardScrollHeight', (Scroll) => {
	scrollHeight = Scroll;
});

mp.events.add('ScoreboardSpielerzahl', () => {
	scoreboard.execute(`document.getElementById('Spieler').innerHTML = 'Spieler online: ${Spieler} / 500';`);﻿
});

function Eintragen(Name, Titel, Spielzeit, Ping) 
{
	scoreboard.execute(`tabelle = document.getElementById('scoreboardtabelle').getElementsByTagName('tbody')[0];`);
	scoreboard.execute(`eintrag = tabelle.insertRow(tabelle.rows.length);`);
	scoreboard.execute(`zelle1 =  eintrag.insertCell(0);`);
	scoreboard.execute(`zelle2 = eintrag.insertCell(1);`);
	scoreboard.execute(`zelle3 = eintrag.insertCell(2);`);
	scoreboard.execute(`zelle4 = eintrag.insertCell(3);`);

	scoreboard.execute(`inhalt1 = document.createTextNode('${Name}');`);
	scoreboard.execute(`inhalt2 = document.createTextNode('${Titel}');`);
	scoreboard.execute(`inhalt3 = document.createTextNode('${Spielzeit}');`);
	scoreboard.execute(`inhalt4 = document.createElement('B');`);
	
	if(Ping < 40)
	{
		scoreboard.execute(`inhalt4.type = "text";`);
		scoreboard.execute(`inhalt4.className = "green";`);
		scoreboard.execute(`inhalt4a = document.createTextNode('${Ping} ms');`);
		scoreboard.execute(`inhalt4.appendChild(inhalt4a);`);
	}
	else if(Ping >= 40 && Ping < 70)
	{
		scoreboard.execute(`inhalt4.type = "text";`);
		scoreboard.execute(`inhalt4.className = "orange";`);
		scoreboard.execute(`inhalt4a = document.createTextNode('${Ping} ms');`);
		scoreboard.execute(`inhalt4.appendChild(inhalt4a);`);
	}
	else if(Ping >= 70)
	{
		scoreboard.execute(`inhalt4.type = "text";`);
		scoreboard.execute(`inhalt4.className = "red";`);
		scoreboard.execute(`inhalt4a = document.createTextNode('${Ping} ms');`);
		scoreboard.execute(`inhalt4.appendChild(inhalt4a);`);
	}
	
	scoreboard.execute(`zelle1.appendChild(inhalt1);`);
	scoreboard.execute(`zelle2.appendChild(inhalt2);`);
	scoreboard.execute(`zelle3.appendChild(inhalt3);`);
	scoreboard.execute(`zelle4.appendChild(inhalt4);`);
	
	Spieler += 1;
}

mp.events.add('Scoreboardschliessen', () => {
	scoreboard.destroy();
	Spieler = 0;
});






