﻿$(document).ready(function(){
	showPlayer();
	showTown();
	showTowns();
	startLoop();
});

function startLoop()
{
    var refreshId = setInterval(function()
    {
    	showPlayer();
    	showTown();
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

function showTown()
{
	$.get('CurrentTown.aspx', function(result){
    	$result = $(result);

    	$('#townCont').empty();
    	$result.find('style').appendTo('#townCont');
    	$result.find('#body').appendTo('#townCont');
    	$result.find('script').appendTo('#townCont');
	}, 'html');
}


function showPlayer()
{
	$.get('Player.aspx', function(result){
    	$result = $(result);

    	$('#playerCont').empty();
    	$result.find('style').appendTo('#playerCont');
    	$result.find('#body').appendTo('#playerCont');
    	$result.find('script').appendTo('#playerCont');
	}, 'html');
}