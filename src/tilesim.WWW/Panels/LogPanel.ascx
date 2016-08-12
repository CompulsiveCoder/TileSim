<%@ Control Language="C#" Inherits="tilesim.LogPanel" %>
<%@ Import Namespace="tilesim.Log" %>
<div class="pnl" id="LogPanel">
	<h2>Log</h2>
	<div id="log" style="overflow:auto;height:300px;">
	<%= "" %>
	</div>
	<script language="javascript">
		var objDiv = document.getElementById("log");
		objDiv.scrollTop = objDiv.scrollHeight;
	</script>
</div>