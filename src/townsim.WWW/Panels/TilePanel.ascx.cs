using System;
using System.Web;
using System.Web.UI;
using townsim.Engine;

namespace townsim
{

    public partial class TilePanel : System.Web.UI.UserControl
    {
        public GameTile Tile { get; set; }

        void Page_Load(object sender, EventArgs e)
        {
            Tile = CurrentEngine.Context.Player.Tile;
        }
    }
}

