'use strict';

var webSocket = null;

function connect() {
    var url = `wss://${location.host}/`;
    console.log('url is: ' + url);

    webSocket = new WebSocket(url);

    webSocket.onopen = function (event) {
        console.log('Socket opened!');
    };

    webSocket.onclose = function (event) {
        console.log('Socket closed!');
    };

    webSocket.onmessage = function (event) {
        var message = event.data;

        $('#messages').append($('<li>').text(message));
    };
}

connect();