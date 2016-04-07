<%@ Control Language="C#" Inherits="townsim.PlayerPanel" %>
<%@ Import namespace="townsim.Engine.Entities" %>
<div class="pnl">
	<h2>Player</h2>
	<% if (Player != null){ %>
		<% if (!Player.IsAlive){ %>
			<div>Player died. Game over!</div>
		<% } else { %>
    		<div>Age: <%= Convert.ToInt32(Player.Age) %></div>
    		<div>Health: <%= (int)Player.Vitals[PersonVital.Health] %>%<div class="bar" style="width: <%= (int)(Player.Vitals[PersonVital.Health]*2.5m) %>px; background-color: red;"></div></div>
    		<div>Thirst: <%= (int)Player.Vitals[PersonVital.Thirst] %>%<div class="bar" style="width: <%= (int)(Player.Vitals[PersonVital.Thirst]*2.5m) %>px; background-color: lightblue;"></div></div>
    		<div>Hunger: <%= (int)Player.Vitals[PersonVital.Hunger] %>%<div class="bar" style="width: <%= (int)(Player.Vitals[PersonVital.Hunger]*2.5m) %>px; background-color: lightgreen;"></div></div>
    		<div>Activity: <%= Player.Activity.Text %></div>
    		<div>House: <%= (Player.Home != null ? (int)Player.Home.PercentComplete : 0) + "%" %><div class="bar" style="width: <%= (Player.Home != null ? (int)Player.Home.PercentComplete : 0)*2.5 %>px; background-color: brown;"></div></div>
    		<div>&nbsp;</div>
    		<div><b>Inventory</b></div>
    		<div>Food: <%= (int)Player.Inventory[ItemType.Food] %>kg<div class="bar" style="width: <%= (int)Player.Inventory[ItemType.Food]*2.5 %>px; background-color: brown;"></div></div>
    		<div>Water: <%= (int)Player.Inventory[ItemType.Water] %>ml<div class="bar" style="width: <%= (int)Player.Inventory[ItemType.Water]/3 %>px; background-color: lightblue;"></div></div>
    		<div>Timber: <%= (int)Player.Inventory[ItemType.Timber] %><div class="bar" style="width: <%= (int)Player.Inventory[ItemType.Timber]*2.5 %>px; background-color: lightblue;"></div></div>
    		<div>&nbsp;</div>
    		<div><b>Needs</b></div>
            <% foreach (var need in Player.Needs){ %>
    		<div><%= need.ActionType %> <%= need.ItemType %>: <%= need.Quantity %> (<%= need.Priority %>)</div>
            <% } %>
		<% } %>
	<% } else { %>   
        <div>No player found.</div>
    <% } %>
</div>