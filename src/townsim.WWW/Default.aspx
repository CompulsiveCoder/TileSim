<%@ Page Language="C#" Inherits="townsim.Default" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Default</title>
	<script language="javascript" type="text/javascript" src="jquery-2.1.3.min.js"></script>
	<script language="javascript" type="text/javascript" src="default.js"></script>
</head>
<body>
	<link rel="stylesheet" type="text/css" href="default.css">
	<style>
		body
		{
			font-family: Verdana;
			font-size: 11px;
		}

		div
		{
			padding: 4px;
		}
	</style>
	<form id="form1" runat="server">
	<h1>Town Sim</h1>
		<div class="pnl" id="listCont">
		</div>
		<div class="pnl" id="propCont">
			<div id="propInner">
			</div>
		</div>
	</form>
</body>
</html>

