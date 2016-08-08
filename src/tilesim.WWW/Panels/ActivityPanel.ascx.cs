using System;
using System.Web;
using System.Web.UI;
using tilesim.Engine.Entities;
using tilesim.Engine;
using tilesim.Web;

namespace tilesim
{

    public partial class ActivityPanel : System.Web.UI.UserControl
    {
        public Person Player;
        public GameTile Tile;

        public void Page_Load(object sender, EventArgs e)
        {
            if (EngineWebHolder.Current.IsStarted) {
                Player = EngineWebHolder.Current.Context.Player;
            }
        }
    }
}

