<%@ Page Language="C#" %>
<%@ Import Namespace="townsim.Data" %>
<%@ Import Namespace="townsim.Entities" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Town</title>
	<script runat="server">
	public Town Town { get;set; }

	void Page_Load()
	{
		var id = new Guid(Request.QueryString["id"]);
		var reader = new TownReader();
		Town = reader.Read(id);
	}
	</script>
</head>
<body>
	<form runat="server">
		<div id="body">
			<h1><h1><%= Town.Name %></h1>
			<div>Population: <%= Town.Population %></div>
			<div>Forests: <%= Town.Forest %></div>
			<div>Water sources: <%= Town.WaterSources %></div>
			<div>Workers: <%= Town.Workers %></div>
			<div>Workers (available): <%= Town.WorkersAvailable %></div>
			<div>Builders: <%= Town.Builders %></div>
			<div>Houses: <%= Town.Buildings.TotalHouses %></div>
			<div>Incomplete Houses: <%= Town.Buildings.TotalIncompleteHouses %></div>
			<div style="float:left">
			<% foreach (var house in Town.Buildings.IncompleteHouses){ %>
			<div class="house" style="float:left">
				<div>House</div>
				<div><%= house.PercentComplete %>% complete</div>
				<div>Workers: <%= house.WorkerCount %></div>
			</div>
			<% } %>
			</div>
		</div>
	</form>
</body>
</html>

