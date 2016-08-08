<%@ Control Language="C#" Inherits="tilesim.GameInfoPanel" %>
<%@ Import Namespace="tilesim.Engine" %>
<%@ Import Namespace="tilesim.Engine.Entities" %>
<%@ Import Namespace="tilesim.Web" %>
<div class="pnl">
	<h2>Game</h2>
	<div>Start Time: <%= EngineWebHolder.Current.Context.Clock.StartTime %></div>
	<div>Real Time: <%= EngineWebHolder.Current.Context.Clock.GetRealDurationString() %></div>
	<div>Game Time: <%= EngineWebHolder.Current.Context.Clock.GetGameDurationString() %></div>
	<div>Game speed: <%= EngineWebHolder.Current.Context.Info.Settings.GameSpeed %>x</div>
	<div>1 second equals <%= EngineWebHolder.Current.Context.Clock.GetSpeedComparisonString() %></div>
</div>