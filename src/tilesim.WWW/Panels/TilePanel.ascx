<%@ Control Language="C#" Inherits="tilesim.TilePanel" %>
<%@ Import Namespace="tilesim.Engine.Entities" %>
<%@ Import Namespace="tilesim.Engine" %>
<%@ Import Namespace="tilesim.Web" %>
<script runat="server">
</script>
<script language="javascript" type="text/javascript" src="jquery-2.1.3.min.js"></script>
<div class="pnl" id="TilePanel">
    <h1>Current Tile</h1>
    <h2>Resources</h2>
    <div>Timber: <%= (int)Tile.Inventory[ItemType.Timber] %><div class="bar" style="width: <%= (int)Tile.Inventory[ItemType.Timber] / 10 %>px; background-color: brown;"></div></div>
    <div>Water sources: <%= (int)Tile.Inventory[ItemType.Water] %>l<div class="bar" style="width: <%= (int)(Tile.Inventory[ItemType.Water] / 1000) %>px; background-color: lightblue;"></div></div>
    <div>Food sources: <%= (int)Tile.Inventory[ItemType.Food] %> kgs<div class="bar" style="width: <%= (int)(Tile.Inventory[ItemType.Food] / 1000) %>px; background-color: lightgreen;"></div></div>
    <h2>People</h2>
    <div>Population: <%= Tile.People.Length %><div class="bar" style="width: <%= Tile.People.Length %>px; background-color: lightgray;"></div></div>
    <div>
    <% foreach (var person in Tile.People){ %>
        <div><%= person.ToString() %></div>
    <% } %>
    </div>
</div>
    