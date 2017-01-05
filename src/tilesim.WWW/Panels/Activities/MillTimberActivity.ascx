<%@ Control Language="C#" Inherits="tilesim.MillTimberActivity" %>

<div class="activity" id="MillTimberActivity" style="display:none;">
    <script language="javascript">
    function millTimber()
    {
        var amount = $('#MillTimberAmount').val();
        var path = 'Game.aspx?newactivity=Mill-Timber&amount=' + amount;

        // TODO: Overhaul
        $.get(path, function(result){
            $result = $(result);
            $('#MillTimberActivity').hide();
            // TODO: Add output
        }, 'html');
    }
    </script>
    <h3>Mill Timber</h3>
    <div>Amount: <input type="text" value="100" id="MillTimberAmount" /></div>
    <div><input type="button" value="Start" onclick="millTimber();" /></div>
</div>