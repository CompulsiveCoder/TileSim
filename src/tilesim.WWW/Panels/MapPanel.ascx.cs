using System;
using System.Web;
using System.Web.UI;
using tilesim.Web;
using tilesim.Engine.Entities;

namespace tilesim
{

	public partial class MapPanel : System.Web.UI.UserControl
	{
        public int TotalRows = 80;
        public int TotalColumns = 80;
        public Person Player { get;set; }

        public void Page_Load(object sender, EventArgs e)
        {
            if (EngineWebHolder.Current.IsStarted) {
                var game = EngineWebHolder.Current.Context;

                TotalRows = game.Settings.VerticalTileCount;
                TotalColumns = game.Settings.HorizontalTileCount;

                Player = game.Player;
            }
        }

        public string CreateTileContent(int x, int y)
        {
            return Player.Tile.PositionX == x && Player.Tile.PositionY == y ? "P" : "";
        }
	}
}

