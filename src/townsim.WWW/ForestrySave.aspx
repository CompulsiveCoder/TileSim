<%@ Page Language="C#" %>
<%@ Import Namespace="townsim.Data" %>
<%@ Import Namespace="townsim.Entities" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Forestry Save</title>
	<script runat="server">
	Town Town;

	void Page_Load()
	{
		// TOOD: Use a query string to specify the town
		var townId = new Guid(Request.QueryString["town"]);

		var treesPerDay = Convert.ToInt32(Request.QueryString["treesPerDay"]);

		Town = new TownReader().Read(townId);

		Town.TreesToPlantPerDay = treesPerDay;

		// TODO: Instead of directly editing the town, log a message with the engine to perform the update
		new TownSaver().Save(Town);

	}
	</script>
</head>
<body>
	<form id="form">
		<div id="body">
			<h2>Forestry</h2>
			<div>Forestry settings saved</div>
		</div>
	</form>
</body>
</html>

