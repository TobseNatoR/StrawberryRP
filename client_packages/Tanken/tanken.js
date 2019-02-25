document.getElementById("kraftstoff2").checked = true;

$('#abbrechen').click(() => {
	mp.trigger('tankenabbrechen');
});

$('#tanken').click(() => {
	var Diesel = 0;
	var E10 = 0;
	var Super = 0;
	
	var kraftstoff1  = document.getElementById("kraftstoff1");
	var kraftstoff2  = document.getElementById("kraftstoff2");
	var kraftstoff3  = document.getElementById("kraftstoff3");
	
	if (kraftstoff1.checked == true)
	{
		E10 = 0;
		Super = 0;
		Diesel = 1;
	}
	if (kraftstoff2.checked == true)
	{
		Diesel = 0;
		Super = 0;
		E10 = 1;
	}
	if (kraftstoff3.checked == true)
	{
		Diesel = 0;
		E10 = 0;
		Super = 1;
	}
	
	mp.trigger('auftanken', $('#liter').val(), Diesel, E10, Super);
});

function diesel() 
{
	// Get the checkbox
	var checkbox  = document.getElementById("kraftstoff1");
	document.getElementById("kraftstoff2").checked = false;
	document.getElementById("kraftstoff3").checked = false;
}

function e10() 
{
	// Get the checkbox
	var checkbox  = document.getElementById("kraftstoff2");
	document.getElementById("kraftstoff1").checked = false;
	document.getElementById("kraftstoff3").checked = false;
}

function ksuper() 
{
	// Get the checkbox
	var checkbox  = document.getElementById("kraftstoff3");
	document.getElementById("kraftstoff1").checked = false;
	document.getElementById("kraftstoff2").checked = false;
}





