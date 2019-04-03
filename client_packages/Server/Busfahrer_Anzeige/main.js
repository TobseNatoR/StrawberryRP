var busfahrerAnzeige;
mp.events.add('busfahreranzeigeoeffnen', () => {
    busfahrerAnzeige = mp.browsers.new('package://Server/Busfahrer_Anzeige/busfahrer.html');
});

mp.events.add('busfahreranzeigeschliessen', () => {
    busfahrerAnzeige.destroy();
});

mp.events.add('busdaten', (passwort) => {
    mp.events.callRemote('LoginVersuch', passwort);
});


