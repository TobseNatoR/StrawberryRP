var soundBrowser;
mp.events.add('soundbrowseroeffnen', () => {
    soundBrowser = mp.browsers.new('package://Server/Sounds/sounds.html');
	mp.gui.cursor.show(false, false);
});

mp.events.add('soundbrowserschliessen', () => {
    soundBrowser.destroy();
	mp.gui.cursor.show(false, false);
});

mp.events.add('SoundStarten', (Typ) => {
    soundBrowser.execute(`startAudio('${Typ}');`);
});

