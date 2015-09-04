<%@ Page Language="C#" %>
<%@ Import Namespace="townsim.Data" %>
<%@ Import Namespace="townsim.Entities" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Towns</title>
	<script runat="server">
	public Town[] Towns { get;set; }

	void Page_Load()
	{
		var indexer = new TownIndexer();
		Towns = indexer.Get();
	}
	</script>
</head>
<body>
	<form id="form" runat="server">
		<link rel="stylesheet" type="text/css" href="Towns.css">
		<div id="body">
			<script language="javascript" type="text/javascript" src="jquery-2.1.3.min.js"></script>
			<script language="javascript" type="text/javascript" src="towns.js"></script>
			<h2>Towns</h2>
			<div id="listInner">
			<% foreach (Town town in Towns){ %>
			<div class='li' onmouseover="showTownProperties('<%= town.Id.ToString() %>')" onmouseout='hideTownProperties();'><%= town.Name %> - <%= town.Population %> <a href="Town.aspx?id=<%= town.Id.ToString() %>">open</a></div>
			<% } %>
			</div>
		</div>
	</form>
</body>
</html>

