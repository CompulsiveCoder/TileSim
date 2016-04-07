<%@ Control Language="C#" Inherits="townsim.TilePanel" %>
<%@ Import Namespace="townsim.Engine.Entities" %>
<%@ Import Namespace="townsim.Engine" %>
<script runat="server">
</script>
  <script language="javascript" type="text/javascript" src="jquery-2.1.3.min.js"></script>
<script language="javascript">
function editTreesToPlantPerDay()
{
  /*$('#forestryCont').css("visibility", "visible");

  $.get('Forestry.aspx', function(result){
      $result = $(result);

      //$result.find('style').html('#forestryCont');
      $result.find('#body').appendTo('#forestryCont');
      $result.find('script').appendTo('#forestryCont');
  }, 'html');*/
}
</script>
<div class="pnl">
    <h1>Current Tile</h1>
    <h2>People</h2>
    <div>Population: <%= Tile.People.Length %><div class="bar" style="width: <%= Tile.People.Length %>px; background-color: lightgray;"></div></div>
    <h2>Resources</h2>
    <div>Timber: <%= (int)Tile.Inventory[ItemType.Timber] %><div class="bar" style="width: <%= (int)Tile.Inventory[ItemType.Timber] / 10 %>px; background-color: brown;"></div></div>
    <div>Water sources: <%= (int)Tile.Inventory[ItemType.Water] %>l<div class="bar" style="width: <%= (int)(Tile.Inventory[ItemType.Water] / 1000) %>px; background-color: lightblue;"></div></div>
    <div>Food sources: <%= (int)Tile.Inventory[ItemType.Food] %> kgs<div class="bar" style="width: <%= (int)(Tile.Inventory[ItemType.Food] / 1000) %>px; background-color: lightgreen;"></div></div>
    