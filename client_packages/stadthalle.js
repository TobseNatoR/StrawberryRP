let anzeigen = 0;

let menu = new MainMenu("Stadthalle");
let Stadthalle = new TextMenuItem("Personalausweis beantragen (100$)", "beantragen");

Stadthalle.addOnClickEvent({
    trigger: data => {
        mp.gui.chat.push("Click "+ data);
    }
});
menu.add(Stadthalle);

mp.events.add("Stadthalle", () => {
	anzeigen = 1;
});

mp.events.add("StadthalleWeg", () => {
	anzeigen = 0;
});

mp.events.add("render", () => {
	if(anzeigen == 1)
	{
		menu.render(0.85, 0.1);
	}
});