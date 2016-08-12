<%@ Control Language="C#" Inherits="tilesim.GatherWaterActivity" %>
<div class="activity" id="GatherWaterActivity" style="display:none;">
    <script language="javascript">
    function gatherWater()
    {
        var path = "Game.aspx?newactivity=Gather-Water&amount=" + $("#Amount").val();

        // TODO: Overhaul
        $.get(path, function(result){
            $result = $(result);
            $('#GatherWaterActivity').hide();
            // TODO: Add output
        }, 'html');
    }
    </script>
    <h3>Collect Water</h3>
    <div>Amount (ml): <input type="text" value="1000" id="Amount" /></div>
    <div><input type="button" value="Collect" onclick="gatherWater()" /></div>
</div>