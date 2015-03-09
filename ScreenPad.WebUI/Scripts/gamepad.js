$(function () {
    
    setButtonsSize();
    
    $(window).resize(function () {
        setButtonsSize();
    });
    
    // Reference the auto-generated proxy for the hub.  
    var game = $.connection.gameHub,
        gameConnectionName = $('#container').data('gameConnectionName'),
        connectionName = "gp" + gameConnectionName,
        holdInterval,
        holdTimeout,
        holdTimeoutDuration = 500,
        holdIntervalPeriod = 50;
    
    game.client.addNewMessageToPage = function (message, id) {
        if (id === gameConnectionName) {
            switch (message) {
                case "gameover": alert('игра окончена'); break;
            }
        }
    };
    // Start the connection.
    $.connection.hub.qs = "connectionName=" + connectionName;
    $.connection.hub.start().done(function () {
        game.server.send("start", gameConnectionName);
        $('.button').on("touchstart", function (ev) {
            var message = $(this).data("message");
            game.server.send(message, gameConnectionName);
            if (message === "down") {
                holdTimeout = setTimeout(function() {
                    holdInterval = setInterval(function() {
                        console.log(message);
                        game.server.send(message, gameConnectionName);
                    }, holdIntervalPeriod);
                }, holdTimeoutDuration);
            }
            return false;
        });
        $('.button').on("touchend", function (ev) {
            clearInterval(holdInterval);
            clearTimeout(holdTimeout);
            return false;
        });
    });
    $.connection.hub.stateChanged(connectionStateChanged);
});



function connectionStateChanged(state) {
    var stateConversion = { 0: 'connecting', 1: 'connected', 2: 'reconnecting', 4: 'disconnected' };
    console.log('SignalR state changed from: ' + stateConversion[state.oldState]
     + ' to: ' + stateConversion[state.newState]);
    if (state.newState === 2) {
        $.connection.hub.stop();
        $.connection.hub.start();
    }
}

function setButtonsSize() {
    // Make action button circle
    $('#action').height($('#action').width());
    $('#action').css('border-radius', $('#action').width() / 2 + 3);

    // Set cross block height equal to its width
    $('#cross').height($('#cross').width());

    // Set action button "top"
    $('#action').offset({ top: $('#cross').offset().top + $('#cross').height() / 2 - $('#action').height() / 2 });
}