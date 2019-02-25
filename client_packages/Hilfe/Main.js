var HilfeBrowser;
mp.events.add('hilfebrowseroeffnen', (Admin) => {
    HilfeBrowser = mp.browsers.new('package://Hilfe/hilfe.html');
	mp.gui.cursor.show(true, true);


    //Admin Commands
    var AdminLevel = '"<b><u>Admin</u></b><br>/admingeben - Ab Level 4<br>/ferstellen - Ab Level 4<br>/waffe - Ab Level 4<br>';
    AdminLevel += '/parken - Ab Level 3<br>/frespawn - Ab Level 3<br>/freparieren - Ab Level 3<br>/fzuweisen & /fzuweisenprivat - Ab Level 3<br>';
    AdminLevel += '/fporten - Ab Level 3<br>/teleports - Ab Level 1<br>/teleporten - Ab Level 1<br>"';
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
