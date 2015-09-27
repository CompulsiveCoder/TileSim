<%@ Page Language="C#" %>
<%@ Import Namespace="townsim.Engine" %>
<%@ Register TagPrefix="uc" TagName="Town" Src="~/Panels/Town.ascx" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Current Town</title>
</head>
<body>
	<form id="form1" runat="server">
		<uc:Town id="CurrentTown" runat="server" />
	</form>
</body>
</html>

