<%@ Page Language="C#" %>
<%@ Import Namespace="townsim.Data" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>CurrentTown</title>
	<script runat="server">
	void Page_Load(object sender, EventArgs e)
	{
		var townIndexer = new TownIndexer();
		var towns = townIndexer.Get();
		var town = towns[0];
		Response.Redirect("Town.aspx?id=" + town.Id);
	}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	
	</form>
</body>
</html>

