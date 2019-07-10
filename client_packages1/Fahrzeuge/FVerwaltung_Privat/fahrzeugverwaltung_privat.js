$('#abbrechen').click(() => {
	mp.trigger('FVerwaltung_Privat_Abbrechen');
});

function abgeschlossen() 
{
	var abgeschlossen = document.getElementById("abgeschlossen");
	
	if (abgeschlossen.checked == true)
	{
		mp.trigger('abschließen', 1);
	}
	else
	{
		mp.trigger('abschließen', 0);
	}
}






