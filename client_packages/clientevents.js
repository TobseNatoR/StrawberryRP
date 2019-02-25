mp.game.vehicle.defaultEngineBehaviour = false;
let Player = mp.players.local;

mp.events.add('FahrzeugReparieren', () => {
	Player.vehicle.setDeformationFixed();
});

mp.events.add('RollerSpeed', () => {
	Player.vehicle.setEnginePowerMultiplier(25);
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

mp.events.add('Enter', (Eingeloggt) => {
    if(Eingeloggt == 0)
	{
		return;
	}
});

//Client event 
mp.events.add('StartFire', (posX, posY, posZ, maxChilderen, gasPowerd) => {
    // The fireId is a int
    let fireId = mp.game.fire.startScriptFire(posX, posY, posZ, maxChilderen, gasPowerd);
	mp.game.fire.addExplosion(posX, posY, posZ, 34, 60, true, false, 10);
});

mp.events.add('WantedLevel', (Anzahl) => {
    mp.game.gameplay.setFakeWantedLevel(Anzahl);
});

