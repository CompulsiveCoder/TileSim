<%@ Control Language="C#" Inherits="tilesim.PlayerPanel" %>
<%@ Import namespace="tilesim.Engine.Entities" %>
<div class="pnl" id="PlayerPanel">
	<h2>Player</h2>
	<% if (Player != null){ %>
		<% if (!Player.IsAlive){ %>
			<div>Player died. Game over!</div>
		<% } else { %>
    		<div>Age: <%= Convert.ToInt32(Player.Age) %></div>
    		<div>Health: <%= (int)Player.Vitals[PersonVitalType.Health] %>%<div class="bar" style="width: <%= (int)(Player.Vitals[PersonVitalType.Health]*2.5m) %>px; background-color: red;"></div></div>
    		<div>Thirst: <%= (int)Player.Vitals[PersonVitalType.Thirst] %>%<div class="bar" style="width: <%= (int)(Player.Vitals[PersonVitalType.Thirst]*2.5m) %>px; background-color: lightblue;"></div></div>
    		<div>Hunger: <%= (int)Player.Vitals[PersonVitalType.Hunger] %>%<div class="bar" style="width: <%= (int)(Player.Vitals[PersonVitalType.Hunger]*2.5m) %>px; background-color: lightgreen;"></div></div>
    		<div>Shelter: <%= (Player.Home != null ? (int)Player.Home.PercentComplete : 0) + "%" %><div class="bar" style="width: <%= (Player.Home != null ? (int)Player.Home.PercentComplete : 0)*2.5 %>px; background-color: brown;"></div></div>
    		<div>&nbsp;</div>
    		<div><b>Inventory</b></div>
    		<div>Food: <%= (int)Player.Inventory[ItemType.Food] %>kg<div class="bar" style="width: <%= (int)Player.Inventory[ItemType.Food]*2.5 %>px; background-color: brown;"></div></div>
    		<div>Water: <%= (int)Player.Inventory[ItemType.Water] %>ml<div class="bar" style="width: <%= (int)Player.Inventory[ItemType.Water]/30 %>px; background-color: lightblue;"></div></div>
            <div>Wood: <%= (int)Player.Inventory[ItemType.Wood] %><div class="bar" style="width: <%= (int)Player.Inventory[ItemType.Wood]/10 %>px; background-color: lightblue;"></div></div>
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