'use strict';

var socket;

//var socketConnector = function () {
//    var socket;

//    this.connect = function (url) {
//        if (!url) {
//            url = `wss://${location.host}/test`;
//        }

//        socket = new WebSocket(url);

//        socket.onopen = function (event) {
//            console.log('Socket opened!');
//        };

//        socket.onclose = function (event) {
//            console.log('Socket closed!');
//        };

//        socket.onmessage = function (event) {
//            console.log('Message:' + event.data);
//        };

//        console.log('Socket connected!');
//    };

//    this.sendMessage = function (message) {
//        socket.send(message);
//    };
//};

//var connector = new socketConnector();

// TODO: Create function to operate with sockets.

$('#connect').on('click', function (event) {
    var url = `wss://${location.host}/test`;

    socket = new WebSocket(url);

    socket.onopen = function (event) {
        console.log('Socket opened!');

        ckeckHealth();
    };

    socket.onclose = function (event) {
        console.log('Socket closed!');

        ckeckHealth();
    };

    socket.onmessage = function (event) {
        console.log('Message:' + event.data);

        $('#messages').append($('<li>').text(`Message from server: ${event.data}`));
    };

    console.log('Socket connected!');
});

$('#close').on('click', function (event) {
    if (!socket || socket.readyState !== WebSocket.OPEN) {
        alert('Socket not connected!');
    }

    socket.close(1000, 'Closed from client!');
});

$('#send-btn').on('click', function (event) {
    if (!socket || socket.readyState !== WebSocket.OPEN) {
        alert('Socket not connected!');
    }

    var message = $('#message').val();

    socket.send(message);

    $('#messages').append($('<li>').text(`Message from client: ${message}`));
});

function ckeckHealth() {
    if (!socket || socket.readyState !== WebSocket.OPEN) {
        $('#send-btn, #close').attr('disabled', 'disabled');
        $('div#socket-status').text('Closed');
    } else {
        $('#send-btn, #close').removeAttr('disabled', 'disabled');
        $('div#socket-status').text('Connected');
    }
}

ckeckHealth();