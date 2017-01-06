<%@ Page Language="C#" %>
<%@ Import namespace="tilesim.Web" %>
<!DOCTYPE html>
<html>
<head runat="server">
 <title>Start Empty Engine</title>
 <script runat="server">
 void Page_Load(object sender, EventArgs e)
 {
    var speed = 1;
    // TODO: Remove or reimplement
    //var speed = Convert.ToInt32(Request.QueryString["speed"]);
    EngineWebHolder.Current.StartGame(speed);
    Response.Redirect("../Default.aspx");
 }
 </script>
</head>
<body>
 <form id="form1" runat="server">
 Empty engine started.
 </form>
</body>
</html>

