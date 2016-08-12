<%@ Control Language="C#" Inherits="tilesim.StartActivityPanel" %>
<%@ Register TagPrefix="uc" TagName="FindWaterActivity" Src="~/Panels/Activities/FindWaterActivity.ascx" %>
<%@ Register TagPrefix="uc" TagName="GatherWaterActivity" Src="~/Panels/Activities/GatherWaterActivity.ascx" %>
<%@ Register TagPrefix="uc" TagName="DrinkWaterActivity" Src="~/Panels/Activities/DrinkWaterActivity.ascx" %>
<%@ Register TagPrefix="uc" TagName="FellWoodActivity" Src="~/Panels/Activities/FellWoodActivity.ascx" %>
<div class="pnl" id="StartActivityPanel">
    <script language="javascript">
        function showActivityForm()
        {
            var activity = $("#<%= ActionSelect.ClientID %>").val();

            $("#" + activity + "Activity").show();
        }
    </script>
    <h2>Start Activity</h2>
    <asp:DropDownList runat="server" id="ActionSelect" onchange="showActivityForm();">
      <asp:ListItem Value=""> -- choose -- </asp:ListItem>
      <asp:ListItem Value="FindWater">Find Water</asp:ListItem>
      <asp:ListItem Value="GatherWater">Gather Water</asp:ListItem>
      <asp:ListItem Value="DrinkWater">Drink Water</asp:ListItem>
      <asp:ListItem Value="GatherFood">Gather Food</asp:ListItem>
      <asp:ListItem Value="EatFood">Eat Food</asp:ListItem>
      <asp:ListItem Value="BuildShelter">Build Shelter</asp:ListItem>
      <asp:ListItem Value="FellWood">Fell Wood</asp:ListItem>
      <asp:ListItem Value="MillTimber">Mill Timber</asp:ListItem>
    </asp:DropDownList>
    <uc:FindWaterActivity id="FindWaterActivity" runat="server" />
    <uc:GatherWaterActivity id="GatherWaterActivity" runat="server" />
    <uc:DrinkWaterActivity id="DrinkWaterActivity" runat="server" />
    <uc:FellWoodActivity id="FellWoodActivity" runat="server" />
</div>