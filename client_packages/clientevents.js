mp.game.vehicle.defaultEngineBehaviour = true;
let Player = mp.players.local;
let Fahrzeug = mp.players.local.vehicle;
mp.nametags.enabled = true;

mp.events.add('FahrzeugVerlassen', () => {
	let localVeh = mp.players.local.vehicle;
	if (localVeh) 
	{
		mp.players.local.taskLeaveVehicle(localVeh.handle, 16);
	}
});

mp.events.add('NichtFahrbar', () => {
	let localVeh = mp.players.local.vehicle;
	if (localVeh) 
	{
		localVeh.setUndriveable(true);
	}
});

mp.events.add('Fahrbar', () => {
	let localVeh = mp.players.local.vehicle;
	if (localVeh) 
	{
		localVeh.setUndriveable(false);
	}
});

mp.events.add('DiscordStatusSetzen', (serverName, status) => {
  mp.discord.update(serverName, status)
});

mp.events.add('FahrzeugReparieren', () => {
	Player.vehicle.setDeformationFixed();
});

mp.events.add('RollerSpeed', () => {
	Player.vehicle.setEnginePowerMultiplier(25);
});

mp.events.add('LKWSpeed', () => {
	Player.vehicle.setEnginePowerMultiplier(-30);
});

mp.events.add('Chathiden', () => {
	mp.gui.chat.show(false);
});

mp.events.add('Freeze', () => {
	Player.freezePosition(true);
});

mp.events.add('Unfreeze', () => {
	Player.freezePosition(false);
});

mp.events.add('Chatzeigen', () => {
	mp.gui.chat.show(true);
});

//Client event 
mp.events.add('StartFire', (posX, posY, posZ, maxChilderen, gasPowerd) => {
    // The fireId is a int
    let fireId = mp.game.fire.startScriptFire(posX, posY, posZ, maxChilderen, gasPowerd);
	mp.game.fire.addExplosion(posX, posY, posZ, 34, 60, true, false, 10);
});

//Navigation
mp.events.add('Navigation', (posX, posY) => {
    mp.game.ui.setNewWaypoint(posX, posY);
});


mp.events.add('WantedLevel', (Anzahl) => {
    mp.game.gameplay.setFakeWantedLevel(Anzahl);
});


