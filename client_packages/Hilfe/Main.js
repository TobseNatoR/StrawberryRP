var HilfeBrowser;
mp.events.add('hilfebrowseroeffnen', (Admin) => {
    HilfeBrowser = mp.browsers.new('package://Hilfe/Hilfe.html');
	mp.gui.cursor.show(true, true);
	
	if(Admin > 0)
	{
		HilfeBrowser.execute(`document.getElementById('admin').innerHTML="<b><u>Admin</u></b><br>/fahrzeugerstellen - Ab Level 5"`);
	}
});

mp.events.add('hilfebrowserschliessen', () => {
    HilfeBrowser.destroy();
	mp.gui.cursor.show(false, false);
});
