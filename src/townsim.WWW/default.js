$(document).ready(function(){
	showTowns();
});

function showTowns()
{
	$.get('Towns.aspx', function(result){
    	$result = $(result);

    	$result.find('style').appendTo('#listCont');
    	$result.find('#body').appendTo('#listCont');
    	$result.find('script').appendTo('#listCont');
	}, 'html');
	/*$( "#listCont" ).load( "Towns.aspx #body" );*/
}