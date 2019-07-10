$('#absendebutton').click(() => {
    $('.alert').remove(); 
    mp.trigger('geburtstagzumserver', $('#datum').val());
});


