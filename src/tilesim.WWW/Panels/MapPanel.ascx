<%@ Control Language="C#" Inherits="tilesim.MapPanel" %>

<div id="map">

    <link rel="stylesheet" type="text/css" href="default.css"/>
    <style>
    .tile
    {
        width: 8px;
        height: 8px;
        border: 1px solid lightgray;
        padding: 0px;
    }

    .tile:hover
    {
        background-color: lightgray;
    }
    </style>
    <table id="mapTable" style="border-collapse: collapse;">
        <% for (int y = 0; y < TotalRows; y++){ %>
        <tr>
            <% for (int x = 0; x < TotalColumns; x++){ %>
            <td class="tile"><%= CreateTileContent(x, y) %></td>
            <% } %>
        </tr>
        <% } %>
    </table>
</div>