let speedo = mp.browsers.new("package://Server/Tacho/CEF/tacho.html");
let showed = false;
let player = mp.players.local;
let km;
let gas;
let volumen;

mp.events.add('TachoUpdaten', (Kilometerstand, Sprit, Tank) => {
    km = Kilometerstand;
	gas = Sprit;
	volumen = Tank;
});

mp.events.add('render', () =>
{
	if (player.vehicle && player.vehicle.getPedInSeat(-1) === player.handle) // Check if player is in vehicle and is driver
	{
		if (mp.players.local.vehicle) 
		{
     ﻿   	mp.game.audio.setRadioToStationName("OFF");
			mp.game.audio.setUserRadioControlEnabled(false);
		}
		if(showed === false) // Check if speedo is already showed
		{
			speedo.execute("showSpeedo();");
			showed = true;
		}
		/*Get vehicle infos*/
		let vel = player.vehicle.getSpeed() * 3.6;  	
		let rpm = player.vehicle.rpm * 1000; 			
		
		speedo.execute(`update(${vel}, ${rpm}, ${gas}, ${km}, ${volumen});`); // Send data do CEF
	}
	else
	{
		if(showed)
		{
			speedo.execute("hideSpeedo();");
			showed = false;
		}
	}
});