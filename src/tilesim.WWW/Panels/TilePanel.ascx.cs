using System;
using System.Web;
using System.Web.UI;
using tilesim.Engine;

namespace tilesim
{

    public partial class TilePanel : System.Web.UI.UserControl
    {
        public GameTile Tile { get; set; }

        void Page_Load(object sender, EventArgs e)
        {
            Tile = EngineHolder.Context.Player.Tile;
        }
    }
}

