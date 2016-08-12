<%@ Control Language="C#" Inherits="tilesim.FindWaterActivity" %>
<div class="activity" id="FindWaterActivity" style="display:none;">
    <script language="javascript">
    function search()
    {
        var path = "Game.aspx?newactivity=Test";

        // TODO: Overhaul
        $.get(path, function(result){
            $result = $(result);
            //$('#startPanel').hide();
            // TODO: Add output
        }, 'html');
    }
    </script>
    <h3>Find Water</h3>
    <div><input type="button" value="Search" onclick="search()" /></div>
</div>