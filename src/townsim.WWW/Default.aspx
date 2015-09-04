<%@ Page Language="C#" Inherits="townsim.Default" %>
<%@ Import namespace="townsim.Data" %>
<%@ Import namespace="townsim" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Default</title>
	<script runat="server">
	string EngineId = String.Empty;

	void Page_Load(object sender, EventArgs e)
	{
		EngineId = Request.QueryString["engineId"];	
		CurrentEngine.Attach(EngineId);
	}
	</script>
	<script language="javascript" type="text/javascript" src="jquery-2.1.3.min.js"></script>
	<script language="javascript" type="text/javascript" src="default.js"></script>
</head>
<body>
	<link rel="stylesheet" type="text/css" href="default.css">
	<style>
		body
		{
			font-family: Verdana;
			font-size: 11px;
		}

		div
		{
			padding: 4px;
		}
	</style>
	<script language="javascript">
	function updateClock ( )
 	{
 	var currentTime = new Date ( );
  	var currentHours = currentTime.getHours ( );
  	var currentMinutes = currentTime.getMinutes ( );
  	var currentSeconds = currentTime.getSeconds ( );

  	// Pad the minutes and seconds with leading zeros, if required
  	currentMinutes = ( currentMinutes < 10 ? "0" : "" ) + currentMinutes;
  	currentSeconds = ( currentSeconds < 10 ? "0" : "" ) + currentSeconds;

  	// Choose either "AM" or "PM" as appropriate
  	var timeOfDay = ( currentHours < 12 ) ? "AM" : "PM";

  	// Convert the hours component to 12-hour format if needed
  	currentHours = ( currentHours > 12 ) ? currentHours - 12 : currentHours;

  	// Convert an hours component of "0" to "12"
  	currentHours = ( currentHours == 0 ) ? 12 : currentHours;

  	// Compose the string for display
  	var currentTimeString = currentHours + ":" + currentMinutes + ":" + currentSeconds + " " + timeOfDay;
  	
  	
   	$("#clock").html(currentTimeString);
   	  	
 }

$(document).ready(function()
{
   setInterval('updateClock()', 1000);
});
	</script>

	<form id="form1">
	<h1>Town Sim</h1>
	<div id="clock"></div>
		<div class="pnl" id="playerCont">
		</div>
		<div class="pnl" id="townCont">
		</div>
		<div class="pnl" id="listCont">
		</div>
		<div class="pnl" id="propCont">
			<% if (String.IsNullOrEmpty(EngineId)){ %>
			<% foreach (var engineId in new EngineIdManager().GetIds()){ %>
				<div><a href="Default.aspx?engineId=<%= engineId %>"><%= engineId %></a></div>
			<% } %>
			<% } %>
			<div id="propInner">
			</div>
		</div>
		<div class="pml" id="gardenCont">
		<div>Start Garden</div>
		</div>
	</form>
</body>
</html>

