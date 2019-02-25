//Um zu checken ob es angezeigt wird oder nicht
let anzeigen = 0;
let fahrzeuganzahl = 0;
let Fahrzeug;
let TankGröße;

let menu = new MainMenu("Fahrzeuge");

var Fahrzeug = [[],[]];

mp.events.add('FahrzeugHinzufügen', (Durchgang, Name, Tank) => {
	Fahrzeug[Durchgang][0] = Name;
	Fahrzeug[Durchgang][1] = TankVolumen;
	
	Fahrzeuge = new TextMenuItem("Fahrzeugname: " + Fahrzeug[Durchgang][0]);
	TankGröße = new TextMenuItem("Tank Größe: " + Fahrzeug[Durchgang][1]);
	
	menu.add(Fahrzeuge);
	menu.add(TankGröße);
	
	menu.add(new CheckboxMenuItem("Abgeschlossen", false));
});

mp.events.add("FahrzeugAnzahl", (Anzahl) => {
	fahrzeuganzahl = Anzahl;
});

mp.events.add("FahrzeugPanelPrivat", () => {
	anzeigen = 1;
});

mp.events.add("FahrzeugPanelPrivatWeg", () => {
	anzeigen = 0;
	fahrzeuganzahl = 0;
});

mp.events.add("render", () => {
	if(anzeigen == 1)
	{
		menu.render(0.85, 0.1);
	}
});