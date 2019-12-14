"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

$('#send-message').attr('disabled', 'disabled');

connection.start().then(function () {
    $('#send-message').removeAttr('disabled');
}).catch(function (error) {
    return console.error(err.toString());
});

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = $('<li>').text(encodedMsg);

    $(li).appendTo('#messagesList');
});

$('#send-message').on('click', function (event) {
    var user = $('#User').val();
    var message = $('#Message').val();

    connection.invoke("SendMessageAsync", user, message).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();
});