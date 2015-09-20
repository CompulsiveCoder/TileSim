<%@ Page Language="C#" Inherits="townsim.Default" %>
<%@ Import namespace="townsim.Data" %>
<%@ Import namespace="townsim.Engine" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Town Sim</title>
	<script runat="server">
	Guid EngineId = Guid.Empty;

	void Page_Load(object sender, EventArgs e)
	{
		var idString = Request.QueryString["engineId"];
		if (!String.IsNullOrEmpty(idString))
		{
			EngineId = Guid.Parse(idString);	
			if (EngineId != Guid.Empty)
				CurrentEngine.StartThread(EngineId);
		}
		else
		{
			if (CurrentEngine.Id != Guid.Empty)
				EngineId = CurrentEngine.Id;
		}
	}
	</script>
	<script language="javascript" type="text/javascript" src="jquery-2.1.3.min.js"></script>
	<script language="javascript" type="text/javascript" src="default.js"></script>
	<script language="javascript" type="text/javascript" src="jquery-ui.min.js"></script>
	<link rel="stylesheet" type="text/css" href="default.css">
</head>
<body>
	<form id="form1">
	<link rel="stylesheet" href="jquery-ui.css">
	<script language="javascript">
	<% if (EngineId != Guid.Empty){ %>
		$(document).ready(function(){
			loadGame();
		});
	<% } %>
	</script>
	<h1>Town Sim</h1>
		<div id="newGame" onclick="newGame();">New Game</div>
		<div id="leftCol">
			<div class="pnl" id="gameCont" style="visibility:hidden;"></div>
			<div class="pnl" id="playerCont" style="visibility:hidden;"></div>
			<div class="pnl" id="logCont" style="visibility:hidden;"></div>
		</div>
		<div id="midCol">
			<div class="pnl" id="townCont" style="visibility:hidden;"></div>
			<div class="pnl" id="listCont" style="visibility:hidden;"></div>
		</div>
		<div id="rightCol">
			<div class="pnl" id="forestryCont" style="visibility:hidden;"></div>
		</div>
	</form>
</body>
</html>

