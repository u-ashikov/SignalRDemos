"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/voteHub")
    .withAutomaticReconnect()
    .build();

$('#vote-btn').attr('disabled', 'disabled');

connection.start().then(function () {
    console.log("SignalR connected!");
    $('#vote-btn').removeAttr('disabled');
});

$('#vote-form').on('submit', function (event) {
    event.preventDefault();

    var user = $('#User').val();
    var voteChoice = $('#vote-form input[name="Choice"]:checked').val();

    connection.invoke("Vote", { user: user, voteChoice: parseInt(voteChoice) })
        .catch(error => console.error(error));
});

connection.on('ReceiveVotingResultAsync', function (response) {
    var voteLog = `User ${response.user} voted for team - ${response.selectedTeam}.`;

    var voteResult = $('<div>').text(voteLog);

    $('#voting-log').append(voteResult);

    var $teamPointsContainer = $(`div#${response.selectedTeam} span.voting-points`)

    var teamPoints = $teamPointsContainer.text();

    $teamPointsContainer.text(++teamPoints);
});