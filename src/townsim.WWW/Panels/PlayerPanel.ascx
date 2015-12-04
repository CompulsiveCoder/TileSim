<%@ Control Language="C#" Inherits="townsim.PlayerPanel" %>
<div class="pnl">
	<h2>Player</h2>
	<% if (Player != null){ %>
		<% if (Player.Health == 0){ %>
			<div>Player died. Game over!</div>
		<% } else { %>
		<div>Age: <%= Convert.ToInt32(Player.Age) %></div>
		<div>Health: <%= (int)Player.Health %>%<div class="bar" style="width: <%= (int)(Player.Health*2.5m) %>px; background-color: red;"></div></div>
		<div>Thirst: <%= (int)Player.Thirst %>%<div class="bar" style="width: <%= (int)(Player.Thirst*2.5m) %>px; background-color: lightblue;"></div></div>
		<div>Hunger: <%= (int)Player.Hunger %>%<div class="bar" style="width: <%= (int)(Player.Hunger*2.5m) %>px; background-color: lightgreen;"></div></div>
		<div>Activity: <%= Player.Activity.ToString() %></div>
		<div>House: <%= (Player.Home != null ? (int)Player.Home.PercentComplete : 0) + "%" %><div class="bar" style="width: <%= (Player.Home != null ? (int)Player.Home.PercentComplete : 0)*2.5 + "%" %>px; background-color: lightbrown;"></div></div>
		<% } %>
	<% } %>
</div>