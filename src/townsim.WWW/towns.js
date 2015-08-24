

function showTownProperties()
{
	$( "#propInner" ).load( "Town.aspx #body" );
}


function hideTownProperties()
{
	$("#propInner").empty();
}