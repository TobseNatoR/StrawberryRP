let scrollWidth;
var pferdRot = null;
var pferdGruen = null;
var pferdGelb = null;
var pferdBlau = null;

let pferdRotPosition = 0;
let pferdGruenPosition = 0;
let pferdGelbPosition = 0;
let pferdBlauPosition = 0;

let ManagerInterval;
let pferdRotInterval;
let pferdGruenInterval; 
let pferdGelbInterval; 
let pferdBlauInterval;  

setInterval(pferdeRennenManager, 5000);

pferdRotInterval = setInterval(pferdRotRechts, randomIntFromInterval(9, 20));
pferdGruenInterval = setInterval(pferdGruenRechts, randomIntFromInterval(9, 20)); 
pferdGelbInterval = setInterval(pferdGelbRechts, randomIntFromInterval(9, 20)); 
pferdBlauInterval = setInterval(pferdBlauRechts, randomIntFromInterval(9, 20));  
            
var objekt = document.getElementById("rennbahn");
scrollWidth = objekt.scrollWidth;

pferdRot = document.getElementById('pferdrot');
pferdRot.style.position= 'relative'; 
pferdRot.style.left = '0px'; 

pferdGruen = document.getElementById('pferdgruen');
pferdGruen.style.position= 'relative'; 
pferdGruen.style.left = '0px'; 

pferdGelb = document.getElementById('pferdgelb');
pferdGelb.style.position= 'relative'; 
pferdGelb.style.left = '0px'; 

pferdBlau = document.getElementById('pferdblau');
pferdBlau.style.position= 'relative'; 
pferdBlau.style.left = '0px'; 

function pferdeRennenManager() {
	clearInterval(pferdRotInterval);
	clearInterval(pferdGruenInterval);
	clearInterval(pferdGelbInterval);
	clearInterval(pferdBlauInterval);
	
	pferdRotInterval = setInterval(pferdRotRechts, randomIntFromInterval(9, 20));
	pferdGruenInterval = setInterval(pferdGruenRechts, randomIntFromInterval(9, 20)); 
	pferdGelbInterval = setInterval(pferdGelbRechts, randomIntFromInterval(9, 20)); 
	pferdBlauInterval = setInterval(pferdBlauRechts, randomIntFromInterval(9, 20));  
}

function randomIntFromInterval(min,max)
{
    return Math.floor(Math.random()*(max-min+1)+min);
}

function pferdRotRechts() {
	
	if(pferdRotPosition < scrollWidth - 50)
	{
		pferdRot = document.getElementById('pferdrot');
		pferdRot.style.left = parseInt(pferdRot.style.left) + 1 + 'px';
		pferdRotPosition += 1;
	}
	
	if(pferdRotPosition >= scrollWidth - 50)
	{
		document.getElementById('gewinner').innerHTML = 'Rot<br>';
		clearInterval(pferdRotInterval);
	}
}

function pferdGruenRechts() {
	if(pferdGruenPosition < scrollWidth - 50)
	{
		pferdGruen = document.getElementById('pferdgruen');
		pferdGruen.style.left = parseInt(pferdGruen.style.left) + 1 + 'px';
		pferdGruenPosition += 1;
	}
	
	if(pferdGruenPosition >= scrollWidth - 50)
	{
		document.getElementById('gewinner').innerHTML = 'Grün<br>';
		clearInterval(pferdGruenInterval);
	}
}

function pferdGelbRechts() {
	if(pferdGelbPosition < scrollWidth - 50)
	{
		pferdGelb = document.getElementById('pferdgelb');
		pferdGelb.style.left = parseInt(pferdGelb.style.left) + 1 + 'px';
		pferdGelbPosition += 1;
	}
	
	if(pferdGelbRechts >= scrollWidth - 50)
	{
		document.getElementById('gewinner').innerHTML = 'Gelb<br>';
		clearInterval(pferdGelbInterval);
	}
}

function pferdBlauRechts() {
	if(pferdBlauPosition < scrollWidth - 50)
	{
		pferdBlau = document.getElementById('pferdblau');
		pferdBlau.style.left = parseInt(pferdBlau.style.left) + 1 + 'px';
		pferdBlauPosition += 1;
	}
	
	if(pferdBlauPosition >= scrollWidth - 50)
	{
		document.getElementById('gewinner').innerHTML = 'Blau<br>';
		clearInterval(pferdBlauInterval);
	}
}










