'use strict';

var socket;

$('#connect').on('click', function (event) {
    var url = `wss://${location.host}/test`;

    socket = new WebSocket(url);

    socket.onopen = function (event) {
        console.log('Socket opened!');
    };

    socket.onclose = function (event) {
        console.log('Socket closed!');
    };

    socket.onmessage = function (event) {
        console.log('Message:' + event.data);

        $('#messages').append($('<li>').text(`Message from server: ${event.data}`));
    };

    console.log('Socket connected!');
});

$('#close').on('click', function (event) {
    socket.close(1000, 'Closed from client!');
});

$('#send-btn').on('click', function (event) {
    var message = $('#message').val();

    socket.send(message);

    $('#messages').append($('<li>').text(`Message from client: ${message}`));
});