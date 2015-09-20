var panelRefreshRate = 10000;

function loadGame()
{
	show();
}

function show()
{
	showGameInfo();
	showPlayer();
	showLog();
	showTown();
	showTowns();
}

function showGameInfo()
{
	$('#gameCont').css("visibility", "visible");

	if( !$.trim( $('#gameCont').html() ).length ) {
		$('#gameCont').append("Loading...");
	}

	$.get('GameInfo.aspx', function(result){
    	$result = $(result);

    	$('#gameCont').empty();
    	//$result.find('style').html('#gameCont');
    	$result.find('#body').appendTo('#gameCont');
    	//$result.find('script').appendTo('#gameCont');
	}, 'html');

	setInterval(function()
    {
    	showGameInfo();
    }, panelRefreshRate);
}

function showTowns()
{
	$.get('Towns.aspx', function(result){
    	$result = $(result);

    	$result.find('style').html('#listCont');
    	$result.find('#body').appendTo('#listCont');
    	$result.find('script').appendTo('#listCont');
	}, 'html');
}

function showTown()
{
	$('#townCont').css("visibility", "visible");

	$.get('CurrentTown.aspx', function(result){
    	$result = $(result);

    	$('#townCont').empty();

    	//$result.find('style').html('#townCont');
    	$result.find('#body').appendTo('#townCont');
    	$result.find('script').appendTo('#townCont');
	}, 'html');

	setInterval(function()
    {
    	showTown();
    }, panelRefreshRate);
}


function showPlayer()
{
	$('#playerCont').css("visibility", "visible");

	$.get('Player.aspx', function(result){
    	$result = $(result);

    	$('#playerCont').empty();

    	//$result.find('style').html('#playerCont');
    	$result.find('#body').appendTo('#playerCont');
    	//$result.find('script').appendTo('#playerCont');
	}, 'html');

	setInterval(function()
    {
    	showPlayer();
    }, panelRefreshRate);

}

function showLog()
{
	$('#logCont').css("visibility", "visible");

	$.get('Log.aspx', function(result){
    	$result = $(result);

    	$('#logCont').empty();

    	//$result.find('style').html('#logCont');
    	$result.find('#body').appendTo('#logCont');
    	$result.find('script').appendTo('#logCont');
	}, 'html');

	setInterval(function()
    {
    	showLog();
    }, panelRefreshRate);
}

function showForestry()
{
	$('#forestryCont').css("visibility", "visible");

	$.get('Forestry.aspx', function(result){
    	$result = $(result);

    	//$result.find('style').html('#forestryCont');
    	$result.find('#body').appendTo('#forestryCont');
    	$result.find('script').appendTo('#forestryCont');
	}, 'html');
}

function newGame()
{
	window.location.replace("NewGame.aspx");
}

function editTreesToPlantPerDay()
{
	showForestry();
}