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

var i = 0;
function showGame()
{
    if (hasStarted == true)
    {
        if (i  > 0)
        {
            $.get("Game.aspx").done(function(r) {
                var newDom = $(r);
                $('#PlayerPanel').replaceWith($('#PlayerPanel',newDom));
                $('#TilePanel').replaceWith($('#TilePanel',newDom));
                $('#GameInfoPanel').replaceWith($('#GameInfoPanel',newDom));
                $('#CurrentActivityPanel').replaceWith($('#CurrentActivityPanel',newDom));
                //$('#assigned').replaceWith($('#assigned',newDom));
                //$('#pending').replaceWith($('#pending',newDom));
             });
         }
         else
         {
        /*$.ajax({
          url: "Game.aspx",
          cache: false,
          success: function(data){
             $("#PlayerPanel").html(data);
          } 
        });*/
            $.get('Game.aspx', function(result){
                $result = $(result);

                $('#gameCont').empty();
                $result.find('style').appendTo('#gameCont');
                $result.find('#game').appendTo('#gameCont');
                $result.find('script').appendTo('#gameCont');
            }, 'html');
            i = 1;
        }
    }
    setTimeout(function()
    {
        showGame();
    }, refreshRate);
}