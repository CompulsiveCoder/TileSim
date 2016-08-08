var refreshRate = 2000;
var hasStarted = false;

function startGame()
{
    var gameSpeed = $('#GameSpeed').val();
    var path = "NewGame.aspx?speed=" + gameSpeed;
    //alert(path);

    $.get(path, function(result){
        $result = $(result);
        $('#startPanel').hide();
        // TODO: Add output
    }, 'html');

    hasStarted=true;
}

function showGame()
{
    if (hasStarted == true)
    {
        $.get('Game.aspx', function(result){
            $result = $(result);

            $('#gameCont').empty();
            $result.find('style').appendTo('#gameCont');
            $result.find('#game').appendTo('#gameCont');
            $result.find('script').appendTo('#gameCont');
        }, 'html');
    }
    setTimeout(function()
    {
        showGame();
    }, refreshRate);
}