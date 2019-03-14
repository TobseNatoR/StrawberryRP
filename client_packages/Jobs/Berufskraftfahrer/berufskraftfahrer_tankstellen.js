$('#abbrechen').click(() => {
	mp.trigger('Berufskraftfahrer_Tankstellenbrowserschliessen');
});


function tankeAngenommen() {
	var e = window.event,
	btn = e.target || e.srcElement;
	mp.trigger('TankeAngenommen', btn.id);
}