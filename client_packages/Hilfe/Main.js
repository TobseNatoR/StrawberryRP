var HilfeBrowser;
mp.events.add('hilfebrowseroeffnen', (Admin) => {
    HilfeBrowser = mp.browsers.new('package://Hilfe/Hilfe.html');
	mp.gui.cursor.show(true, true);


    //Admin Commands
    var AdminLevel = '"<b><u>Admin</u></b><br>/admingeben - Ab Level 5<br>/fahrzeugderstellen - Ab Level 5<br>/waffe - Ab Level 5<br>/parken - Ab Level 3<br>/teleporten - Ab Level 1"';
    var AdminDiv = `document.getElementById('admin').innerHTML=` + AdminLevel;
    

	if(Admin > 0)
    {
        HilfeBrowser.execute(AdminDiv);
	}
});

mp.events.add('hilfebrowserschliessen', () => {
    HilfeBrowser.destroy();
	mp.gui.cursor.show(false, false);
});
