

function showTownProperties(id)
{
	$( "#propInner" ).load( "Town.aspx?id=" + id + " #body" );
}


function hideTownProperties()
{
	$("#propInner").empty();
}