$('#schliessen').click(() => {
    $('.alert').remove(); 
    mp.trigger('hilfebrowserschliessen');
});


