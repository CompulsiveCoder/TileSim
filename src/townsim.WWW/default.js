$(document).ready(function(){
	showTowns();
	startLoop();
});

function startLoop()
{
    var refreshId = setInterval(function()
    {
    	showTowns();
    }, 2000);
}

function showTowns()
{
	$.get('Towns.aspx', function(result){
    	$result = $(result);

    	$('#listCont').empty();
    	$result.find('style').appendTo('#listCont');
    	$result.find('#body').appendTo('#listCont');
    	$result.find('script').appendTo('#listCont');
	}, 'html');
}