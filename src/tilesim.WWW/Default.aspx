<%@ Page Language="C#" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Tile Sim</title>
	<script language="javascript" type="text/javascript" src="jquery-2.1.3.min.js"></script>
	<script language="javascript" type="text/javascript" src="default.js"></script>
    <link rel="stylesheet" type="text/css" href="default.css"/>
</head>
<body>
	<form id="form1" runat="server">
		<h1>Tile Sim</h1>
        <div>Speed: <input type="text" id="GameSpeed" value="10"/></div>
        <div id="newGame" onclick="newGame();">New Game</div>
		<div id="gameCont"></div>
    
        <div id="rightCol">
          <div id="forestryCont"></div>
        </div>
		<script language="javascript">
			$(document).ready(function(){
				showGame();
			});
		</script>
	</form>
</body>
</html>

