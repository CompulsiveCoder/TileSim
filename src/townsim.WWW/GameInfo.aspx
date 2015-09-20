<%@ Page Language="C#" %>
<%@ Import Namespace="townsim.Data" %>
<%@ Import Namespace="townsim.Engine" %>
<%@ Import Namespace="townsim.Entities" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Game Info</title>
	<script runat="server">
	EngineInfo Engine;
	EngineClock Clock;

	void Page_Load()
	{
		//Engine = new EngineInfoReader().Read(CurrentEngine.Id);
		//Clock = new EngineClock(Engine.StartTime, Engine.Settings);
	}
	</script>
</head>
<body>
	<form id="form">
		<div id="body">
			<h2>Game</h2>
			<div>Start Time: <%= CurrentEngine.Clock.StartTime %></div>
			<div>Real Time: <%= CurrentEngine.Clock.GetRealDurationString() %></div>
			<div>Game Time: <%= CurrentEngine.Clock.GetGameDurationString() %></div>
			<div>Game speed: <%= CurrentEngine.Info.Settings.GameSpeed %>x</div>
		</div>
	</form>
</body>
</html>

