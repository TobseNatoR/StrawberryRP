$('#absendebutton').click(() => {
    $('.alert').remove(); 
    mp.trigger('nicknamezumserver', $('#nickname').val());
});


