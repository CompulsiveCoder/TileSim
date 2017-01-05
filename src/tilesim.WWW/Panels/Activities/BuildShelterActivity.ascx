<%@ Control Language="C#" Inherits="tilesim.BuildShelterActivity" %>

<div class="activity" id="BuildShelterActivity" style="display:none;">
    <script language="javascript">
    function millTimber()
    {
        var path = 'Game.aspx?newactivity=Build-Shelter';

        // TODO: Overhaul
        $.get(path, function(result){
            $result = $(result);
            $('#BuildShelterActivity').hide();
            // TODO: Add output
        }, 'html');
    }
    </script>
    <h3>Build Shelter</h3>
    <div><input type="button" value="Start" onclick="millTimber();" /></div>
</div>