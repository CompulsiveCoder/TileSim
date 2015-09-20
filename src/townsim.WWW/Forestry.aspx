<%@ Page Language="C#" %>
<%@ Import Namespace="townsim.Data" %>
<%@ Import Namespace="townsim.Entities" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Forestry</title>
	<script runat="server">
	Town Town;

	void Page_Load()
	{
		// TOOD: Use a query string to specify the town
		//var townId = Request.QueryString["town"];
		//Town = new TownReader().Read(townId);

		var indexer = new TownIndexer();
		var towns = indexer.Get();
		Town = towns[0];
	}
	</script>
</head>
<body>
	<form id="form">
		<link rel="stylesheet" type="text/css" href="Towns.css">
		<script language="javascript" type="text/javascript" src="jquery-2.1.3.min.js"></script>
		<div id="body">
			<script language="javascript">
			function save()
			{
				var treesPerDay = $('#treesToPlant').val();

				var url = 'ForestrySave.aspx?town=<%= Town.Id %>&treesPerDay=' + treesPerDay;
				//window.open(url, '_blank')
				//alert(url);
				$.get(url, function(result){
			    	$result = $(result);
				}, 'html');

				$("#ForestryResult").html("Updated!");
			}
			</script>
			<h2>Forestry</h2>
			<div>Forests: <%= Convert.ToInt32(Town.Forest.Length) %></div>
			<div>Trees planted: <%= Town.TotalTreesPlanted %></div>
			<div>Trees to plant per day:<br/>
				<input id="treesToPlant" value='<%= Town.TreesToPlantPerDay %>'><input type="button" value="Update" onclick="save();"/></div>
			<div id="ForestryResult"></div>
		</div>
	</form>
</body>
</html>

