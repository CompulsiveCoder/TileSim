<%@ Page Language="C#" AutoEventWireup="True" %>
<%@ Import Namespace="townsim.Data" %>
<%@ Import Namespace="townsim.Entities" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Forestry Save</title>
	<script runat="server">
	Town Town;

	void Page_Load(object sender, EventArgs e)
	{
		var townId = new Guid(Request.QueryString["town"]);

		var treesPerDay = Convert.ToInt32(Request.QueryString["treesPerDay"]);

		var instruction = new EditInstruction(typeof(Town), townId, "TreesToPlantPerDay", treesPerDay);

		new InstructionSaver().Save(instruction);
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

