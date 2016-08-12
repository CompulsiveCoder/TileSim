<%@ Control Language="C#" Inherits="tilesim.FellWoodActivity" %>

<div class="activity" id="FellWoodActivity" style="display:none;">
    <script language="javascript">
    function fellWood()
    {
        var amount = $('#FellWoodAmount').val();
        var path = 'Game.aspx?newactivity=Fell-Wood&amount=' + amount;

        // TODO: Overhaul
        $.get(path, function(result){
            $result = $(result);
            $('#FellWoodActivity').hide();
            // TODO: Add output
        }, 'html');
    }
    </script>
    <h3>Fell Wood</h3>
    <div>Amount: <input type="text" value="100" id="FellWoodAmount" /></div>
    <div><input type="button" value="Start" onclick="fellWood();" /></div>
</div>