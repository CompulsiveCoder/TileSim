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
			<div>Forests: <%= Convert.ToInt32(Town.Forest) %></div>
			<div>Water sources: <%= (int)Town.WaterSources %><div class="bar" style="width: <%= (int)(Town.WaterSources / 10) %>px; background-color: lightblue;"></div></div>
			<div>Food sources: <%= (int)Town.FoodSources %><div class="bar" style="width: <%= (int)(Town.FoodSources / 10) %>px; background-color: lightgreen;"></div></div>
			<div>Workers (employed): <%= Town.TotalEmployed %><div class="bar" style="width: <%= Town.TotalEmployed %>px; background-color: lightgray;"></div></div>
			<div>Workers (unemployed): <%= Town.TotalUnemployed %><div class="bar" style="width: <%= Town.TotalUnemployed %>px; background-color: lightgray;"></div></div>
			<div>Builders: <%= Town.TotalBuilders %><div class="bar" style="width: <%= Town.TotalBuilders %>px; background-color: lightgray;"></div></div>
			<div>Homeless People: <%= Town.TotalHomelessPeople %><div class="bar" style="width: <%= Town.TotalHomelessPeople %>px; background-color: lightgray;"></div></div>
			<div>Houses: <%= Town.Buildings.Count %><div class="bar" style="width: <%= Town.Buildings.Count %>px; background-color: brown;"></div></div>
			<div>Incomplete Houses: <%= Town.Buildings.TotalIncompleteHouses %><div class="bar" style="width: <%= Town.Buildings.TotalIncompleteHouses %>px; background-color: brown;"></div></div>

			<% if (false) { %>
			<div style="float:left">
			<% foreach (var house in Town.Buildings.IncompleteHouses){ %>
			<div class="house" style="float:left">
				<div>House</div>
				<div><%= house.PercentComplete %>% complete</div>
				<div>Workers: <%= house.WorkerCount %></div>
			</div>
			<% } %>
			</div>

			<% } %>
		</div>
	</form>
</body>
</html>

