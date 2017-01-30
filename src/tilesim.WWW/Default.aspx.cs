using System;
using System.Web;
using System.Web.UI;
using tilesim.Engine.Entities;
using tilesim.Data;

namespace tilesim.WWW
{
    
    public partial class Default : System.Web.UI.Page
    {
        public EngineInfo[] ExistingEngines = new EngineInfo[]{};
        void Page_Load(object sender, EventArgs e)
        {
            ExistingEngines = new GameLister ().ListGames ();
        }
    }
}

