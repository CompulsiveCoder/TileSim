<%@ Page Language="C#" %>
<%@ Import Namespace="townsim.Data" %>
<%@ Import Namespace="townsim.Engine" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Log</title>
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
			<link rel="stylesheet" type="text/css" href="default.css">
			<h2>Log</h2>
			<div id="log" style="overflow:auto;height:300px;">
			<%= LogWriter.Current.ReadAll(EngineId).Replace("\n", "<br/>").Replace("\r", "<br/>") %>
			</div>
			<script language="javascript">
				var objDiv = document.getElementById("log");
				objDiv.scrollTop = objDiv.scrollHeight;
			</script>
		</div>
	</form>
</body>
</html>

