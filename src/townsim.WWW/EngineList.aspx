<%@ Page Language="C#" Inherits="townsim.Default" %>
<%@ Import namespace="townsim.Data" %>
<%@ Import namespace="townsim.Engine" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Engine List</title>
	<script runat="server">
	string EngineId = String.Empty;

	void Page_Load(object sender, EventArgs e)
	{
		EngineId = Request.QueryString["engineId"];	
		if (!String.IsNullOrEmpty(EngineId))
			CurrentEngine.StartThread(EngineId);
	}
	</script>
	<script language="javascript" type="text/javascript" src="jquery-2.1.3.min.js"></script>
	<script language="javascript" type="text/javascript" src="default.js"></script>
</head>
<body>
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

	<form id="form1">
	<link rel="stylesheet" type="text/css" href="default.css">
	<h1>Engine List/h1>
		<div class="pnl" id="propCont">
			<% if (String.IsNullOrEmpty(EngineId)){ %>
			<% foreach (var engineId in new EngineIdManager().GetIds()){ %>
				<div><a href="Default.aspx?engineId=<%= engineId %>"><%= engineId %></a></div>
			<% } %>
			<% } %>
			<div id="propInner">
			</div>
		</div>
	</form>
</body>
</html>

