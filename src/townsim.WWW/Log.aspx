<%@ Page Language="C#" %>
<%@ Import Namespace="townsim.Data" %>
<%@ Import Namespace="townsim.Engine" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Town</title>
	<script runat="server">
	string EngineId = String.Empty;

	void Page_Load()
	{
		EngineId = CurrentEngine.Id;
	}
	</script>
</head>
<body>
	<form>
		<div id="body">
			<h2>Log</h2>
			<%= LogWriter.Current.ReadAll(EngineId).Replace("\n", "<br/>") %>
		</div>
	</form>
</body>
</html>

