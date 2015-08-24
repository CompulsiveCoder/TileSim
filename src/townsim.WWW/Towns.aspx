<%@ Page Language="C#" %>
<%@ Import Namespace="townsim.Data" %>
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
		<script type="text/javascript">
		//$(document).ready(function(){
		//	drawList();
		//});


		//function drawList()
		//{
		//	for (var i = 0; i < 10; i++)
		//	{
		//		$("#listInner").append("<div class='li' onmouseover='showTownProperties();' onmouseout='hideTownProperties();'>Item " + i + "</div>");
		//	}
		//}
		</script>
		<link rel="stylesheet" type="text/css" href="Towns.css">
		<div id="body">
			<script language="javascript" type="text/javascript" src="jquery-2.1.3.min.js"></script>
			<script language="javascript" type="text/javascript" src="towns.js"></script>
			<h2>Towns</h2>
			<div id="listInner">
			<% foreach (Town town in Towns){ %>
			<div class='li' onmouseover="showTownProperties('<%= town.Id.ToString() %>')" onmouseout='hideTownProperties();'><%= town.Name %> - <%= town.Population %></div>
			<% } %>
			</div>
		</div>
	</form>
</body>
</html>

