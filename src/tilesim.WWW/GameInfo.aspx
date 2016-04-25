<%@ Page Language="C#" %>
<%@ Register TagPrefix="uc" TagName="GameInfo" Src="~/Panels/GameInfoPanel.ascx" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Game Info</title>
</head>
<body>
	<form id="form">
		<uc:GameInfo id="GameInfo" runat="server" />
	</form>
</body>
</html>

