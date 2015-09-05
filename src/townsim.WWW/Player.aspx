<%@ Page Language="C#" %>
<%@ Import Namespace="townsim.Data" %>
<%@ Import Namespace="townsim.Entities" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Player</title>
	<script runat="server">
	Person Player;
	Town Town;

	void Page_Load()
	{
		var indexer = new TownIndexer();
		var towns = indexer.Get();
		Town = towns[0];
		Player = Town.People[0];

	}
	</script>
</head>
<body>
	<form id="form">
		<link rel="stylesheet" type="text/css" href="Towns.css">
		<div id="body">
			<script language="javascript" type="text/javascript" src="jquery-2.1.3.min.js"></script>
			<script language="javascript" type="text/javascript" src="player.js"></script>
			<h2>Player</h2>
			<% if (Player.Health == 0){ %>
			<div>Player died. Game over!</div>
			<% } %>
			<div>Age: <%= Convert.ToInt32(Player.Age) %></div>
			<div>Health: <%= (int)Player.Health %>%<div style="width: <%= Player.Health*2.5 %>px; background-color: red;"></div></div>
			<div>Thirst: <%= (int)Player.Thirst %>%<div style="width: <%= Player.Thirst*2.5 %>px; background-color: lightblue;"></div></div>
			<div>Hunger: <%= (int)Player.Hunger %>%<div style="width: <%= Player.Hunger*2.5 %>px; background-color: lightgreen;"></div></div>
		</div>
	</form>
</body>
</html>

