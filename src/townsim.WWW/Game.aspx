<%@ Page Language="C#" Inherits="townsim.Game" %>
<%@ Import namespace="townsim.Engine" %>
<%@ Register TagPrefix="uc" TagName="GameInfo" Src="~/Panels/GameInfoPanel.ascx" %>
<%@ Register TagPrefix="uc" TagName="Player" Src="~/Panels/PlayerPanel.ascx" %>
<%@ Register TagPrefix="uc" TagName="Town" Src="~/Panels/TownPanel.ascx" %>
<%@ Register TagPrefix="uc" TagName="Log" Src="~/Panels/LogPanel.ascx" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Game</title>
	<script runat="server">
	</script>
</head>
<body>
	<form id="form1" runat="server">
		<div id="body">
			<% if (CurrentEngine.Id != Guid.Empty) { %>
				<div id="leftCol">
					<uc:GameInfo id="GameInfo" runat="server" />
					<uc:Player id="Player" runat="server" />
					<uc:Log id="Log" runat="server" />
				</div>
				<div id="midCol">
					<uc:Town id="CurrentTown" runat="server" />
				</div>

			<% } else { %>
				<div>A game hasn't been started yet. Click "New Game" to begin.</div>
			<% } %>
		</div>
	</form>
</body>
</html>

