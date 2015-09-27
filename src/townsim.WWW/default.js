var refreshRate = 2000;

function newGame()
{
	window.location.replace("NewGame.aspx");
}

function showGame()
{
	$.get('Game.aspx', function(result){
    	$result = $(result);

    	$('#gameCont').empty();
    	$result.find('style').appendTo('#gameCont');
    	$result.find('#body').appendTo('#gameCont');
    	$result.find('script').appendTo('#gameCont');
	}, 'html');

	setTimeout(function()
    {
    	showGame();
    }, refreshRate);
}