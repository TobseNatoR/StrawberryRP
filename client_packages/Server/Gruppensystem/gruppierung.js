$('form').keypress(function(e) { 
    return e.keyCode != 13;
});

$('#erstellen').click(() => {
    $('.alert').remove(); 
    mp.trigger('gruppierungerstellenzumserver', $('#gruppenname').val(), $('#gruppentag').val(), $('#gruppenfarbe').val());
});

$('#erstellenabbrechen').click(() => {
    $('.alert').remove(); 
    mp.trigger('gruppenerstellenbrowserschliessen');
});

$('#meinegruppierungschliessen').click(() => {
    mp.trigger('meinegruppierungbrowserschliessen');
});

$('#rangspeichern').click(() => {
    $('.alert').remove(); 
    mp.trigger('rangezumserver', $('#range5name').val(), $('#range4name').val(), $('#range3name').val(), $('#range2name').val(), $('#range1name').val());
});

$('#inviten').click(() => {
    $('.alert').remove(); 
    mp.trigger('invitenzumserver', $('#spielername').val());
});

$('#annehmen').click(() => {
    $('.alert').remove(); 
    mp.trigger('einladungannehmenzumserver');
});

$('#ablehnen').click(() => {
    $('.alert').remove(); 
    mp.trigger('einladungablehnenzumserver');
});



function RangDaten(Rang5, Rang4, Rang3, Rang2, Rang1) 
{
	document.getElementById("range5name").value = Rang5;
	document.getElementById("range4name").value = Rang4;
	document.getElementById("range3name").value = Rang3;
	document.getElementById("range2name").value = Rang2;
	document.getElementById("range1name").value = Rang1;
}

function speichern() {
	var e = window.event,
	btn = e.target || e.srcElement;
	let input = "Rang" + btn.id;
	var inputrang = document.getElementById(input).value;
	mp.trigger('gruppenspielerspeichernzumserver', btn.id, inputrang);
}

function uninvite() {
	var e = window.event,
	btn = e.target || e.srcElement;
	mp.trigger('uninvitezumserver', btn.id);
}

function GruppenDaten(Name, Leader, Tag, Farbe, Geld, IstLeader) 
{
	if(Farbe == "rot" || Farbe == "Rot") { document.getElementById('Gruppenname').innerHTML = "<span style='color: red;'>" + Name; }
	else if(Farbe == "blau" || Farbe == "Blau") { document.getElementById('Gruppenname').innerHTML = "<span style='color: blue;'>" + Name; }
	else if(Farbe == "grün" || Farbe == "Grün") { document.getElementById('Gruppenname').innerHTML = "<span style='color: green;'>" + Name; }
	else if(Farbe == "gelb" || Farbe == "Gelb") { document.getElementById('Gruppenname').innerHTML = "<span style='color: yellow;'>" + Name; }
	else if(Farbe == "lila" || Farbe == "Lila") { document.getElementById('Gruppenname').innerHTML = "<span style='color: purple;'>" + Name; }
	document.getElementById('Gruppenleader').innerHTML = Leader;
	if(Farbe == "rot" || Farbe == "Rot") { document.getElementById('Gruppentag').innerHTML = "<span style='color: red;'>" + Tag; }
	else if(Farbe == "blau" || Farbe == "Blau") { document.getElementById('Gruppentag').innerHTML = "<span style='color: blue;'>" + Tag; }
	else if(Farbe == "grün" || Farbe == "Grün") { document.getElementById('Gruppentag').innerHTML = "<span style='color: green;'>" + Tag; }
	else if(Farbe == "gelb" || Farbe == "Gelb") { document.getElementById('Gruppentag').innerHTML = "<span style='color: yellow;'>" + Tag; }
	else if(Farbe == "lila" || Farbe == "Lila") { document.getElementById('Gruppentag').innerHTML = "<span style='color: purple;'>" + Tag; }
	document.getElementById('Gruppengeld').innerHTML = Geld;
	
	if(IstLeader == 0)
	{
		let einladen = document.getElementById("einladen");
		einladen.className += " disabled";

		let verwalten = document.getElementById("verwalten");
		verwalten.className += " disabled";

		let einstellungen = document.getElementById("einstellungen");
		einstellungen.className += " disabled";
	}
}


