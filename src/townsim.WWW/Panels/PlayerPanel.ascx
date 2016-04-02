<%@ Control Language="C#" Inherits="townsim.PlayerPanel" %>
<%@ Import namespace="townsim.Entities" %>
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
		<div>House: <%= (Player.Home != null ? (int)Player.Home.PercentComplete : 0) + "%" %><div class="bar" style="width: <%= (Player.Home != null ? (int)Player.Home.PercentComplete : 0)*2.5 %>px; background-color: brown;"></div></div>
		<div>&nbsp;</div>
		<div><b>Supplies</b></div>
		<div>Food: <%= (int)Player.Supplies[needTypes.Food] %>kg<div class="bar" style="width: <%= (int)Player.Supplies[needTypes.Food]*2.5 %>px; background-color: brown;"></div></div>
		<div>Water: <%= (int)Player.Supplies[needTypes.Water] %>ml<div class="bar" style="width: <%= (int)Player.Supplies[needTypes.Water]/3 %>px; background-color: lightblue;"></div></div>
		<div>Timber: <%= (int)Player.Supplies[NeedType.Timber] %><div class="bar" style="width: <%= (int)Player.Supplies[NeedType.Timber]*2.5 %>px; background-color: lightblue;"></div></div>
		<div>&nbsp;</div>
		<div><b>Priorities</b></div>
		<div>Food: <%= (int)Player.Priorities[PriorityTypes.Food] + "%" %><div class="bar" style="width: <%= (int)Player.Priorities[PriorityTypes.Food]*2.5 %>px; background-color: brown;"></div></div>
		<div>Water: <%= (int)Player.Priorities[PriorityTypes.Water] + "%" %><div class="bar" style="width: <%= (int)Player.Priorities[PriorityTypes.Water]*2.5 %>px; background-color: lightblue;"></div></div>
		<div>Shelter: <%= (int)Player.Priorities[PriorityTypes.Shelter] + "%" %><div class="bar" style="width: <%= (int)Player.Priorities[PriorityTypes.Shelter]*2.5 %>px; background-color: lightblue;"></div></div>
		<% } %>
	<% } %>
</div>