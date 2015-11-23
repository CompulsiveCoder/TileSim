﻿<%@ Control Language="C#" Inherits="townsim.TownPanel" AutoEventWireup="true" %>
<%@ Import Namespace="townsim.Entities" %>
<%@ Import Namespace="townsim.Engine" %>
<script runat="server">
</script>
  <script language="javascript" type="text/javascript" src="jquery-2.1.3.min.js"></script>
<script language="javascript">
function editTreesToPlantPerDay()
{
  $('#forestryCont').css("visibility", "visible");

  $.get('Forestry.aspx', function(result){
      $result = $(result);

      //$result.find('style').html('#forestryCont');
      $result.find('#body').appendTo('#forestryCont');
      $result.find('script').appendTo('#forestryCont');
  }, 'html');
}
</script>
<div class="pnl">
	<h1><%= Town.Name %></h1>
	<h2>People</h2>
	<div>Population: <%= Town.Population %><div class="bar" style="width: <%= Town.Population %>px; background-color: lightgray;"></div></div>
	<div>Males: <%= Town.TotalMales %><div class="bar" style="width: <%= Town.TotalMales %>px; background-color: lightgray;"></div></div>
	<div>Females: <%= Town.TotalFemales %><div class="bar" style="width: <%= Town.TotalFemales %>px; background-color: lightgray;"></div></div>
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
	<div>Water sources: <%= (int)Town.WaterSources/1000 %>kl<div class="bar" style="width: <%= (int)(Town.WaterSources / 1000) %>px; background-color: lightblue;"></div></div>
	<div>Food sources: <%= (int)Town.FoodSources %> kgs<div class="bar" style="width: <%= (int)(Town.FoodSources / 1000) %>px; background-color: lightgreen;"></div></div>
	<h2>Forestry</h2>
	<div>Forestry workers: <%= Town.TotalForestryWorkers %><div class="bar" style="width: <%= Town.TotalForestryWorkers %>px; background-color: lightgreen;"></div></div>
	<div><a href="javascript:void(0);" onclick="editTreesToPlantPerDay();">Trees to plant per day: <%= Town.TreesToPlantPerDay %></a><div class="bar" style="width: <%= Town.TreesToPlantPerDay %>px; background-color: lightgreen;"></div></div>
	<div>Trees planted today: <%= Town.CountTreesPlantedToday(CurrentEngine.Clock.GameDuration) %><div class="bar" style="width: <%= Town.CountTreesPlantedToday(CurrentEngine.Clock.GameDuration) %>px; background-color: lightgreen;"></div></div>
	<div>Trees planted: <%= Town.TotalTreesPlanted %><div class="bar" style="width: <%= Town.TotalTreesPlanted/5 %>px; background-color: lightgreen;"></div></div>
	<div>Trees being planted: <%= Town.TotalTreesBeingPlanted %><div class="bar" style="width: <%= Town.TotalTreesBeingPlanted %>px; background-color: lightgreen;"></div></div>
	<h2>Environment</h2>
	<div>Trees: <%= Town.Trees.Length %><div class="bar" style="width: <%= Town.Trees.Length/10 %>px; background-color: green;"></div></div>
	<div>Average tree size: <%= (int)Town.AverageTreeSize %><div class="bar" style="width: <%= (int)Town.AverageTreeSize/5 %>px; background-color: green;"></div></div>
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