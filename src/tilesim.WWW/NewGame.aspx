<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="tilesim.Engine" %>
<%@ Import Namespace="tilesim.Web" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>New Game</title>
	<script runat="server">
	void Page_Load()
	{
        var speed = Convert.ToInt32(Request.QueryString["speed"]);
		EngineWebHolder.Current.StartGame(speed);
		Response.Redirect("Default.aspx");
	}
	</script>
</head>
<body>
	<form id="form" runat="server">
		<div id="body">
		Game started
		</div>
	</form>
</body>
</html>

