using System;
using System.Web;
using System.Web.UI;
using tilesim.Engine;
using tilesim.Web;

namespace tilesim
{

    public partial class TilePanel : System.Web.UI.UserControl
    {
        public GameTile Tile { get; set; }

        void Page_Load(object sender, EventArgs e)
        {
            if (EngineWebHolder.Current.IsStarted) {
                Tile = EngineWebHolder.Current.Context.Player.Tile;
            }
        }
    }
}

