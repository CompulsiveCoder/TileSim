<%@ Page Language="C#" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="UTF-8"> 
	<title>Tile Sim</title>
    <link rel="stylesheet" type="text/css" href="default.css"/>
    <script type="text/javascript" src="jquery-2.1.3.min.js"></script>
    <script type="text/javascript" src="default.js"></script>
</head>
<body>
	<form id="form1" runat="server">   
        <div id="outer">
            <script language="javascript">
            var hasStarted = false;
            var refreshRate = 2000;

            </script>
    		<h1>Tile Sim</h1>
            <div id="startPanel">
            <p>Speed: <input type="text" id="GameSpeed" value="10"/></p>
            <p>
                <input type="button" id="newGame" onclick="startGame();" value="Start &raquo;"/>
            </p>
            </div>
    		<div id="gameCont"></div>
        
            <div id="rightCol">
              <div id="forestryCont"></div>
            </div>
    		<script language="javascript">
    			$(document).ready(function(){
    				showGame();
    			});
    		</script>
        </div>
	</form>
</body>
</html>

