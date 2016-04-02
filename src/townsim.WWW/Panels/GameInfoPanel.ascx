<%@ Control Language="C#" Inherits="townsim.GameInfoPanel" %>
<%@ Import Namespace="townsim.Data" %>
<%@ Import Namespace="townsim.Engine" %>
<%@ Import Namespace="townsim.Engine.Entities" %>
<div class="pnl">
	<h2>Game</h2>
	<div>Start Time: <%= CurrentEngine.Clock.StartTime %></div>
	<div>Real Time: <%= CurrentEngine.Clock.GetRealDurationString() %></div>
	<div>Game Time: <%= CurrentEngine.Clock.GetGameDurationString() %></div>
	<div>Game speed: <%= CurrentEngine.Info.Settings.GameSpeed %>x</div>
	<div>1 second equals <%= CurrentEngine.Clock.GetSpeedComparisonString() %></div>
</div>