let sceneryCamera = mp.cameras.new('default', new mp.Vector3(78.44406, -1353.406, 29.41779), new mp.Vector3(0,0,0), 40);

mp.events.add('kameraon', () => {
    sceneryCamera.pointAtCoord(94.67142, -1364.345, 29.34226);
	sceneryCamera.setActive(true);
	mp.game.cam.renderScriptCams(true, false, 0, true, false);
});

mp.events.add('kameraoff', () => {
	sceneryCamera.setActive(false);
	mp.game.cam.renderScriptCams(false, false, 0, false, false);
});
