<%@ Page Language="C#" %>
<%@ Import Namespace="townsim.Data" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Town</title>
	<script runat="server">
	public Town Town { get;set; }

	void Page_Load()
	{
		var id = new Guid(Request.QueryString["id"]);
		var reader = new TownReader();
		Town = reader.Read(id);
	}
	</script>
</head>
<body>
	<form runat="server">
		<div id="body">
			<h1><h1><%= Town.Name %></h1>
			<div>Population: <%= Town.Population %></div>
			<div>Forests: <%= Town.Forest %></div>
			<div>Water sources: <%= Town.WaterSources %></div>
		</div>
	</form>
</body>
</html>

