mp.events.add('InteriorLaden', (Interior) => {
	mp.game.streaming.requestIpl(Interior);
});