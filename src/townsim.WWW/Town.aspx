<%@ Page Language="C#" %>
<%@ Import Namespace="townsim.Data" %>
<%@ Import Namespace="townsim.Engine" %>
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
	<form>
		<div id="body">
			<h1><%= Town.Name %></h1>
			<h2>People</h2>
			<div>Population: <%= Town.Population %></div>
			<div>Males: <%= Town.TotalMales %></div>
			<div>Females: <%= Town.TotalFemales %></div>
			<div>Workers (employed): <%= Town.TotalEmployed %><div class="bar" style="width: <%= Town.TotalEmployed %>px; background-color: lightgray;"></div></div>
			<div>Workers (unemployed): <%= Town.TotalUnemployed %><div class="bar" style="width: <%= Town.TotalUnemployed %>px; background-color: lightgray;"></div></div>
			<div>Builders: <%= Town.TotalBuilders %><div class="bar" style="width: <%= Town.TotalBuilders %>px; background-color: lightgray;"></div></div>
			<div>Births: <%= Town.TotalBirths %><div class="bar" style="width: <%= Town.TotalBirths %>px; background-color: lightgray;"></div></div>
			<div>Deaths: <%= Town.TotalDeaths %><div class="bar" style="width: <%= Town.TotalDeaths %>px; background-color: lightgray;"></div></div>
			<div>Immigrants: <%= Town.TotalImmigrants %><div class="bar" style="width: <%= Town.TotalImmigrants %>px; background-color: lightgray;"></div></div>
			<div>Emigrants: <%= Town.TotalEmigrants %><div class="bar" style="width: <%= Town.TotalEmigrants %>px; background-color: lightgray;"></div></div>
			<div>Couples: <%= Town.TotalBreedingPairs %><div class="bar" style="width: <%= Town.TotalBreedingPairs %>px; background-color: lightgray;"></div></div>
			<div>Homeless People: <%= Town.TotalHomelessPeople %><div class="bar" style="width: <%= Town.TotalHomelessPeople %>px; background-color: lightgray;"></div></div>
			<h2>Resources</h2>
			<div>Timber: <%= (int)Town.Timber %><div class="bar" style="width: <%= (int)Town.Timber / 10 %>px; background-color: brown;"></div></div>
			<div>Water sources: <%= (int)Town.WaterSources %> litres<div class="bar" style="width: <%= (int)(Town.WaterSources / 100) %>px; background-color: lightblue;"></div></div>
			<div>Food sources: <%= (int)Town.FoodSources %> kgs<div class="bar" style="width: <%= (int)(Town.FoodSources / 100) %>px; background-color: lightgreen;"></div></div>
			<h2>Forestry</h2>
			<div>Forestry workers: <%= Town.TotalForestryWorkers %><div class="bar" style="width: <%= Town.TotalForestryWorkers %>px; background-color: lightgreen;"></div></div>
			<div>Trees planted today: <%= Town.CountTreesPlantedToday(CurrentEngine.Clock.GameDuration) %><div class="bar" style="width: <%= Town.CountTreesPlantedToday(CurrentEngine.Clock.GameDuration) %>px; background-color: lightgreen;"></div></div>
			<div>Trees planted: <%= Town.TotalTreesPlanted %><div class="bar" style="width: <%= Town.TotalTreesPlanted %>px; background-color: lightgreen;"></div></div>
			<div>Trees being planted: <%= Town.TotalTreesBeingPlanted %><div class="bar" style="width: <%= Town.TotalTreesBeingPlanted %>px; background-color: lightgreen;"></div></div>
			<h2>Environment</h2>
			<div>Trees: <%= Town.Trees.Length %><div class="bar" style="width: <%= Town.Trees.Length %>px; background-color: green;"></div></div>
			<div>Average tree size: <%= (int)Town.AverageTreeSize %><div class="bar" style="width: <%= (int)Town.AverageTreeSize %>px; background-color: green;"></div></div>
			<div>Average tree age: <%= (int)Town.AverageTreeAge %><div class="bar" style="width: <%= (int)Town.AverageTreeAge %>px; background-color: green;"></div></div>
			<h2>Buildings</h2>
			<div>Houses: <%= Town.Buildings.TotalCompletedHouses %><div class="bar" style="width: <%= Town.Buildings.TotalCompletedHouses %>px; background-color: brown;"></div></div>
			<div>Incomplete Houses: <%= Town.Buildings.TotalIncompleteHouses %><div class="bar" style="width: <%= Town.Buildings.TotalIncompleteHouses %>px; background-color: brown;"></div></div>
			<div>Average Percent Complete: <%= (int)Town.Buildings.AveragePercentComplete %><div class="bar" style="width: <%= (int)Town.Buildings.AveragePercentComplete %>px; background-color: brown;"></div></div>

			<% if (false) { %>
			<div style="float:left">	
			<% foreach (var house in Town.Buildings.IncompleteHouses){ %>
			<div class="house" style="float:left">
				<div>House</div>
				<div><%= house.PercentComplete %>% complete</div>
				<div>Workers: <%= house.Workers.Length %></div>
			</div>
			<% } %>
			</div>

			<% } %>
		</div>
	</form>
</body>
</html>

