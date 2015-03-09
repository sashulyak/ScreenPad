$(function () {
    var $qrPopup = $('#start'),
        $resultPopup = $('#gameOver'),
        $totalScoreField = $('#totalScore'),
        $countdown = $('#countdown'),
        $scoreBar = $('#score'),
        $level = $('#level'),
        $resultScoreField = $('#resultScore'),
        connectionName = $('#container').data('connectionName'),
        prevScoreInPercents = 0,
        timeLeft = 60,
        minLevelScore = 300,
        scoreStep = 300,
        level = 1,
        speedShiftingCoefficient = 0.8,
        colors = ['#FFE656', '#FFA473', '#FF5A6B', '#AF7CFB', '#37D4F3', '#39F392'];

    // Reference the auto-generated proxy for the hub.  
    var game = $.connection.gameHub;
    
    function restartCountdown(seconds) {
        $countdown.countdown('destroy');
        initCountdown(seconds);
    }

    function showResults(totalScore) {
        pause();
        $resultScoreField.text(totalScore);
        $resultPopup.show();
        setTimeout(function() {
            window.location.reload();
        }, 5000);
    }

    function setHighScore(score) {
        $.ajax({
            type: "POST",
            url: "/game/sethighscore",
            data: { highScore: score }
        });
    }

    function timeIsOver() {
        if (levelScore < minLevelScore) {
            setHighScore(score);
            showResults(score);
        }
    }
    
    function checkIfGameOver() {
        if (gameOver) {
            setHighScore(score);
            showResults(score);
        } 
    }

    function displayTotalScore() {
        $totalScoreField.text(score);
    }

    function getGoalPercents() {
        var levelScore = score % minLevelScore;
        return ((levelScore / minLevelScore) * 100).toFixed();
    }

    function drawScoreProgressBar() {
        var newBarHeight = getGoalPercents();
        if ((prevScoreInPercents === newBarHeight && $scoreBar.height() !== 0)) return;
        prevScoreInPercents = newBarHeight;
        $scoreBar.animate({
            height: newBarHeight + "%",
            backgroundColor: colors[(level % colors.length) - 1]
        }
        );
    }

    function timerTick() {
        checkIfGameOver();
        displayTotalScore();
        calculateCurrentLevel();
        drawScoreProgressBar();
    }

    function calculateCurrentLevel() {
        var oldLevel = level;
        level = integerDivision(score, scoreStep) + 1;
        minLevelScore = scoreStep * level;
        if (oldLevel != level) {
            restartCountdown(timeLeft);
            speed.start *= speedShiftingCoefficient;
        }
        $level.text(level);
    }

    function initCountdown(seconds) {
        var date = new Date();
        date.setSeconds(date.getSeconds() + seconds);
        $countdown.countdown({
            until: date,
            onExpiry: timeIsOver,
            format: 'S',
            compact: true,
            onTick: timerTick
        });
    }

    game.client.addNewMessageToPage = function (message) {
        if (playing) {
            switch (message) {                
            case "left":
                actions.push(DIR.LEFT);
                break;
            case "right":
                actions.push(DIR.RIGHT);
                break;
            case "up":
                actions.push(DIR.UP);
                break;
            case "down":
                actions.push(DIR.DOWN);
                break;
            }
        } else {
            if (message === "start") {
                $($qrPopup).hide();
                play();
                initCountdown(timeLeft);
            }
        }
    };
    
    // Start the connection.
    $.connection.hub.qs = "connectionName=" + connectionName;
    $.connection.hub.start().done(function () {
    });

    function connectionStateChanged(state) {
        var stateConversion = { 0: 'connecting', 1: 'connected', 2: 'reconnecting', 4: 'disconnected' };
        console.log('SignalR state changed from: ' + stateConversion[state.oldState]
            + ' to: ' + stateConversion[state.newState]);
        if (state.newState === 2) {
            $countdown.countdown('pause');
            pause();
            $('#reconnecting').css('display', 'block');
            $.connection.hub.stop();
            $.connection.hub.start().done(function () {
                $('#reconnecting').css('display', 'none');
                resume();
                $countdown.countdown('resume');
            });
        }
    }

    function integerDivision(x, y) {
        return (x - x % y) / y;
    }

    $.connection.hub.stateChanged(connectionStateChanged);
});

