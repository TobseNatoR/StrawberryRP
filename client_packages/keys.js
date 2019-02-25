//Taste M
mp.keys.bind(0x4D, true, function () {
    mp.events.callRemote('AutoAnAus');
});

//Taste Enter
mp.keys.bind(0x0D, true, function () {
    mp.events.callRemote('EnterCheck');
});

//Taste E
mp.keys.bind(0x45, true, function () {
    mp.events.callRemote('ECheck');
});

//Taste K
mp.keys.bind(0x4B, true, function () {
    mp.events.callRemote('KCheck');
});

//Taste B
mp.keys.bind(0x42, true, function () {
    mp.events.callRemote('BCheck');
});

//Taste F2
mp.keys.bind(0x71, true, function () {
    mp.events.callRemote('F2Check');
});

